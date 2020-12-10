using System.Collections.Generic;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QueryAggregator.Persistence.QueryAggregatorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QueryAggregator.Persistence.QueryAggregatorContext context)
        {
            #region AddQueryWithLinks
            var links = new List<Link>
            {
                new Link
                {
                    Url = "https://www.google.com/appserve/mkt/p/AHANi0Ymd0RfzBGyO52RXE-KqP501v_BoLYCmbEVu3ed4v20GWE73BA0QLKqb5qcBz9DoR95Q01h2oVbSvso5oD-8cb13H7LBY4I0rt03WuEzRSGdt8aq7XNSRjSPMLNWnMvV_1GXgu8JzMgWjK_RBzqLw_Pt7yg2L3TuZb561k0gINDvxroqztwNXjPpSX-PebKodACIQICR-n5-17gWvGpDOBuR3fqxN0",
                    Title = "Kristen Bell, Dax Shepherd launch natural baby line Hello Bello at ...",
                    Description = "Kristen Bell, Dax Shepherd debut Hello Bello at Walmart. Anne Stych, \nContributing Writer Feb 26, 2019, 8:41am EST. Kristen Bell, Dax Shepherd \nlaunch natural ..."
                },
                new Link
                {
                    Url = "https://www.google.com/appserve/mkt/p/AD-FnExF1M88MvL4fLlelHZH0jN9ErveHb6bYtrTsRJRjPXiqNOOdDMhalyRQu3WQ20z6zKlqmZ607JMHMmoJ4Q2gn_ntNbcQR_EQtgjV6z3UO-5onCI10yTGLGEZNTr-QLDprXUeftRsET7IbGMtnEFATk4PUoffdQMxQyRsFN6UhYEUSD-KWVIUkRhww5qiovHwG6uxxeqGpF9N6RrgmA",
                    Title = "Meghan Markle upgraded engagement ring from Prince Harry - see ...",
                    Description = "Jun 4, 2020 ... HELLO! confirmed in honour of their first wedding anniversary on 19 May, Prince \nHarry gifted his wife with a stunning new eternity ring. Meghan ..."
                },
                new Link
                {
                    Url = "https://www.google.com/appserve/mkt/p/AM7kBiUTlfb3agFMUBaRYBQDaFAhN4JTDjYMF_FSbu5oEp8VRcTbtKJelnthsKlNyrPIkEJAw_0LojZuw8jmUAH7HzkZcQFh4iot77elMs922SPvFtQaVzqWrXhdJv3Fts9TSBwodjsP_2zzNa5YJR-ANda5V-yuHBPuCqxBwYU6HVUU7xBvuuoltLvvApodMlQklb1KMH1Wtg",
                    Title = "Inside Your Home Made Perfect star Angela Scanlon's stylish ...",
                    Description = "Apr 7, 2020 ... ... extra storage and to display more personal mementos. © HELLO! Total or \npartial reproduction of this article and its photographs is prohibited, ..."
                }
            };

            var query = new Query
            {
                QueryString = "hello",
                Links = links
            };

            context.Queries.AddOrUpdate(query);
            #endregion
        }
    }
}
