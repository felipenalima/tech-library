using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infraestructure.DataAccess;
using TechLibrary.Infraestructure.Security.Cryptography;
using TechLibrary.Infraestructure.Security.Tokens.Access;

namespace TechLibrary.UseCases.Users.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {
        var dbContext = new TechLibraryDbContext();
        
        ValidateUser(request, dbContext);
        
        var cryptography = new BCryptAlgorithm();
        
        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = cryptography.HashPassword(request.Password),
        };
        
        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        var tokenGenerator = new JwtTokenGenerator();
        
        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
            AccessToken = tokenGenerator.GenerateToken(entity),
        };
    }

    private void ValidateUser(RequestUserJson request, TechLibraryDbContext dbContext)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var checkEmail = dbContext.Users.Any(u => u.Email == request.Email);
        if (checkEmail)
        {
            result.Errors.Add(new ValidationFailure("Email", "Email already exists")); 
        }
        
        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}