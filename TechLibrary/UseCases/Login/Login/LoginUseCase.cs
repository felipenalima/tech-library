using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;
using TechLibrary.Infraestructure.DataAccess;
using TechLibrary.Infraestructure.Security.Cryptography;
using TechLibrary.Infraestructure.Security.Tokens.Access;

namespace TechLibrary.UseCases.Login.Login;

public class LoginUseCase
{
    public ResponseRegisteredUserJson Execute(RequestLoginJson request)
    {
        var dbContext = new TechLibraryDbContext();

        var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
        if (user == null)
        {
            throw new InvalidLoginException();
        }

        var cryptography = new BCryptAlgorithm();
        var passwordIsValid = cryptography.VerifyHashedPassword(user.Password, request.Password);
        if (!passwordIsValid)
        {
            throw new InvalidLoginException();
        }

        var tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            AccessToken = tokenGenerator.GenerateToken(user),
        };
    }
}