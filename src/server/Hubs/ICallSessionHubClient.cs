using System.Threading.Tasks;
using OP.RemoteAdvisor.DAO;

namespace OP.RemoteAdvisor.Hubs
{
  public interface ICallSessionHubClient
  {
    Task ParticipantsChanged(CallSessionParticipant[] participants);
  }
}