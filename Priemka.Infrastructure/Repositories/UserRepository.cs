﻿using FluentResults;
using Microsoft.EntityFrameworkCore;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;

namespace Priemka.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public async Task<Result<UserEntity>> GetByEmail(Email email, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
            if (user is null)
                return Result.Fail($"Пользователь с такой почтой не найден: {email}");
            return Result.Ok(user);
        }
        public async Task<Result<UserEntity>> GetEmailAsString(string email, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken: cancellationToken);
            if (user is null)
                return Result.Fail($"Пользователь с такой почтой не найден: {email}");
            return Result.Ok(user);
        }
    }
}
