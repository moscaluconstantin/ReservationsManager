namespace ReservationsManager.Common.Dtos.Auth
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}
