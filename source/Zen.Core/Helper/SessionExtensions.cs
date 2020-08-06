using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Zen.Core.Helper
{
    public static class SessionExtensions
    {
        private const string CART_KEY = "cart";

        public static T GetData<T>(this ISession session, string key = CART_KEY)
        {
            var data = session.GetString(key);
            if (data is null)
                return default;

            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetData(this ISession session, object data, string key = CART_KEY)
        {
            session.SetString(key, JsonConvert.SerializeObject(data));
        }
    }
}