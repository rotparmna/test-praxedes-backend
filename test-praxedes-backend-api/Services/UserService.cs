using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Data.SqlClient;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Exceptions;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Services
{
    public class UserService : IUserService
	{
        private const string MESSAGE_ERROR = "Se intentó crear un usuario con un numero de documento existente, número de documento ";
        private const string MESSAGE_FATAL_ERROR = "Error inesperado en la creación del usuario, número de documento ";
        private readonly ISpGetUserById spGetUserById;
        private readonly ISpGetUsers spGetUsers;
        private readonly ISpInsertUser spInsertUser;
        private readonly ISpDeleteUser spDeleteUser;
        private readonly ISpUpdateUser spUpdateUser;
        private readonly ILogger<UserService> logger;
        private readonly IValidator<User> validator;

        public UserService(ISpGetUserById spGetUserById,
            ISpGetUsers spGetUsers,
            ISpInsertUser spInsertUser,
            ISpDeleteUser spDeleteUser,
            ISpUpdateUser spUpdateUser,
            ILogger<UserService> logger,
            IValidator<User> validator)
		{
            this.spGetUserById = spGetUserById;
            this.spGetUsers = spGetUsers;
            this.spInsertUser = spInsertUser;
            this.spDeleteUser = spDeleteUser;
            this.spUpdateUser = spUpdateUser;
            this.logger = logger;
            this.validator = validator;
        }

        public async Task<bool> CreateUser(User user)
        {
            var validationResult = await validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                var exception = new ValidatorException("Have a problem with input validations.");
                validationResult.Errors.ForEach(x => exception.DataError.Add(x.ErrorMessage));
                throw exception;
            }
            try
            {
                await spInsertUser.Execute(user);
                return true;
            }
            catch(UniqueConstraintException ex)
            {
                logger.LogError(ex, MESSAGE_ERROR + user.DocumentNumber);
                return false;
            }
            catch(SqlException sqlEx)
            {
                logger.LogError(sqlEx, MESSAGE_FATAL_ERROR + user.DocumentNumber);
                return false;
            }
        }

        public async Task<User> GetUserById(int idUser)
        {
            return await spGetUserById.Execute(idUser);
        }

        public async Task<List<User>> GetUsers()
        {
            return await spGetUsers.Execute();
        }

        public async Task RemoveUser(int idUser)
        {
            await spDeleteUser.Execute(idUser);
        }

        public async Task UpdateUser(User user)
        {
            await spUpdateUser.Execute(user);
        }
    }
}

