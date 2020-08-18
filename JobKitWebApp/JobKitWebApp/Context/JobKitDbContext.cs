using JobKitWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Context
{
    public class JobKitDbContext : DbContext
    {
        public JobKitDbContext() : base("JobKitDb")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // remove the convention of cascade delete on from 'one to many' and 'many to many' relationships
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DbSet<FreelancerCategory> FreelancerCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ApplyJob> ApplyJobs { get; set; }
        public DbSet<FreelancerFeedback> FreelancerFeedbacks { get; set; }
        public DbSet<UserFeedback> UserFeedbacks { get; set; }
        public DbSet<ConnectPricing> ConnectPricings { get; set; }
        public DbSet<Connect> Connects { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ApplyJobConversation> ApplyJobConversations { get; set; }


    }
}