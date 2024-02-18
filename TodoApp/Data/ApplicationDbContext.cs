using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApp.Models;

namespace TodoApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Status> Statuses {  get; set; }
    public DbSet<Zgloszenia> Zgloszenia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "All"},
            new Category { Id = 2, Name = "Work"},
            new Category { Id = 3, Name = "Home"},
            new Category { Id = 4, Name = "Exercise"},
            new Category { Id = 5, Name = "Shopping"},
            new Category { Id = 6, Name = "Contact"}
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "All"},
            new Status { Id = 2, Name = "Open"},
            new Status { Id = 3, Name = "Completed" }
        );
    }
    public class TodoApp : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Imie { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Nazwisko { get; set; }
    }



}