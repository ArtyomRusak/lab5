using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.EPAM.Services.Exceptions
{
    public class MembershipServiceException : ServiceException
    {
        protected MembershipServiceException()
        {

        }

        public MembershipServiceException(string message)
            : base(message)
        {

        }

        public MembershipServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
