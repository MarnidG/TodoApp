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
            new Category { Id = 1, Name = "Wszystkie"},
            new Category { Id = 2, Name = "Praca"},
            new Category { Id = 3, Name = "Dom"},
            new Category { Id = 4, Name = "Cwiczenia"},
            new Category { Id = 5, Name = "Zakupy"},
            new Category { Id = 6, Name = "Kontakty"}
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "Wszystko"},
            new Status { Id = 2, Name = "Otwarte"},
            new Status { Id = 3, Name = "Ukonczone" }
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