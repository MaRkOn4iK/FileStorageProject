﻿using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for UnitOfWork class which contains readonly props for each repository
    /// </summary>
    public interface IUnitOfWork
    {
        IRepository<File> FileRepository { get; }
        IRepository<FileSecureLevel> FileSecureLevelRepository { get; }
        IRepository<FileType> FileTypeRepository { get; }
        IRepository<FullFileInfo> FullFileInfoRepository { get; }
        IRepository<User> UserRepository { get; }
        Task SaveAsync();
    }
}
