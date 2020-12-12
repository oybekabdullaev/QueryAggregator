using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Web.Common;
using QueryAggregator.Apis;
using QueryAggregator.Core;
using QueryAggregator.Persistence;

namespace QueryAggregator.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            var httpClient = ApiHelper.HttpClient;

            Bind<IApi>().To<GoogleApi>()
                .WithConstructorArgument("httpClient", httpClient);
            Bind<IApi>().To<BingApi>()
                .WithConstructorArgument("httpClient", httpClient);

            Bind<IUnitOfWork>().To<UnitOfWork>()
                .WithConstructorArgument("context", new QueryAggregatorContext());
        }
    }
}