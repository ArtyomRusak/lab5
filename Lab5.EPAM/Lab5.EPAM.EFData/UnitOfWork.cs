using System;
using System.Data.Entity;
using Lab5.EPAM.Core;
using Lab5.EPAM.Core.Entities;
using Lab5.EPAM.Core.Exceptions;
using Lab5.EPAM.Core.InterfaceRepository;
using Lab5.EPAM.EFData.Repositories;

namespace Lab5.EPAM.EFData
{
    public class UnitOfWork : IUnitOfWork, IRepositoryFactory
    {
        #region [Private members]

        private bool _disposed;
        private bool _isTransactionActive;
        private readonly DbContext _context;
        private readonly DbContextTransaction _transaction;
        private IRepository<User, int> _userRepository;
        private IRepository<Role, int> _roleRepository;

        #endregion


        #region [Ctor's]

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
            _isTransactionActive = true;
        }

        #endregion


        #region Implementation of IDisposable

        public void Dispose()
        {
            if (_isTransactionActive)
            {
                try
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
                catch (Exception e)
                {
                    _transaction.Rollback();
                    _context.Dispose();
                    _disposed = true;
                    _isTransactionActive = false;
                    throw new RepositoryException(e);
                }
            }
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        #endregion

        #region Implementation of IUnitOfWork

        public void Commit()
        {
            try
            {
                if (_isTransactionActive && !_disposed)
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                _isTransactionActive = false;
                throw new RepositoryException(e.Message);
            }
        }

        public void Rollback()
        {
            if (_isTransactionActive)
            {
                try
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
                catch (Exception e)
                {
                    _transaction.Rollback();
                    _isTransactionActive = false;

                    _context.Dispose();
                    _disposed = true;

                    throw new RepositoryException(e);
                }
            }
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        public void PreSave()
        {
            _context.SaveChanges();
        }

        #endregion

        #region Implementation of IRepositoryFactory

        public IRepository<User, int> GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new Repository<User, int>(_context));
        }

        public IRepository<Role, int> GetRoleRepository()
        {
            return _roleRepository ?? (_roleRepository = new Repository<Role, int>(_context));
        }

        #endregion
    }
}
