using BusinessObjects.Entities;
using Repositories.Repositories.RoleRepo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepo;

        public RoleService(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public void Add(Role role)
        {
            _roleRepo.Add(role);
        }

        public void Delete(Role role)
        {
            _roleRepo.Delete(role);
        }

        public Role GetRoleById(string id)
        {
            return _roleRepo.GetRoleById(id);
        }

        public List<Role> GetRoles()
        {
            return _roleRepo.GetRoles();
        }

        public void Update(Role role)
        {
            _roleRepo.Update(role);
        }
    }
}
