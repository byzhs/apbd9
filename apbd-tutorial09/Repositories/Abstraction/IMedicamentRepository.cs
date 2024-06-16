namespace apbd_tutorial09.Repositories.Abstraction;

public interface IMedicamentRepository
{
    Task<bool> IsMedicamentExistsAsync(int id);
}