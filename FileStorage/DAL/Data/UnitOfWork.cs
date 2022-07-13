using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace DAL.Data
{
    /// <summary>
    /// UnitOfWork class contains all repositories and used only one dbContext
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        private readonly FileStorageContext _context;

        private IRepository<Entities.File> _fileRepository;
        private IRepository<FileSecureLevel> _fileSecureLevelRepository;
        private IRepository<FileType> _fileTypeRepository;
        private IRepository<FullFileInfo> _fullFileInfoRepository;
        private IRepository<User> _userRepository;
        public IRepository<Entities.File> FileRepository
        {
            get
            {
                if (_fileRepository == null)
                {
                    _fileRepository = new FileRepository(_context);
                }
                return _fileRepository;
            }
        }
        public IRepository<FileSecureLevel> FileSecureLevelRepository
        {
            get
            {
                if (_fileSecureLevelRepository == null)
                {
                    _fileSecureLevelRepository = new FileSecureLevelRepository(_context);
                }
                return _fileSecureLevelRepository;
            }
        }
        public IRepository<FileType> FileTypeRepository
        {
            get
            {
                if (_fileTypeRepository == null)
                {
                    _fileTypeRepository = new FileTypeRepository(_context);
                }
                return _fileTypeRepository;
            }
        }
        public IRepository<FullFileInfo> FullFileInfoRepository
        {
            get
            {
                if (_fullFileInfoRepository == null)
                {
                    _fullFileInfoRepository = new FullFileInfoRepository(_context);
                }
                return _fullFileInfoRepository;
            }
        }
        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public UnitOfWork(FileStorageContext Db)
        {

            _context = Db;
        }

        /// <summary>
        /// Save any changes in db
        /// </summary>
        /// <returns>Task</returns>
        public async Task SaveAsync()
        {
            _ = await _context.SaveChangesAsync();
        }
    }
}
