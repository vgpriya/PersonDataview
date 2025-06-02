using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private const string RequestQueueName = "person_details_request";
    private const string ResponseQueueName = "person_details_response";

    private const string RequestQueueListName = "person_list_request";
    private const string ResponseQueueListName = "person_list_response";

    private readonly RabbitMqService _rabbitMqService;

    public PersonController(RabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;

        // Declare queues (optional: move to RabbitMqService)
        _rabbitMqService.Channel.QueueDeclare(RequestQueueName, false, false, false, null);
        _rabbitMqService.Channel.QueueDeclare(ResponseQueueName, false, false, false, null);
        _rabbitMqService.Channel.QueueDeclare(RequestQueueListName, false, false, false, null);
        _rabbitMqService.Channel.QueueDeclare(ResponseQueueListName, false, false, false, null);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetPersonList()
    {
        var correlationId = Guid.NewGuid().ToString();
        var message = $"list|{correlationId}";
        var body = Encoding.UTF8.GetBytes(message);

        var props = _rabbitMqService.Channel.CreateBasicProperties();
        props.ReplyTo = ResponseQueueListName;
        props.CorrelationId = correlationId;

        _rabbitMqService.Channel.BasicPublish("", RequestQueueListName, props, body);

        var response = await WaitForResponse(ResponseQueueListName, correlationId);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonDetails(string id)
    {
        var correlationId = Guid.NewGuid().ToString();
        var message = $"{id}|{correlationId}";
        var body = Encoding.UTF8.GetBytes(message);

        var props = _rabbitMqService.Channel.CreateBasicProperties();
        props.ReplyTo = ResponseQueueName;
        props.CorrelationId = correlationId;

        _rabbitMqService.Channel.BasicPublish("", RequestQueueName, props, body);

        var response = await WaitForResponse(ResponseQueueName, correlationId);
        return Ok(response);
    }

    private Task<string> WaitForResponse(string responseQueueName, string correlationId)
    {
        var tcs = new TaskCompletionSource<string>();

        // Create a dedicated consumer for this request
        var consumer = new EventingBasicConsumer(_rabbitMqService.Channel);

        // Generate a unique consumer tag for this request
        string consumerTag = null; // store the tag returned by BasicConsume
        consumer.Received += (model, ea) =>
        {
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                var response = Encoding.UTF8.GetString(ea.Body.ToArray());
                if (tcs.TrySetResult(response))
                {
                    // Acknowledge and stop consuming this response
                    _rabbitMqService.Channel.BasicAck(ea.DeliveryTag, false);
                    if (!string.IsNullOrEmpty(consumerTag))
                    {
                        // Cancel consumer to unsubscribe event
                        _rabbitMqService.Channel.BasicCancel(consumerTag);
                    }
                }
            }
        };
        // Start consuming and get the actual consumer tag returned by RabbitMQ
        consumerTag = _rabbitMqService.Channel.BasicConsume(
            queue: responseQueueName, 
            noAck: false, 
            consumer: consumer);

        return tcs.Task;
    }
}
