﻿using Microsoft.AspNetCore.Identity;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
namespace SimpleWebDal.Models.WebUser;

public class User : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public Guid? BasicInformationId { get; set; }
    public BasicInformation BasicInformation { get; set; } = new BasicInformation();
    public Guid? UserCalendarId { get; set; }
    public CalendarActivity UserCalendar { get; set; } = new CalendarActivity()
    {
        Id = Guid.NewGuid(),
        Activities = new List<Activity>()
    };
    public ICollection<Role>? Roles { get; set; }
    public ICollection<Adoption>? Adoptions { get; set; }
    public ICollection<Pet>? Pets { get; set; }
}
