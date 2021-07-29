namespace ApiServices.jwtManager
{
  public interface IJwtTokenManager
  {
    string GenerateToken(string username,string role);
  }
}
