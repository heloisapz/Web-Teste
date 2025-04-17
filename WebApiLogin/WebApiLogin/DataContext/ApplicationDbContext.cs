using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiLogin.Models;


namespace WebAPILogin.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}