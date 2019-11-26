using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OP.RemoteAdvisor.Hubs
{
  public class UserIdProvider : IUserIdProvider
  {
    public string GetUserId(HubConnectionContext connection)
    {
      //var claims = connection.User.Claims;
      //return claims.First(x => x.Type == ClaimTypes.SerialNumber).Value;
      var bytes = Encoding.Default.GetBytes(connection.ConnectionId);
      var sb = new StringBuilder();

      foreach(var b in bytes)
      {
        sb.AppendFormat("{0:X2}", b);
      }
      return sb.ToString();
    }
  }
}