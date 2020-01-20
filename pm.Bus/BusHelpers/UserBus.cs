using pm.Data;
using pm.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pm.Bus.BusHelpers
{
    public interface IUserBus
    {
        User GetBynationalCode(string email);
        User GetById(Guid uId);
        List<User> GetAll();
        User Update(User user);

        void Create(User user);
    }

    public class UserBus : IUserBus
    {
        private prContext _ctx;

        public UserBus(prContext ctx)
        {
            _ctx = ctx;
        }

        public User GetBynationalCode(string nationalId)
        {
            var user = _ctx.Users.FirstOrDefault(x => x.NationalCode == nationalId);
            return user;
        }

        public User GetById(Guid uId)
        {
            var user = _ctx.Users.SingleOrDefault(x => x.Id == uId);
            return user;
        }

        public User Update(User user)
        {
            _ctx.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _ctx.SaveChanges();
            return user;
        }

        public List<User> GetAll()
        {
            var users = _ctx.Users.ToList();
            return users;
        }

        //public void Create(User userData)
        //{
        //    user = new User
        //    {
        //        NationalCode = userData.NationalCode,
        //        PasswordHash = passwordHash,
        //        PasswordSalt = passwordSalt
        //    };

        //    _ctx.Add(user);
        //    _ctx.SaveChanges();
        //}
    }
}
