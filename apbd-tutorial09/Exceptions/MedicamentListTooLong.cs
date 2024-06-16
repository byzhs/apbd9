namespace apbd_tutorial09.Exceptions;

public class MedicamentListTooLong: Exception
{
    public MedicamentListTooLong(int numberOfMedicaments): base($"Number of medicaments should be max 10, it was {numberOfMedicaments}")
    {
    }
}