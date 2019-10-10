using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BookStore.Helper
{
    public class SessionHelper
    {
        public static void SetObjectAsJson(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetWebsiteSession(ISession session, string hash)
        {
            session.SetString("BookStore", hash);
        }

        public static void SetUserSession(ISession session, int userID)
        {
            session.SetInt32("UserID", userID);
        }

        public static void SetCartSession(ISession session)
        {
            SetObjectAsJson(session, "Cart", new List<CartBook>());
        }

        public static List<CartBook> GetCartSession(ISession session)
        {
            return GetObjectFromJson<List<CartBook>>(session, "Cart");
        }

        public static void ClearSession(ISession session)
        {
            session.Clear();
        }
    }
}
