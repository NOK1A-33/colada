using Messanger.DataAccess.Models;

namespace Messenger.Repository.Interfaces
{
    public interface IUserRepository 
    {
        public Task<List<User>> GetAsync();

        public Task<User> GetByIdAsync(int id);

        public Task AddAsync(User user);

        public Task SaveChangesAsync();
    }
} 