using Newtonsoft.Json;
using System.Text;

namespace ReservationsManager.Tests
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(this object obj) =>
            new(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
}
