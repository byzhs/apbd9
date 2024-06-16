namespace apbd_tutorial09.Exceptions;

public abstract class NotFoundException(string message) : Exception(message);

public class MedicamentNotFound(int id): NotFoundException($"Medicament with id {id} not found.");

public class PatientNotFound(int id) : NotFoundException($"Patient with id {id} not found.");

public class UserNotFound(string username) : NotFoundException($"User with username {username} not found.");
