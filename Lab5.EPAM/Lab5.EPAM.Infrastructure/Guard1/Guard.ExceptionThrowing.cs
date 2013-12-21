using System;
using Lab5.EPAM.Infrastructure.Guard.Exceptions;
using YP.ToolKit.System;

namespace Lab5.EPAM.Infrastructure.Guard
{
    /// <summary>
    /// Represents guardian for common used validation rules
    /// </summary>
    public static partial class Guard
    {
        /// <summary>
        /// Throws exception
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="exception">Exception to throw</param>
        public static void Throw<T>([NotNull] T exception) where T : Exception
        {
            throw exception;
        }

        /// <summary>
        /// Throws exception
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="message">The exception message</param>
        public static void Throw<T>(string message) where T : ExceptionBase, new()
        {
            var exception = new T();
            exception.SetMessage(message);
            throw exception;
        }

        /// <summary>
        /// Throws exception
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception of current exception</param>
        public static void Throw<T>(string message, Exception innerException) where T : ExceptionBase, new()
        {
            var exception = new T();
            exception.SetMessage(message);
            exception.SetInnerException(innerException);
            throw exception;
        }
        /// <summary>
        /// Throws custom excetion
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        public static void Throw<T>() where T : Exception, new()
        {
            throw new T();
        }
    }
}