﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Core.Token
{
    public class TokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
