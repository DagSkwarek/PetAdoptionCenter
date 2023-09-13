﻿
using SimpleWebDal.Models.Calendar;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.WebUser;

public class User
{
    [Key]
    [Required]
    public int UserId { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string Username { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string Password { get; set; }
    public int BasicInformationId { get; set; }
    [Required]
    public BasicInformation BasicInformation { get; set; }
    public int TimeTableId { get; set; }
    public TimeTable UserTimeTable { get; set; }
    public ICollection<Role> Roles { get; set; }
}
