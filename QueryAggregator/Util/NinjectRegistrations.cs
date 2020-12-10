using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Web.Common;
using QueryAggregator.Core;
using QueryAggregator.Persistence;

namespace QueryAggregator.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>()
                .InRequestScope()
                .WithConstructorArgument("context", new QueryAggregatorContext());
        }
    }
}