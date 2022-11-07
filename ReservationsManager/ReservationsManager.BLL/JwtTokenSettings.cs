namespace ReservationsManager.BLL
{
    public class JwtTokenSettings
    {
        public string SecretKey { get; set; }
        public int Duration { get; set; }
    }
}