using System;

namespace Lab5.EPAM.Services.Exceptions
{
    public class ServiceException : Exception
    {
        protected ServiceException()
        {

        }

        public ServiceException(string message)
            : base(message)
        {

        }

        public ServiceException(Exception exception)
            : base("See inner exception", exception)
        {

        }
    }
}
