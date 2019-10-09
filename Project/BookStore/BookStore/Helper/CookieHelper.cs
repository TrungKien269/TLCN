﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Http;

namespace BookStore.Helper
{
    public class CookieHelper
    {
        public static CookieOptions SetCookie()
        {
            return new CookieOptions
            {
                Expires = DateTime.Now.AddHours(2)
            };
        }

        public static void SetWebsiteCookie(HttpResponse Response, string hash)
        {
            Response.Cookies.Append("BookStore", hash, CookieHelper.SetCookie());
        }
    }
}
