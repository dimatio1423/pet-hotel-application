using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.RoleService
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        Role GetRoleById(string id);
        void Add(Role role);
        void Delete(Role role);
        void Update(Role role);
    }
}
