using System.ComponentModel.DataAnnotations;

namespace apbd_tutorial09.Contracts;

public class RegisterUserRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}