using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcpserver.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace mcpserver.Model
{
    public class PGContext : DbContext
    {
        public PGContext(DbContextOptions<PGContext> options)
        : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ContractEntity> m_contracts { get; set; }
        public DbSet<LotContractEntity> m_lotcontract { get; set; }

    }
}