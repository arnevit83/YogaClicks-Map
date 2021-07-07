using System;
using System.Web.Mvc;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return DependencyResolver.Current.GetService(validatorType) as IValidator;
        }
    }
}