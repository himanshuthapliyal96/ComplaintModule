namespace ApiServices
{
  public class ApiResponse
  {
    public string Token { get; set; }
    public object Context { get; set; }

    public static ApiResponse GenerateResponse(string token, object context)
    {
      return  new ApiResponse {Token = token,Context = context};
    }
  }
}
