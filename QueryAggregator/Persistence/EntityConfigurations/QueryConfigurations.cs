using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Persistence.EntityConfigurations
{
    public class QueryConfigurations : EntityTypeConfiguration<Query>
    {
        public QueryConfigurations()
        {
            Property(q => q.QueryString)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}