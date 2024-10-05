using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Request;
using CarRentalSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                return users;
            }
        }

        public async Task<UserEntity> GetAsyncByEmail(string email)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
                return user;
            }
        }

        public async Task<UserEntity> GetAsyncById(Guid id)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                return user;
            }
        }

        public async Task<UserEntity> GetAsyncByMobile(string mobile)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Mobile == mobile);
                return user;
            }
        }

        public async Task<UserEntity> GetAsyncByUsername(string username)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);
                return user;
            }
        }

        public async Task<bool> SaveAsync(UserEntity user)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.Users.AddAsync(user);
                var res = await context.SaveChangesAsync();
                return res > 0 ? true : false;
            }
        }

        public async Task<bool> UpdateAsync(UserEntity user)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Users.Update(user);
                var res = await context.SaveChangesAsync();
                return res > 0 ? true : false;
            }
        }
    }
}
