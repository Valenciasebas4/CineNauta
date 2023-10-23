﻿using Cine_Nauta.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cine_Nauta.DAL
{
    public class DataBaseContext : DbContext
    {

        /*Constructor*/
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        /*Mapeando la entidad para la Tabla*/
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }


        /*Indicies para las tablas*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /* Se usa para validar que el nombre sea Unico*/
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Name", "CountryId").IsUnique(); // Para estos casos, debo crear un índice Compuesto
            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique(); // Para estos casos, debo crear un índice Compuesto
           

        }
    }
}
