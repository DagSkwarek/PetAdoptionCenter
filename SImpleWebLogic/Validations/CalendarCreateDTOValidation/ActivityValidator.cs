﻿using FluentValidation;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;

namespace SImpleWebLogic.Validations.CalendarCreateValidation;

public class ActivityValidator : AbstractValidator<ActivityCreateDTO>
{
    public ActivityValidator()
    {
        RuleFor(activity => activity.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(activity => activity.ActivityDate)
            .NotEmpty().WithMessage("ActivityDate cannot be empty.")
            .Must(date => date > DateTime.Now).WithMessage("ActivityDate must be in the future.");
    }
}