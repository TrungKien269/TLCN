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
        public static async Task<CookieOptions> SetCookie()
        {
            return await Task.FromResult<CookieOptions>(new CookieOptions
            {
                Expires = DateTime.Now.AddHours(2)
            });
        }
    }
}
