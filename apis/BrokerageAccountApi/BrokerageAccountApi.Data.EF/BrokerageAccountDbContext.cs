using BrokerageAccountApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Data.EF
{
    public class BrokerageAccountDbContext : DbContext
    {


        public BrokerageAccountDbContext(DbContextOptions<BrokerageAccountDbContext> options) : base(options)
        {

        }

        public DbSet<State> States { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ConfigureState(modelBuilder);

        }



        private void ConfigureState(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .ToTable("States")
                .HasKey(t => t.StateCode);

        }

    }
}
