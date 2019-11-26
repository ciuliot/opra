using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using OP.RemoteAdvisor.DAO;
using System.Threading.Tasks;
using System.Linq;

namespace OP.RemoteAdvisor.Hubs
{
  public class CallSessionHub : Hub<ICallSessionHubClient>
  {
    readonly ILogger _logger;
    readonly IMongoCollection<FinishedCall> _finishedCalls;
    readonly IDistributedCache _distributedCache;
    const string CallParticipantsKey = "callParticipants";
    const string CallSessionGroup = "callSession";

    public CallSessionHub(IMongoDatabase database,
      IDistributedCache distributedCache,
      ILogger<CallSessionHub> logger)
    {
      _logger = logger;
      _distributedCache = distributedCache;
      _finishedCalls = database.GetCollection<FinishedCall>("finishedCalls");
    }

    async Task<CallSession> GetSession()
    {
      var val = await _distributedCache.GetStringAsync(CallParticipantsKey);
      return string.IsNullOrEmpty(val) ?
        new CallSession() :
        JsonConvert.DeserializeObject<CallSession>(val);
    }

    async Task SetSession(CallSession session)
    {
      await _distributedCache.SetStringAsync(CallParticipantsKey,
        JsonConvert.SerializeObject(session));

      await Clients.Group(CallSessionGroup).ParticipantsChanged(session.Participants.Keys.ToArray());
    }

    public override async Task OnDisconnectedAsync(System.Exception exception)
    {
      var session = await GetSession();

      var q = from p in session.Participants
              where p.Value == Context.ConnectionId
              select p.Key;

      if (q.Any())
      {
        var role = q.First();
        _logger.LogInformation($"Unsubscribing {role} {Context.User.Identity.Name}" +
          $" [{Context.UserIdentifier}]");

        session.Participants.Remove(role);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, CallSessionGroup);

        await SetSession(session);
      }

      await base.OnDisconnectedAsync(exception);
    }

    public async Task RegisterParticipant(CallSessionParticipant participant)
    {
      var session = await GetSession();

      if (session.Participants.ContainsKey(participant))
      {
        _logger.LogInformation($"Removing participant {participant}");
        await Groups.RemoveFromGroupAsync(session.Participants[participant], CallSessionGroup);

        session.Participants.Remove(participant);
      }

      _logger.LogInformation($"Registering {Context.User.Identity.Name}" +
        $" [{Context.UserIdentifier}] as {participant}");

      session.Participants.Add(participant, Context.ConnectionId);
      await Groups.AddToGroupAsync(Context.ConnectionId, CallSessionGroup);

      await SetSession(session);
    }
  }
}