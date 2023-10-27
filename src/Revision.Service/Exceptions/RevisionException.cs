namespace Revision.Service.Exceptions;

public class RevisionException : Exception
{
    public int StatusCode { get; set; }
    public RevisionException(int statusCode = 500, string message = "Something went wrong") : base(message)
    {
        StatusCode = statusCode;
    }
}
