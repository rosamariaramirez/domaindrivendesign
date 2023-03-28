using BubberDinner.Application.Common.Errors;
using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Interfaces.Persistence;
using BubberDinner.Domain.Common.Errors;
using BubberDinner.Domain.Entities;
using ErrorOr;

namespace BubberDinner.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthResult> Login(string email, string password)
        {
            // 1. Validate user does exists

            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 2. Validate the password is correct
            if (user.Password != password)
            {
                return new[] { Errors.Authentication.InvalidCredentials };
            }

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthResult(user, token);
        }

        public ErrorOr<AuthResult> Register(string firstName, string lastName, string email, string password)
        {
            // 1. Validate user does not exist

            if (_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // 2. Create user (generate unique ID) && persist to DB
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);

            // 3. Create JTW token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult(user, token);
        }
    }
}
