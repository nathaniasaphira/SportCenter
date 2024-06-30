namespace SportCenter.Models.Entities;

public class User
{
    public static readonly string Table = "user";

    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
