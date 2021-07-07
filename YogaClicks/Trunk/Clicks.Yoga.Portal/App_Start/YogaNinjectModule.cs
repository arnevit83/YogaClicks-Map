using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Data;
using Clicks.Yoga.Data.Repositories;
using Clicks.Yoga.Data.Search;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.MailingLists;
using Clicks.Yoga.MailingLists.MailChimp;
using Clicks.Yoga.Media;
using Clicks.Yoga.Ninject.Factories;
using Clicks.Yoga.Permissions;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Validation;
using Clicks.Yoga.Requests;
using FluentValidation;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Clicks.Yoga.Portal
{
    public class YogaNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<YogaDataContext>()
                .ToSelf()
                .InRequestScope();

            Bind<IEntityContext, IWorkUnit>()
                .To<YogaEntityContext>()
                .InRequestScope();

            Bind<IMailingListProvider>()
                .To<MailChimpMailingListProvider>()
                .InRequestScope();

            Bind<ISearchEngine>()
                .To<ElasticSearchEngine>()
                .InRequestScope();

            Bind<ISecurityContext>()
                .To<SecurityContext>()
                .InRequestScope();

            Bind<IImageStore>()
                .To<ImageStore>()
                .InRequestScope();

            Bind(typeof(IRepository<>))
                .To(typeof(PrincipalRepository<>))
                .When(r => typeof(PrincipalEntity).IsAssignableFrom(r.Service.GetGenericArguments().First()))
                .InRequestScope();

            Bind(typeof(IRepository<>))
                .To(typeof(Repository<>))
                .InRequestScope();

            Kernel.Bind(x => x
                .FromAssemblyContaining(typeof(Repository<>))
                .SelectAllClasses()
                .InheritedFrom(typeof(Repository<>))
                .WhichAreNotGeneric()
                .BindDefaultInterface()
                .Configure(b => b.InRequestScope())
            );

            Kernel.Bind(x => x
                .FromAssemblyContaining(typeof(ServiceBase))
                .SelectAllClasses()
                .InheritedFrom(typeof(ServiceBase))
                .BindDefaultInterface()
                .Configure(b => b.InRequestScope())
            );

            Kernel.Bind(x => x
                .FromAssemblyContaining<ValidatorFactory>()
                .SelectAllClasses()
                .InheritedFrom(typeof(AbstractValidator<>))
                .WhichAreNotGeneric()
                .BindDefaultInterfaces()
                .Configure(b => b.InSingletonScope())
            );

            Bind<IRequestCompletionHandlerFactory>()
                .ToFactory(() => new TypeDelegateFactoryInstanceProvider<RequestType>(a => a.GetCompletionHandlerType()));

            Kernel.Bind(x => x
                .FromAssemblyContaining<IRequestCompletionHandler>()
                .SelectAllClasses()
                .InheritedFrom<IRequestCompletionHandler>()
                .BindWith(new TypeDelegateFactoryBindingGenerator<IRequestCompletionHandler>()));

            Bind<IWallPermissionProviderFactory>()
                .ToFactory(() => new TypeDelegateFactoryInstanceProvider<Wall>(a => a.GetWallPermissionProviderType()));

            Kernel.Bind(x => x
                .FromAssemblyContaining<IWallPermissionProvider>()
                .SelectAllClasses()
                .InheritedFrom<IWallPermissionProvider>()
                .BindWith(new TypeDelegateFactoryBindingGenerator<IWallPermissionProvider>()));

            Bind<ICommentPermissionProviderFactory>()
                .ToFactory(() => new TypeDelegateFactoryInstanceProvider<ICommentable>(a => a.GetCommentPermissionProviderType()));

            Kernel.Bind(x => x
                .FromAssemblyContaining<ICommentPermissionProvider>()
                .SelectAllClasses()
                .InheritedFrom<ICommentPermissionProvider>()
                .BindWith(new TypeDelegateFactoryBindingGenerator<ICommentPermissionProvider>()));

            Bind<IGalleryPermissionProviderFactory>()
                .ToFactory(() => new TypeDelegateFactoryInstanceProvider<IGalleryAssociate>(a => a.GetGalleryAssociatePermissionProviderType()));

            Kernel.Bind(x => x
                .FromAssemblyContaining<IGalleryPermissionProvider>()
                .SelectAllClasses()
                .InheritedFrom<IGalleryPermissionProvider>()
                .BindWith(new TypeDelegateFactoryBindingGenerator<IGalleryPermissionProvider>()));

            Bind<IMediaResourceScannerFactory>()
                .ToFactory(() => new TypeDelegateFactoryInstanceProvider<MediaResourceType>(a => a.GetScannerType()));

            Kernel.Bind(x => x
                .FromAssemblyContaining<IMediaResourceScanner>()
                .SelectAllClasses()
                .InheritedFrom<IMediaResourceScanner>()
                .BindWith(new TypeDelegateFactoryBindingGenerator<IMediaResourceScanner>()));
        }
    }
}