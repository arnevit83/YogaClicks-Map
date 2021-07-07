using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExceptionHandling
{
    public interface IExceptionHandler
    {
        void Handle(Exception ex);
    }
}
