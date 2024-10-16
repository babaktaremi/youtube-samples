namespace KafkaWithBlockingCollection.Brokers;

public interface IMessageBroker
{
    T? Consume<T>(string topic, TimeSpan timeout) where T : class;
    IEnumerable<T?> ConsumeEnumerable<T>(string topic, CancellationToken cancellationToken) where T : class;

    void Produce<T>(string topic, T message) where T : class;

    void Close(string topic);
}