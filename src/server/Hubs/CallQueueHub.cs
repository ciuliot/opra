using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using OP.RemoteAdvisor.DAO;
using System.Threading.Tasks;

namespace OP.RemoteAdvisor.Hubs
{
  public class CallQueueHub : Hub<ICallQueueHubClient>
  {
    readonly ILogger _logger;
    readonly IMongoCollection<FinishedCall> _finishedCalls;

    public CallQueueHub(IMongoDatabase database,
      ILogger<CallQueueHub> logger)
    {
      _logger = logger;
      _finishedCalls = database.GetCollection<FinishedCall>("finishedCalls");
    }

    public override Task OnDisconnectedAsync(System.Exception exception)
    {
      _logger.LogInformation($"Unsubscribing agent {Context.User.Identity.Name}" +
        $" [{Context.UserIdentifier}]");

      return base.OnDisconnectedAsync(exception);
    }

    public async Task SubscribeAgent(string workflowType)
    {
      _logger.LogInformation($"Subscribing agent {Context.User.Identity.Name}" +
        $" [{Context.UserIdentifier}] to workflow type {workflowType}");
      
      await Groups.AddToGroupAsync(Context.ConnectionId, workflowType);
      var agentId = new ObjectId(Context.UserIdentifier);

      var finishedCalls = await _finishedCalls.Find(x =>
        x.AgentId == agentId &&
        x.WorkflowType == workflowType).SortByDescending(x => x.Started)
        .Limit(10).ToListAsync();

      await Clients.Caller.FinishedCallsChanged(finishedCalls.ToArray());
    }    
  }
}