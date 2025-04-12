using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=MvcProject;integrated security=true;MultipleActiveResultSets=True");
        }

        public DbSet<About> Abouts
        {
            get; set;
        }
        public DbSet<Category> Categories
        {
            get; set;
        }
        public DbSet<Content> Contents
        {
            get; set;
        }
        public DbSet<Heading> Headings
        {
            get; set;
        }
        public DbSet<Writer> Writers
        {
            get; set;
        }
        public DbSet<Communication> Communications
        {
            get; set;
        }
        public DbSet<Message> Messages 
        {
            get; set;
        }
        public DbSet<ImageFile> ImageFiles 
        {
            get; set;
        }
        public DbSet<Admin> Admins
        {
            get; set;
        }
    }
}
