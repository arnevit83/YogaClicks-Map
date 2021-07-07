using System;
using System.Linq;
using System.Reflection;
using Ninject.Extensions.Factory;
using Ninject.Parameters;

namespace Clicks.Yoga.Ninject.Factories
{
    public class TypeDelegateFactoryInstanceProvider<TArgument> : StandardInstanceProvider where TArgument : class
    {
        public TypeDelegateFactoryInstanceProvider(Func<TArgument, Type> typeFunction)
        {
            TypeFunction = typeFunction;
        }

        private Func<TArgument, Type> TypeFunction { get; set; }

        protected override string GetName(MethodInfo methodInfo, object[] arguments)
        {
            var argument = arguments[0] as TArgument;

            if (argument == null) throw new ArgumentException(string.Format("Expected instance of {0}.", typeof(TArgument).FullName));

            var type = TypeFunction(argument);

            if (type == null) throw new Exception("Type delegate returned null.");

            return type.FullName;
        }

        protected override IConstructorArgument[] GetConstructorArguments(MethodInfo methodInfo, object[] arguments)
        {
            return base.GetConstructorArguments(methodInfo, arguments).Skip(1).ToArray();
        }
    }
}