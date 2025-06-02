using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using AssessmentService.Models;

class Program{
    private const string PersonListRequestQueue = "person_list_request";
    private const string PersonListResponseQueue = "person_list_response";
    private const string PersonDetailRequestQueue = "person_details_request";
    private const string PersonDetailResponseQueue = "person_details_response";
    
    
    static void Main(string[] args)
    {        
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Person");
        var collection = database.GetCollection<Person>("PersonDetails");

        // Declare queues for both processes
        channel.QueueDeclare(queue: PersonListRequestQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueDeclare(queue: PersonListResponseQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueDeclare(queue: PersonDetailRequestQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueDeclare(queue: PersonDetailResponseQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        // Setup consumer for Person List
        var listConsumer = new EventingBasicConsumer(channel);
        listConsumer.Received += (model, ea) =>        {            
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            var parts = message.Split('|');
            var correlationId = parts[1];
            channel.BasicAck(ea.DeliveryTag, false);
            var personList = GetPersonListFromMongo(collection);

            var responseProps = channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;
            var responseMessage = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(personList));
            channel.BasicPublish(exchange: "", routingKey: PersonListResponseQueue, basicProperties: responseProps, body: responseMessage);
        };
        // Setup consumer for Person Detail
        var detailConsumer = new EventingBasicConsumer(channel);
        detailConsumer.Received += (model, ea) =>        {            
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            var parts = message.Split('|');
            var personId = parts[0];
            var correlationId = parts[1];
            channel.BasicAck(ea.DeliveryTag, false);
            var personDetails = GetPersonDetailsFromMongo(personId,collection);

            var responseProps = channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;
            var responseMessage = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(personDetails));
            channel.BasicPublish(exchange: "", routingKey: PersonDetailResponseQueue, basicProperties: responseProps, body: responseMessage);
        };
        // Start consuming messages
        channel.BasicConsume(queue: PersonListRequestQueue, noAck: false, consumer: listConsumer);
        channel.BasicConsume(queue: PersonDetailRequestQueue, noAck: false, consumer: detailConsumer);

        Console.WriteLine("Combined worker started. Listening for messages...");
        Console.ReadLine();
    }
    private static List<Person> GetPersonListFromMongo(IMongoCollection<Person> collection)
    {        
        return collection.Find(_ => true).ToList();
    }

    private static Person GetPersonDetailsFromMongo(string personId,IMongoCollection<Person> collection)
    {
        return collection.Find(p => p.Id == personId).FirstOrDefault();
    }
}
