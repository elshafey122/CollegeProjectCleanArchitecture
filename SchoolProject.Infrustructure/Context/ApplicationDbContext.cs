﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Entities.Views;
using System.Reflection;

namespace SchoolProject.Infrustructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Departement> departements { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<DepartementSubject> departementSubjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }
        public DbSet<InstructorSubject> instructorSubjects { get; set; }
        public DbSet<UserRefreshToken> userRefreshTokens { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }

        #region Views
        public DbSet<ViewDepartStudentCount> ViewDepartStudentCount { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
