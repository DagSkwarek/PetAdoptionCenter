﻿using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;

namespace SimpleWebDal.Models.WebUser;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public int FlatNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public Shelter? Shelter { get; set; }
    public TempHouse? TemporaryHouse { get; set; }
}