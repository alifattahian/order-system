using Domain.Constants;

namespace Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public string Code { get; private set; }
        public DomainValidationException(ErrorCode errorCode) : this(errorCode.Code, errorCode.Description)
        {

        }
        public DomainValidationException() : base()
        {

        }

        public DomainValidationException(string code, string message) : base(message)
        {
            Code = code;
        }

        public DomainValidationException(string message) : base(message)
        {

        }

        public DomainValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
