namespace ReservationsManager.BLL.Interfaces
{
    public interface IGetIdService
    {
        Task<int> GetIdByUernameAsync(string username);
    }
}
