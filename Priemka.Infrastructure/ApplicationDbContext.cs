﻿using Microsoft.EntityFrameworkCore;
using Priemka.Domain.Entities;
using Priemka.Infrastructure.Configurations;
namespace Priemka.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors => Set<Doctor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly,
                type => type.FullName?.Contains("Configurations") ?? false);
        }
    }
}
