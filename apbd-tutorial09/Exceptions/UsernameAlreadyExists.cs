namespace apbd_tutorial09.Exceptions;

public class UsernameAlreadyExists: Exception
{
    public UsernameAlreadyExists(string username) : base($"Username {username} already exists.")
    {
    }
}