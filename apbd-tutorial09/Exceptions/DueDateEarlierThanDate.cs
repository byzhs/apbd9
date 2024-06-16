namespace apbd_tutorial09.Exceptions;

public class DueDateEarlierThanDate: Exception
{
    public DueDateEarlierThanDate(): base("Due date can not be earlier than the date")
    {
    }
}