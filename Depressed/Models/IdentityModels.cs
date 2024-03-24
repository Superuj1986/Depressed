using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Depressed.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Depressed.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public string Age { get; set; }
        public string Main_subject { get; set; }
        public string Address { get; set; }
        public DateTime? BirtDate { get; set; }
        public byte[] Image { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Khoahoc> Khoahocs { get; set; }
        public DbSet<Lophoc> Lophocs { get; set; }
        public DbSet<RollOut> RollOuts { get; set; }
        public DbSet<Thongbao> Thongbaos { get; set; }
        public DbSet<ClassMember> ClassMembers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lichhoc> Lichhocs {  get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext,Configuration>());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}