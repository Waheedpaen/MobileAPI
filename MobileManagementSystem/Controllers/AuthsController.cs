using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MobileManagementSystem.Controllers
 ;
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthsController : BaseController {  
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public AuthsController(IUserService userService, IMapper mapper, IConfiguration config)
        {
            _userService = userService;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUser()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);  
            var enityData = await _userService.GetUsers();
            var userDto = _mapper.Map<List<UserListDto>>(enityData);
            if (userDto != null)
            {
                _response.Data = userDto;
                _response.Success = true;
                return Ok(_response);
            }
            else
            {
                _response.Success = false;
                _response.Message = CustomMessage.DataNotExit;
                return Ok(_response);
            }
             
        }
        [HttpGet("GetUserCount")]
        public async Task<IActionResult> GetUserCount()
        {
            var userCounts = await _userService.GetUserCount();
            return Ok(userCounts);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDtoLogin userDtoLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objUserLogin = await _userService.Login(userDtoLogin);

            if (objUserLogin == null)
            {
                _response.Success = false;
                _response.Message = CustomMessage.UserUnAuthorized;
                return Ok(_response);
            }
            Claim[] claims = new[]
            {
                new Claim(Enums.ClaimType.UserId.ToString(), objUserLogin.Id.ToString()),
                new Claim(Enums.ClaimType.Email.ToString(), objUserLogin.Email.ToString()),
                new Claim(Enums.ClaimType.Role.ToString(), objUserLogin.Role.ToString()),
                new Claim(Enums.ClaimType.Name.ToString(), objUserLogin.UserName.ToString()),
                new Claim(Enums.ClaimType.UserTypeId.ToString(), objUserLogin.UserTypeId.ToString()),
                new Claim(Enums.ClaimType.ImageUrl.ToString(), objUserLogin.ImageUrl.ToString()),


            };
    
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JsonWebTokenKeys:IssuerSigningKey"]));
            var token = new JwtSecurityToken(expires: DateTime.Now.AddHours(3), claims: claims, signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            _response.Data = new
            {
                loggedInUserId = claims.FirstOrDefault(x => x.Type.Equals(Enums.ClaimType.UserId.ToString())).Value,
                loggedInUserName = claims.FirstOrDefault(x => x.Type.Equals(Enums.ClaimType.Name.ToString())).Value,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                loggedInUserTypeId = claims.FirstOrDefault(x => x.Type.Equals(Enums.ClaimType.UserTypeId.ToString())).Value,
                
      

            };

            _response.Success = true;
            return Ok(_response);
        }

        [HttpPost("AddData")]
        public async Task<IActionResult> AddUser(UserAddDto obj)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            obj.Username = obj.Username.ToLower();
             if(await _userService.UserAlreadyExit(obj.Username))
                return BadRequest(new {message = CustomMessage.UserAlreadyExist});  

           _response= await  _userService.AddUser(obj);
        
      

            return Ok(_response);


        }
        //UWL ... method check exitence name 

        [HttpGet("CheckUserNameExistence/{Name}")]
        public async Task<IActionResult> CheckNameExistence(string Name)
        {
            var obj = await _userService.CheckUserNameExistence(Name);
            return Ok(obj);

        }
        [HttpGet("CheckUserName/{Name}")]
        public async Task<IActionResult> CheckUserName(string Name)
        {
            var obj = await _userService.UserAlreadyExit(Name);
            return Ok(obj);

        }
        [HttpDelete("DeleteUserData/{Id}")]
        public async Task<IActionResult> DeleteUserData(int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var obj = await _userService.GetUser(Id);
            if (obj != null)
            {
                await _userService.Delete(obj);
                _response.Success = true;
                _response.Message = CustomMessage.Deleted;
                return Ok(_response);
            }
            else
            {
                _response.Success = false;
                _response.Message = CustomMessage.RecordNotFound;
                return Ok(_response);
            }
        }

        [HttpGet("SearchData/{Name}")]
        public async Task<IActionResult> SearchData(string Name)
        {
          if (!ModelState.IsValid) return BadRequest(ModelState);
      var osSearchData = await _userService.SearchingData(Name);
            _response.Data = osSearchData;
            _response.Success = true;
            return Ok(_response);
        }

        [HttpGet("GetUserTypes")]
        public async Task<IActionResult> GetUserTypes()
        {
            var data = await _userService.GetUserTypes();
            var dataDto = _mapper.Map<List<UserTypesDto>>(data);
            return Ok(dataDto);

        }
        [HttpGet("GetUser/{Id}")]
        public async Task<IActionResult> GetUser(int Id)
        {
            var data = await _userService.GetUser(Id);
            var dataDto = _mapper.Map<UserListDto>(data);
            if (dataDto != null)
            {
                _response.Data = data; ;
                _response.Success = true;
                return Ok(_response);

            }
            else
            {
                _response.Data = null;
                _response.Success = false;
                return Ok(_response);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var modelEntity = _mapper.Map<User>(model);
            var obj = await _userService.GetUser(modelEntity.Id);
            if (obj != null)
            {
                await _userService.UpdateUser(obj,modelEntity);
                _response.Success = true;
                _response.Message = CustomMessage.Updated;
                return Ok(_response);
            }
            else
            {
                _response.Success = false;
                _response.Message = CustomMessage.RecordNotFound;
                return Ok(_response);
            }

        }

    [HttpPut("UpdatePassword")]

    public async Task<IActionResult> UpdatePassword(ChangePasswordDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var obj = await _userService.GetUser(model.Id);
        if (obj != null)
        {
            await _userService.ChangePassword(obj, model);
            _response.Success = true;
            _response.Message = CustomMessage.Updated;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.RecordNotFound;
            return Ok(_response);
        }
    }





}

