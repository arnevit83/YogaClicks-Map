using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Data.Repositories;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Ninject.Factories;
using Clicks.Yoga.Requests;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Clicks.Yoga.Data;
using Clicks.Yoga.Portal.Context;

namespace Clicks.Yoga.Import.ImageStoreAzure
{
    public class YogaNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<YogaDataContext>()
                .ToSelf()
                .InSingletonScope();

            Bind<IEntityContext, IWorkUnit>()
                .To<YogaEntityContext>()
                .InSingletonScope();

            Bind<ISearchEngine>()
                .ToConstant<ISearchEngine>(null)
                .InSingletonScope();

            Bind<ISecurityContext>()
                .ToConstant<ISecurityContext>(null)
                .InSingletonScope();

            Bind<IImageStore>()
                .To<ImageStore>()
                .InSingletonScope();

            Bind<IRequestCompletionHandlerFactory>()
                .ToFactory(() => new TypeDelegateFactoryInstanceProvider<RequestType>(a => a.GetCompletionHandlerType()));

            Bind(typeof(IRepository<>))
                .To(typeof(PrincipalRepository<>))
                .When(r => typeof(PrincipalEntity).IsAssignableFrom(r.Service.GetGenericArguments().First()))
                .InSingletonScope();

            Bind(typeof(IRepository<>))
                .To(typeof(Repository<>))
                .InSingletonScope();

            Kernel.Bind(x => x
                .FromAssemblyContaining(typeof(Repository<>))
                .SelectAllClasses()
                .InheritedFrom(typeof(Repository<>))
                .WhichAreNotGeneric()
                .BindDefaultInterface()
                .Configure(b => b.InSingletonScope())
            );

            Kernel.Bind(x => x
                .FromAssemblyContaining(typeof(ServiceBase))
                .SelectAllClasses()
                .InheritedFrom(typeof(ServiceBase))
                .BindDefaultInterface()
                .Configure(b => b.InSingletonScope())
            );
        }
    }
}