﻿using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
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
        // add encrypt interface
        private readonly IEncryptionProvider _encryptionProvider;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // assign code for encryption
            _encryptionProvider = new GenerateEncryptionProvider("ac9e2a23fd114722939c67cd9d415192");

            // check if migrations not applied and database not created yet
            try{
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator!=null)
                {
                    if(!databaseCreator.CanConnect())
                    {
                        databaseCreator.Create();            // create database
                    }
                    if(!databaseCreator.HasTables())
                    {
                        databaseCreator.CreateTables();      // create tables
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            // using encrypt
            modelBuilder.UseEncryption(_encryptionProvider);
        }
    }
}
