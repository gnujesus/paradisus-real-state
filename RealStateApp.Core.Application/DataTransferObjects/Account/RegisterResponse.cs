﻿namespace RealStateApp.Core.Application.DataTransferObjects.Account
{
    public class RegisterResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
