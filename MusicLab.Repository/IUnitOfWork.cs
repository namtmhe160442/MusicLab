using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Repository
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
    }
}
