using System;
using Microsoft.AspNetCore.Mvc;
using pm.Manager.Manager;
using pm.Bus.BusHelpers;
using pm.Dto;
using pm.Data.Models;

namespace pm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IAccountManager _accountManager;

        public IUserBus _userBus;

        public LoginController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        public ActionResult Login(LoginDto userData)
        {
            try
            {
                //User user = _accountManager.Login(userData.Email, userData.Password);
                string userJwt = _accountManager.Login(userData.NationalCode, userData.Password);

                if (userJwt == null)
                    return BadRequest("Invalid inputs");


                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    Token = userJwt
                });
            }
            catch (Exception)
            {

                throw;
            }
            
        }



        public string hello()
        {
            return "hello";
        }
    }
}