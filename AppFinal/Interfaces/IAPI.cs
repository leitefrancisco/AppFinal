﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace AppFinal.Interfaces
{
    public interface IAPI
    {
        [Post("/login")]
        Task<string> SignIn([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Post("/register")]
        Task<string> Register([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);
    }
}
