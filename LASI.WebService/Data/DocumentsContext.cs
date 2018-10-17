using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LASI.WebService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LASI.WebService.Data
{
    public class DocumentsContext : DbContext
    {
        public DocumentsContext(DbContextOptions<DocumentsContext> options)
            : base(options)
        {
        }

        public DocumentsContext(string connectionString) : base(new DbContextOptionsBuilder<DocumentsContext>().UseSqlServer(connectionString).Options) { }

        public DbSet<UploadDocument> Uploads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<UploadDocument>()
                .HasOne(e => e.User)
                .WithMany(u => u.Documents)
                .HasForeignKey("UserId")
                .IsRequired();

            modelBuilder
                .Entity<UploadDocument>()
                .ToTable("Uploads")
                .HasQueryFilter(x => !x.Deleted)
                .HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot)
                .HasQueryFilter(e => !e.Deleted)
                .HasAlternateKey(e => e.Name)
                .HasName(nameof(UploadDocument.Name));
        }
    }
}
