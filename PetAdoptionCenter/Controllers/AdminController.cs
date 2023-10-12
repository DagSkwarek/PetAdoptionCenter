﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.Models.WebUser;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost("createRole")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        return Ok();
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser(string email, string password)
    {
        var user = new User { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            return Ok(roleResult);
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("createShelterOwner")]
    public async Task<IActionResult> CreateUserShelterOwner(string email, string password)
    {
        var user = new User { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            
            var roleResult = await _userManager.AddToRoleAsync(user, "ShelterOwner");

            if (roleResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                
                return BadRequest(roleResult.Errors);
            }
        }
        return BadRequest(result.Errors);
    }

   
}