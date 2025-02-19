using System.Net;

namespace TechLibrary.Exception;

public class InvalidLoginException : TechLibraryException
{
    public override List<string> GetErrorMessages()
    {
        return new List<string>
        {
            "Invalid username or password."
        };
    }

    public override HttpStatusCode GetStatusCodes()
    {
        return HttpStatusCode.Unauthorized;
    }
}