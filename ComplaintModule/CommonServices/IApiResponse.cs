using Newtonsoft.Json.Linq;

namespace ApiServices
{
  public interface IApiResponse
  {
    JObject GenerateResponse(string token, object context);
  }
}
