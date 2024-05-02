using Confluent.Kafka;
using LeaderboardService.Contracts.Events;
using LeaderboardService.Domain.Exceptions;
using LeaderboardService.EventHandlers.Interfaces;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Serilog;

namespace LeaderboardService.EventHandlers;

public class EventListenerService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IEventDispatcher _eventDispatcher;

    public EventListenerService(ILogger logger,
        IEventDispatcher eventDispatcher)
    {
        _logger = logger;
        _eventDispatcher = eventDispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Information("Starting listening events...");

        var bootstrapServers = Environment.GetEnvironmentVariable("BootstrapServers");

        if (string.IsNullOrEmpty(bootstrapServers))
        {
            var exception = new InternalServerException("Environment variable 'BootstrapServers' is missing!");
            _logger.Error(exception, "Can't start listening process.");
            throw exception;
        }
        
        var groupId = Environment.GetEnvironmentVariable("GroupId");
        
        if (string.IsNullOrEmpty(groupId))
        {
            var exception = new InternalServerException("Environment variable 'GroupId' is missing!");
            _logger.Error(exception, "Can't start listening process.");
            throw exception;
        }
        
        var topic = Environment.GetEnvironmentVariable("Topic");
        
        if (string.IsNullOrEmpty(topic))
        {
            var exception = new InternalServerException("Environment variable 'Topic' is missing!");
            _logger.Error(exception, "Can't start listening process.");
            throw exception;
        }
        
        var consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        var retryPipeline = GetRetryPipeline();
        
        using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
        {
            consumer.Subscribe(new []{topic});

            await retryPipeline.ExecuteAsync(async token =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    
                    // handle consumed message.
                    var messageBody = consumeResult.Message.Value;

                    try
                    {
                        IEvent<IEventData> e = JsonConvert.DeserializeObject<IEvent<IEventData>>(messageBody, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });
                    
                        if(e == null)
                            continue;
                        
                        
                        await _eventDispatcher.Dispatch(e.EventData);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error occured when processing event message.");
                    }
                }
            } );

            consumer.Close();
        }
    }

    private ResiliencePipeline GetRetryPipeline()
    {
        ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions()
            {
                MaxRetryAttempts = 20,
                OnRetry = args =>
                {
                    _logger.Information("On Retry Consume Events, Attempt: {0}", args.AttemptNumber);
                    
                    return default;
                }
            }) // Add retry using the default options
            .AddTimeout(TimeSpan.FromSeconds(10)) // Add 10 seconds timeout
            .Build();
        
        return pipeline;
    }
}