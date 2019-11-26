using System.IO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace OP.RemoteAdvisor.Pages
{
  public class KioskDesktopModel : PageModel
  {
    #region PackageVersion
    public string PackageVersion
    {
      get
      {
        using (var sr = new StreamReader("package.json"))
        {
          var package = JObject.Parse(sr.ReadToEnd());
          return package["version"].ToString();
        }
      }
    }
    #endregion
    readonly ILogger _logger;

    public KioskDesktopModel(ILogger<AgentModel> logger)
    {
      _logger = logger;
    }

    public void OnGet()
    {

    }
  }
}
