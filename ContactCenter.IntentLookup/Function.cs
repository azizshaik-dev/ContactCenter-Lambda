using Amazon.Lambda.ConnectEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ContactCenter.IntentLookup.cs;

public class Function
{

    private readonly IntentRepo intentRepo;
    private readonly string _defaultQueue;
    public Function()
    {
        Console.WriteLine("In constructor, starting to build dependencies.");
        var startup = new Startup();
        IServiceProvider provider = startup.ConfigureServices();
        intentRepo = provider.GetRequiredService<IntentRepo>();
        _defaultQueue = Environment.GetEnvironmentVariable("DefaultIntent");
        Console.WriteLine("Done building dependencies.");

    }

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<IntentResponse> FunctionHandler(ContactFlowEvent input, ILambdaContext context)
    {
        context.Logger.Log("Fetching the queue info from the db");
        if (input.Details.Parameters.TryGetValue("intent", out string intent))
        {
            var queue = await intentRepo.ReadAsync(intent);
            if (queue != null)
            {
                return new IntentResponse { QueueArn = queue.QueueArn };
            }
        }
        return new IntentResponse { QueueArn = _defaultQueue };

    }
}

public class IntentResponse
{
    public string QueueArn { get; set; }
}