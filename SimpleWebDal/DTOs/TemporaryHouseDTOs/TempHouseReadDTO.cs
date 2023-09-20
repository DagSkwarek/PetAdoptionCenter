﻿using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.DTOs.TemporaryHouseDTOs;

public class TempHouseReadDTO
{
    public Guid Id { get; set; }
    public UserReadDTO TemporaryOwner { get; set; }
    public AddressReadDTO TemporaryHouseAddress { get; set; }
    public ICollection<PetReadDTO> PetsInTemporaryHouse { get; set; }
    public ShelterReadDTO ShelterName { get; set; }
    public DateTime StartOfTemporaryHouseDate { get; set; }
}
