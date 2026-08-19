using Messanger.DataAccess.Models;

namespace Messenger.Repository.Interfaces
{
    public interface IMessageRepository
    {
        public Task<List<Message>> GetAsync();

        public Task<Message> GetByIdAsync(int id);

        public Task AddAsync(User user);

        public Task SaveChangesAsync();

    }
}
