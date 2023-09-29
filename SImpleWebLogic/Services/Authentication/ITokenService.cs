﻿using Microsoft.AspNetCore.Identity;
using SimpleWebDal.Models.WebUser;

public interface ITokenService
{
    string CreateToken(User user, string role);
}