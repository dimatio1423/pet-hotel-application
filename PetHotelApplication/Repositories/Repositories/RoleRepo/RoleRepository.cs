using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.RoleRepo
{
    public class RoleRepository : IRoleRepository
    {
        public void Add(Role role)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                _context.Roles.Add(role);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Role role)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var currRole = _context.Pets.FirstOrDefault(x => x.Id.Equals(role.Id));

                _context.Remove(currRole);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Role GetRoleById(string id)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Roles.FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Role role)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                //_context.Categories.Update(category);
                _context.Entry<Role>(role).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
