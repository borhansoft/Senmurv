using pm.Data;
using pm.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pm.Bus.BusHelpers
{
    public interface IRoleBus
    {
        Role Get(Guid Id);
        List<Role> GetAll();
        void Create(Role role);
        bool IsUniqueRole(string name);

    }

    public class RoleBus : IRoleBus
    {
        private prContext _prContext;
        public RoleBus(prContext prContext)
        {
            _prContext = prContext;
        }

        public void Create(Role role)
        {
            _prContext.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _prContext.SaveChanges();
        }

        public Role Get(Guid id)
        {
            var role = _prContext.Roles.FirstOrDefault(x => x.Id == id);
            return role;
        }

        public List<Role> GetAll()
        {
            var roles = _prContext.Roles.ToList();
            return roles;
        }

        public bool IsUniqueRole(string name)
        {
            return !_prContext.Roles.Any(x => x.Name == name);
        }
    }
}
