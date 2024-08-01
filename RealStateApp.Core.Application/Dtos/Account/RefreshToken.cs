﻿namespace RealStateApp.Core.Application.Dtos.Account
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string ReplaceByToken { get; set; } = string.Empty;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
