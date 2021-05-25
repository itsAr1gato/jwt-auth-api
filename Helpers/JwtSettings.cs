namespace Webproj.Helpers
{
    public interface IJwtSettings
    {
        string Secret { get; set; }
    }

    public class JwtSettings
    {
        public string Secret { get; set; }
    }
}