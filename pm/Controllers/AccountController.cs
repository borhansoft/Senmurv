using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pm.Data;
using pm.Core.Helpers;
using pm.Dto;
using pm.Data.Models;
using pm.Bus.BusHelpers;

namespace pm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private prContext _ctx;
        private IUserBus _userBus;

        //remove from here refactor
        public AccountController(prContext ctx, IUserBus userBus)
        {
            _ctx = ctx;
            _userBus = userBus;

        }

        [HttpPost]
        public ActionResult register(LoginDto newUser)
        {
            User user = null;

            if (!String.IsNullOrWhiteSpace(newUser.NationalCode) &&
                !String.IsNullOrWhiteSpace(newUser.Password) &&
                UserUniqe(newUser.NationalCode))
            {
                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(newUser.Password, out passwordHash, out passwordSalt);
                user = new User
                {
                    NationalCode = newUser.NationalCode,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _ctx.Add(user);
                _ctx.SaveChanges();
                return Ok(user);
            }

            return BadRequest("email is already taken");
        }


        public bool UserUniqe(string nationalCode)
        {
            var exists = _ctx.Users.Any(x => x.NationalCode == nationalCode);

            return !exists;
        }


        [HttpGet]
        public ActionResult Edit(Guid userId)
        {
            var user = _userBus.GetById(userId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPut]
        public ActionResult Edit(User userData)
        {
            //User user = _userBus.GetById(userData.Id);
            if (!UserUniqe(userData.NationalCode))
            {
                //if (_userBus.GetById(userData.Id) != null)
                //{
                    var asdf = _userBus.Update(userData);
                    return Ok(asdf);
                //}
                //else
                //{
                //    return NotFound();
                //}

            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var usersList = _userBus.GetAll().ToList();
            return Ok(usersList);
        }


        [HttpGet("GetById")]
        public ActionResult GetUser(Guid id)
        {
            var user = _userBus.GetById(id);
            return Ok(user);
        }

    }
}