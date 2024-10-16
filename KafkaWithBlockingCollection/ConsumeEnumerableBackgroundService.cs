using KafkaWithBlockingCollection.Brokers;

namespace KafkaWithBlockingCollection;

public class ConsumeEnumerableBackgroundService:BackgroundService
{
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<ConsumerBackgroundService> _logger;

    public ConsumeEnumerableBackgroundService(IMessageBroker messageBroker, ILogger<ConsumerBackgroundService> logger)
    {
        _messageBroker = messageBroker;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var message in _messageBroker.ConsumeEnumerable<MessageModel>(Constants.Enumerable_TOPIC,stoppingToken))
            {
                if (message is not null)
                {
                    _logger.LogInformation("Got a message with Id: {messageId} and Name: {name}", message.Id,
                        message.Name);
                }
                else
                    _logger.LogInformation("There is no message for the given topic.");
            }
        }
    }
}