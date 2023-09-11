﻿using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs;

public class UserDTO
{
    [Key]
    public uint Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string Username { get; set; }
    public BasicInformationDTO BasicInformation { get; set; }
    public TimeTable<UserDTO> UsersTimeTable { get; set; }
    public IEnumerable<RoleDTO> Roles { get; set; }
}
