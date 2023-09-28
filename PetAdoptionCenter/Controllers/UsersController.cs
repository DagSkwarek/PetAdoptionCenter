using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Repository.UserRepo;
using SImpleWebLogic.Configuration;

namespace PetAdoptionCenter.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private readonly ValidatorFactory _validatorFactory;

    public UsersController(IUserRepository userRepository, IMapper mapper, ValidatorFactory validatorFactory)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validatorFactory = validatorFactory;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return Ok(_mapper.Map<IEnumerable<UserReadDTO>>(users));
    }
    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserReadDTO>> GetUserById(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user != null)
        {
            return Ok(_mapper.Map<UserReadDTO>(user));
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<UserReadDTO>> AddUser(UserCreateDTO userCreateDTO)
    {
        var userModel = _mapper.Map<User>(userCreateDTO);
        var addedUser = await _userRepository.AddUser(userModel);
        var userValidator = _validatorFactory.GetValidator<UserCreateDTO>();
        var userCredentialsValidator = _validatorFactory.GetValidator<CredentialsCreateDTO>();
        var userBasicInformationValidator = _validatorFactory.GetValidator<BasicInformationCreateDTO>();
        var userAddressValidator = _validatorFactory.GetValidator<AddressCreateDTO>();

        var validationResult = userValidator.Validate(userCreateDTO);
        var validationResultCredentials = userCredentialsValidator.Validate(userCreateDTO.Credentials);
        var validationResultBasicInformation = userBasicInformationValidator.Validate(userCreateDTO.BasicInformation);
        var validationResultAddress = userAddressValidator.Validate(userCreateDTO.BasicInformation.Address);

        if (!validationResult.IsValid || !validationResultCredentials.IsValid ||
            !validationResultBasicInformation.IsValid || !validationResultAddress.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var userReadDTO = _mapper.Map<UserReadDTO>(userModel);

        return CreatedAtRoute(nameof(GetUserById), new { id = userReadDTO.Id }, userReadDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        bool deleted = await _userRepository.DeleteUser(id);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(Guid id, UserCreateDTO userCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        if (foundUser == null)
        {
            return NotFound();
        }
        if (foundUser.Id != id)
        {
            return BadRequest("User ID does not match the request.");
        }
        var userCreateDto = _mapper.Map(userCreateDTO, foundUser);

        bool updated = await _userRepository.UpdateUser(foundUser);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }

    [HttpGet("pets")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllPets()
    {
        var pets = await _userRepository.GetAllPets();
        return Ok(_mapper.Map<IEnumerable<PetReadDTO>>(pets));
    }

    [HttpGet("pets/{id}", Name = "GetPetById")]
    public async Task<ActionResult<PetReadDTO>> GetPetById(Guid id)
    {
        var pets = await _userRepository.GetPetById(id);
        if (pets != null)
        {
            return Ok(_mapper.Map<PetReadDTO>(pets));
        }
        return NotFound();
    }

    [HttpGet("{id}/calendar/activities")]
    public async Task<ActionResult<IEnumerable<ActivityReadDTO>>> GetAllActivities(Guid id)
    {
        var userCalendar = await _userRepository.GetUserActivities(id);
        if (userCalendar != null)
        {
            return Ok(_mapper.Map<IEnumerable<ActivityReadDTO>>(userCalendar));
        }
        return NotFound();
    }
    [HttpGet("{id}/calendar/activities/{activityId}", Name = "GetActivityById")]
    public async Task<ActionResult<ActivityReadDTO>> GetActivityById(Guid id, Guid activityId)
    {
        var userActivity = await _userRepository.GetUserActivityById(id, activityId);
        if (userActivity != null)
        {
            return Ok(_mapper.Map<ActivityReadDTO>(userActivity));
        }
        return NotFound();
    }

    [HttpPost("{id}/calendar/activities")]
    public async Task<ActionResult<ActivityReadDTO>> AddActivity(Guid id, ActivityCreateDTO activityCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        var activityModel = _mapper.Map<Activity>(activityCreateDTO);
        var addedActivity = await _userRepository.AddActivity(id, activityModel);

        var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        var validationResult = activityValidator.Validate(activityCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var activityReadDTO = _mapper.Map<ActivityReadDTO>(activityModel);

        return CreatedAtRoute(nameof(GetActivityById), new { id = foundUser.Id, activityId = addedActivity.Id }, activityReadDTO);
    }


    [HttpPut("{id}/calendar/activities/{activityId}")]
    public async Task<ActionResult> UpdateUserActivity(Guid id, Guid activityId, ActivityCreateDTO activityCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        var foundActivity = await _userRepository.GetUserActivityById(id, activityId);
        if (foundUser == null || foundActivity == null)
        {
            return NotFound();
        }
        if (foundUser.Id != id || foundActivity.Id != activityId)
        {
            return BadRequest("User ID does not match the request.");
        }
        var activityCreate = _mapper.Map(activityCreateDTO, foundActivity);

        bool updated = await _userRepository.UpdateActivity(id, foundActivity);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}/activities/{activityId}")]
    public async Task<ActionResult> DeleteActivity(Guid id, Guid activityId)
    {
        bool deleted = await _userRepository.DeleteActivity(id, activityId);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }



}




//[HttpGet("{id}/pets/type")]
//public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterPets(Guid shelterId)
//{
//    var pets = await _userRepository.GetAllShelterPets(shelterId);
//    if (pets != null)
//    {
//        return Ok(pets);
//    }
//    return BadRequest();
//}








