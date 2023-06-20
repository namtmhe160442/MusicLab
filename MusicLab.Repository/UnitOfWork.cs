using MusicLab.Repository.Models;
using MusicLab.Repository.Repositories;
using MusicLab.Repository.Repositories.Interfaces;

namespace MusicLab.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicLabContext _context;

        private IUserRepository _userRepository;


        public UnitOfWork(MusicLabContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository { get { return _userRepository ?? new UserRepository(_context); } }
    }
}
