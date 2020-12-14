using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            Bind<HttpClient>().ToSelf().InSingletonScope();

            Bind<IApiHelper>().To<GoogleApiHelper>();
            Bind<IApiHelper>().To<BingApiHelper>();
            //Bind<IApiHelper>().To<YandexApiHelper>();

            Bind<IUnitOfWork>().To<UnitOfWork>()
                .InSingletonScope()
                .WithConstructorArgument("context", new QueryAggregatorContext());

            Unbind<ModelValidatorProvider>();
        }
    }
}