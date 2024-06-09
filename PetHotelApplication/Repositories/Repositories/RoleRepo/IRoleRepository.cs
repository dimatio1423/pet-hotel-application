using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.RoleRepo
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role GetRoleById(string id);
        void Add(Role role);
        void Delete(Role role);
        void Update(Role role);
    }
}
