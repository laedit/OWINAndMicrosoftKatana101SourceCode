﻿using Microsoft.Owin.Security;
using System;

namespace SecuredWebApi
{
    public class DigestAuthenticationOptions : AuthenticationOptions
    {
        public DigestAuthenticationOptions() : base("Digest") { }
        public string Realm { get; set; }
        public Func<byte[]> GenerateNonceBytes { get; set; }
    }
}