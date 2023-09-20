﻿using SimpleWebDal.DTOs.AddressDTOs;

namespace SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;

public class BasicInformationReadDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public AddressReadDTO addressDTO { get; set; }
}