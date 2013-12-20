using System;

namespace Lab5.EPAM.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Rollback();

        void PreSave();
    }
}
