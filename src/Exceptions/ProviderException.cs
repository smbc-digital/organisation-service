
using System;

namespace organisation_service.Exceptions
{
    public class ProviderException : Exception
    {
        public ProviderException(string message)
            : base(message)
        {
        }
    }
}
