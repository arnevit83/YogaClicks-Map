using System;
using System.Collections.Generic;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Syntax;

namespace Clicks.Yoga.Ninject.Factories
{
    public class TypeDelegateFactoryBindingGenerator<TService> : IBindingGenerator
    {
        public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(Type type, IBindingRoot bindingRoot)
        {
            if (type != null && !type.IsAbstract && type.IsClass && typeof(TService).IsAssignableFrom(type))
            {
                yield return bindingRoot.Bind(typeof(TService))
                    .To(type)
                    .Named(type.FullName) as IBindingWhenInNamedWithOrOnSyntax<object>;
            }
        }
    }
}