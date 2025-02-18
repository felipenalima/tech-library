using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infraestructure;

namespace TechLibrary.UseCases.Users.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {
        ValidateUser(request);
        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
        };

        var dbContext = new TechLibraryDbContext();
        dbContext.Users.Add(entity);
        dbContext.SaveChanges();
        
        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
        };
    }

    private void ValidateUser(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}