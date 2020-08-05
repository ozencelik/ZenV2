using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json;

namespace Zen.Core.Helper
{
    public static class Session
    {
        private const string CART_KEY = "cart";

        public static void Set<T>(this ISession session, T value, string key = CART_KEY)
        {
            session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
        }

        public static T Get<T>(this ISession session, string key = CART_KEY)
        {
            byte[] value;

            session.TryGetValue(key, out value);

            return value == null ? default :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}
