using Confluent.Kafka;
using LeaderboardService.Contracts.Events;
using LeaderboardService.Tests.Common;
using LeaderboardService.Tests.MockGameServer;
using Newtonsoft.Json;

Console.WriteLine("Running test mock game server...");

int minEventsPerSecond = int.Parse(Environment.GetEnvironmentVariable("MinEventsPerSecond"));
int maxEventsPerSecond = int.Parse(Environment.GetEnvironmentVariable("MaxEventsPerSecond"));
int minTimeout = int.Parse(Environment.GetEnvironmentVariable("MinTimeout"));
int maxTimeout = int.Parse(Environment.GetEnvironmentVariable("MaxTimeout"));
string bootstrapServers = Environment.GetEnvironmentVariable("BootstrapServers");
string topic = Environment.GetEnvironmentVariable("Topic");

var config = new ProducerConfig
{
    BootstrapServers = bootstrapServers
};

var retryPipeline = PollyHelper.GetRetryPipeline();

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    ConsoleKeyInfo input;
    
    string GetRandomEventJson()
    {
        var updateUserScoreEventData = EntityGenerator.GetUpdateUserScoreEventData();
        var e = new Event<UpdateUserScoreEventData>(updateUserScoreEventData);

        var json = JsonConvert.SerializeObject(e, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
        });
        return json;
    }
    
    // try to send first message
    retryPipeline.ExecuteAsync(async token =>
    {
        var json = GetRandomEventJson();
        await producer.ProduceAsync(topic, new Message<Null, string> { Value=json });
    });
    
    do
    {
        var random = new Random();
        var eventCount = random.Next(minEventsPerSecond, maxEventsPerSecond);

        List<Task> tasks = new List<Task>();
        
        for (int i = 0; i < eventCount; i++)
        {
            var json = GetRandomEventJson();
            var task = producer.ProduceAsync(topic, new Message<Null, string> { Value=json });
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
        
        var timeout = random.Next(minTimeout, maxTimeout);

        await Task.Delay(timeout);
    
        Console.WriteLine("{0} Sent new {1} events to the broker", DateTime.UtcNow, eventCount);
        
    } while (true);
}