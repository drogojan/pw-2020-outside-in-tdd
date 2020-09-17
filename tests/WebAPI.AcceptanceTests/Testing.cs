using System;
using System.Threading.Tasks;
using OpenChat.Application.Common.Interfaces;
using OpenChat.Domain.Entities;
using OpenChat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace WebAPI.AcceptanceTests
{
    public class Testing
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IServiceScopeFactory ScopeFactory { get; set; }
        public static Checkpoint Checkpoint { get; set; }

        static Testing()
        {
            Checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
        }

        public static void EnsureDatabase()
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.Migrate();
        }

        public static async Task ResetState()
        {
            await Checkpoint.Reset(Configuration.GetConnectionString("DefaultConnection"));
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            await context.AddAsync(entity);

            await context.SaveChangesAsync();
        }
    }
}