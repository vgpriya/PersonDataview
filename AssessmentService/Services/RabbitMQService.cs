using RabbitMQ.Client;

public class RabbitMqService : IDisposable{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqService()    {        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public IModel Channel => _channel;

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}