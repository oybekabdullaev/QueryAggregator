using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var httpClient = HttpClientService.Instance;

            Bind<IApiHelper>().To<GoogleApiHelper>()
                .WithConstructorArgument("httpClient", httpClient);
            Bind<IApiHelper>().To<BingApiHelper>()
                .WithConstructorArgument("httpClient", httpClient);
            //Bind<IApiHelper>().To<YandexApiHelper>()
            //    .WithConstructorArgument("httpClient", httpClient);

            Bind<IUnitOfWork>().To<UnitOfWork>()
                .WithConstructorArgument("context", new QueryAggregatorContext());

            Unbind<ModelValidatorProvider>();
        }
    }
}