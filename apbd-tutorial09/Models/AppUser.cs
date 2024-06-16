namespace apbd_tutorial09.Models;

public class AppUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}