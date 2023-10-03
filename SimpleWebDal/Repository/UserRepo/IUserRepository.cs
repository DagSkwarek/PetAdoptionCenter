﻿using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Repository.UserRepo;

public interface IUserRepository
{
    //GET
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User> GetUserById(Guid userId);
    public Task<IEnumerable<Pet>> GetAllPets();
    public Task<Pet> GetPetById(Guid id);
    
    //public Task<IEnumerable<Pet>> GetAllShelters();
    //public Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId);
    // public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type);
    public Task<Pet> GetShelterPetById(Guid shelterId, Guid petId);
    //public Task<CalendarActivity> GetCalendarForUser(Guid userId);
    public Task<IEnumerable<Activity>> GetUserActivities(Guid userId);
    public Task<Activity> GetUserActivityById(Guid userId, Guid activityId);
    public Task<IEnumerable<Pet>> GetAllFavouritePets(Guid id);
    public Task<Pet> GetFavouritePetById(Guid userId, Guid petId);
    public Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets();
    public Task<Pet> GetVirtualAdoptedPetById(Guid favouriteId);
    public Task<IEnumerable<Role>> GetAllUserRoles(Guid id);
    public Task<Role> GetUserRoleById(Guid id, Guid roleId);



    //POST
    public Task<Activity> AddActivity(Guid userId, Activity activity);
    public Task<User> AddUser(User user);
    public Task<Pet> AddFavouritePet(Guid userId, Guid petId);
    public Task<Role> AddRole(Guid id, Role role);



    //PUT or PATCH
    public Task<bool> UpdateUser(User user);
   // public Task<bool> PartialUpdateUser(User user);

    public Task<bool> UpdateActivity(Guid userId, Activity activity);
    public Task<bool> UpdateUserRole(Guid userId, Role role);



    //DELETE
    public Task<bool> DeleteUser(Guid userId);
    public Task<bool> DeleteActivity(Guid userId, Guid activityId);
    public Task<bool> DeleteFavouritePet(Guid id, Guid petId);
    public Task<bool> DeleteUserRole(Guid userId, Guid roleId);
}