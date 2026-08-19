using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {

        public static List<User> Users2 = new List<User>(){
                new User{Id = 1, Email = "RomeoJudge@gmail.com", Name = "123", OutputName = "555", Password = "Z9y$KlmN", RegDate = DateTime.Now}
            };
        public async Task<List<User>> GetAsync()
        {
            return Users2;
        }

        public async Task<User> GetByIdAsync(int id) 
        {
            return new User();
        }

        public async Task AddAsync(User user) 
        {
            Users2.Add(user);
        }

        public async Task SaveChangesAsync() 
        {
            
        }
    }
} 