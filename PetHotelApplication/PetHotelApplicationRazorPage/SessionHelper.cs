using Newtonsoft.Json;

namespace PetHotelApplicationRazorPage
{
    public static class SessionHelper
    {
        public static void SetObjectSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
