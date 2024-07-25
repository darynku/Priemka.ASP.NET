using FluentResults;
using MediatR;
using Priemka.Application.DataAccess;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;

namespace Priemka.Application.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            if (email.IsFailed) return email.ToResult();

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = UserEntity.CreateApplicationUser(email.Value, passwordHash);
            if (user.IsFailed) return user.ToResult();

            await _userRepository.AddAsync(user.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
