using System.Threading.Tasks;
using OP.RemoteAdvisor.DAO;

namespace OP.RemoteAdvisor.Hubs
{
  public interface ICallQueueHubClient
  {
    Task FinishedCallsChanged(FinishedCall[] finishedCalls);
  }
}