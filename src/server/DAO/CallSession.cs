using System.Collections.Generic;

namespace OP.RemoteAdvisor.DAO
{
  public class CallSession
  {
    public IDictionary<CallSessionParticipant, string> Participants =
      new Dictionary<CallSessionParticipant, string>();
  }
}