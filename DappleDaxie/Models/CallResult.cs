using System.Web.Script.Serialization;

namespace DappleDaxie.Models
{
  public class CallResult
  {
    public bool Success { get; set; }
    public object Result { get; set; }

    public string ToJson()
    {
      return new JavaScriptSerializer().Serialize(this);
    }
  }
}