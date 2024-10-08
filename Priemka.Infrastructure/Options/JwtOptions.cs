﻿using Microsoft.Extensions.Options;

namespace Priemka.Infrastructure.Options
{
    public sealed class JwtOptions
    {
        public const string Jwt = nameof(Jwt);
        public string SecretKey { get; init; } = string.Empty;
        public int Expires { get; init; }
    }   
}
