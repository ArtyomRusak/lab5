using System;

namespace Lab5.EPAM.Core.Exceptions
{
    public class RepositoryException : Exception
    {
        protected RepositoryException()
        {

        }

        public RepositoryException(string message)
            : base(message)
        {

        }

        public RepositoryException(Exception exception)
            : base("See inner exception.", exception)
        {

        }
    }
}
