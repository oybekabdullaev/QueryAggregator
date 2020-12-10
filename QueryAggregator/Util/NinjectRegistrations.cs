﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using QueryAggregator.Core;
using QueryAggregator.Persistence;

namespace QueryAggregator.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>()
                .WithConstructorArgument("context", new QueryAggregatorContext());
        }
    }
}