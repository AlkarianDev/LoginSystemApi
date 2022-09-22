using AllegicWebApi.DTO;
using AllegicWebApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllegicWebApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        //--------------------------------------login-User-----------------------------------
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            
            //Get User from database
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == login.UserName.ToLower());

            // If username empty
            if (user == null) return Unauthorized("Invalid username");
            
            // check if the password match for user 
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            //If the password don't match
            if (!result.Succeeded) return Unauthorized("Invalid Password");

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        //-------------------------------------------Register-User---------------------------
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTO register)
        {
            //Check if user Exist
            if (await UserExists(register.UserName)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(register);

            user.UserName = register.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            //Check if role exist if not sead all Roles to the DB
            var roleCheck = await _roleManager.RoleExistsAsync("Member");
            if (!roleCheck)
            {
                //Seed Roles if not exist //
                var roles = new List<AppRole>
                {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
                };
                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
                /////////////////////////////
            }

            //Add to member
            var rolesResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!rolesResult.Succeeded) return BadRequest(result.Errors);

            //Create New User with Token
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }


        //-----------------------------------------------------------------------------------
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }



        //-----------------------------------------------------------------------------------

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
