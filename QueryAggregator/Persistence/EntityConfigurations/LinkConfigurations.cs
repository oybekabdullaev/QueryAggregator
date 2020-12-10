using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Persistence.EntityConfigurations
{
    public class LinkConfigurations : EntityTypeConfiguration<Link>
    {
        public LinkConfigurations()
        {
            Property(l => l.Url)
                .IsRequired();

            Property(l => l.Title)
                .IsRequired();
        }
    }
}