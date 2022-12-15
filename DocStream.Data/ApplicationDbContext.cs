using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocStream.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DocStream.Data.Configuration.Entities;
using DocStream.FileEntities;

namespace DocStream.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantLegalStatus> ApplicantLegalStatuses { get; set; }
        public DbSet<Banker> Bankers { get; set; }
        public DbSet<ContactPerson> ContactPerple { get; set; }
        public DbSet<ProposedBusinessName> ProposedBusinessNames { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Shareholder> Shareholders { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicantConfiguration());
            builder.ApplyConfiguration(new ApplicantLegalStatusConfiguration());
            builder.ApplyConfiguration(new BankerConfiguration());
            builder.ApplyConfiguration(new ContactPersonConfiguration());
            builder.ApplyConfiguration(new DirectorConfiguration());
            builder.ApplyConfiguration(new ProposedBusinessNameConfiguration());
            builder.ApplyConfiguration(new ShareholderConfguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
