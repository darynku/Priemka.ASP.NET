using FluentResults;
using MediatR;
using Priemka.Domain.Interfaces;

namespace Priemka.Application.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginRespones>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<LoginRespones>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetEmailAsString(request.Email, cancellationToken);
            if (user.IsFailed) return user.ToResult();

            var passwordVerify = BCrypt.Net.BCrypt.Verify(request.Password, user.Value.PasswordHash);
            if (passwordVerify == false)
                return Result.Fail("Неверный пароль");

            var token = _jwtProvider.Generate(user.Value);
            if (token.IsFailed) return token.ToResult();

            var respones = new LoginRespones(token.Value, user.Value.Role.Name);

            return respones;
        }
    }
}
