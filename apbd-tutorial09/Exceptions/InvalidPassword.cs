namespace apbd_tutorial09.Exceptions;

public class InvalidPassword: Exception
{
    public InvalidPassword() : base("Invalid password!")
    {
    }
}