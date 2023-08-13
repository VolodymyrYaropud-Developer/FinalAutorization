using FinalAutorization.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalAutorization.Context
{
    public class UsersDBContext: IdentityDbContext<User>
    {
        public UsersDBContext(DbContextOptions<UsersDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        DbSet<User> users { get;set ; }
    }
    public class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }


}
