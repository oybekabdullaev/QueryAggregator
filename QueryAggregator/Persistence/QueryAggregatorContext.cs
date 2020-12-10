using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using QueryAggregator.Core.Domain;
using QueryAggregator.Persistence.EntityConfigurations;

namespace QueryAggregator.Persistence
{
    public class QueryAggregatorContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new QueryConfigurations());
            modelBuilder.Configurations.Add(new LinkConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}