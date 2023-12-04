namespace BuildingBlocks.Exceptions;

using System;

public class ServiceUnavailableException : Exception
{
    public ServiceUnavailableException() : base("The service is currently unavailable.")
    { }

    public ServiceUnavailableException(string message) : base(message)
    { }

    public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException)
    { }
}
