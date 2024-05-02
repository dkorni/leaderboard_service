using Polly;
using Polly.Retry;

namespace LeaderboardService.Tests.MockGameServer;

public static class PollyHelper
{
    public static ResiliencePipeline GetRetryPipeline()
    {
        ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions()
            {
                MaxRetryAttempts = 20,
                OnRetry = args =>
                {
                    Console.WriteLine("On Retry Publish Events, Attempt: {0}", args.AttemptNumber);
                    
                    return default;
                }
            }) // Add retry using the default options
            .AddTimeout(TimeSpan.FromSeconds(10)) // Add 10 seconds timeout
            .Build();
        
        return pipeline;
    }
}