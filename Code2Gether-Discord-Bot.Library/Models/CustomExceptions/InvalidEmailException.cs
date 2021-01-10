using System;

namespace Code2Gether_Discord_Bot.Library.Models.CustomExceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {
            
        }

        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
