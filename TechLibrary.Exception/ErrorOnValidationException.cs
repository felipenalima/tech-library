using System.Net;

namespace TechLibrary.Exception;

public class ErrorOnValidationException : TechLibraryException
{
    private readonly List<string> _errors;

    public ErrorOnValidationException(List<string> errorsMessages)
    {
        _errors = errorsMessages;
    }

    public override List<string> GetErrorMessages()
    {
        return _errors;
    }

    public override HttpStatusCode GetStatusCodes()
    {
        return HttpStatusCode.BadRequest;
    }
}