using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Common
{
    public class ReflectionFunctions
    {
        public static bool IsDerivedFrom(Type input, Type baseType)
        {
            Type current = input.BaseType;

            while (current != null)
            {
                if (current == baseType)
                    return true;

                if (current.IsGenericType && current.GetGenericTypeDefinition() == baseType)
                    return true;

                current = current.BaseType;
            }

            return false;
        }

        public static T GetPropertyValue<T>(object container, string property)
        {
            var prop = container.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public);

            if (prop == null)
                return default(T);

            return (T)Convert.ChangeType(prop.GetValue(container, null), typeof(T));
        }

        public static PropertyInfo GetPropertyInfo(Expression method)
        {
            var lambda = method as LambdaExpression;

            if (lambda == null)
                throw new ArgumentNullException("method");

            var member = lambda.Body as MemberExpression;

            if (member == null)
            {
                var unary = lambda.Body as UnaryExpression;
                member = unary.Operand as MemberExpression;
            }

            return member.Member as PropertyInfo;
        }

        public static IDictionary<string, string> GetPropertyValues(object input)
        {
            var values = new Dictionary<string, string>();

            if (input == null)
                return values;

            var type = input.GetType();

            object value = null;

            foreach (var prop in input.GetType().GetProperties())
            {
                if (!prop.CanRead)
                    continue;

                value = prop.GetValue(input, null) ?? "(null)";

                values.Add(prop.Name, value.ToString());
            }

            return values;
        }

        public static string ParseContent(string template, object input)
        {
            return ParseContent(template, input, "{(.*?)}", r => r);
        }

        public static string ParseContent(string template, object input, Func<string, string> postEvalFormatter)
        {
            return ParseContent(template, input, "{(.*?)}", postEvalFormatter);
        }

        public static string ParseContent(string template, object input, string placeHolderExpression)
        {
            return ParseContent(template, input, placeHolderExpression, r => r);
        }

        public static string ParseContent(string template, object input, string placeHolderExpression, Func<string, string> postEvalFormatter)
        {
            var builder = new StringBuilder();
            var index = 0;

            foreach (Match m in Regex.Matches(template, placeHolderExpression, RegexOptions.Compiled))
            {
                builder.Append(template.Substring(index, m.Index - index));
                builder.Append(postEvalFormatter(Evaluate(input, m.Value)));
                index = m.Index + m.Length;
            }

            if (index < template.Length)
                builder.Append(template.Substring(index));

            return builder.ToString();
        }

        public static string Evaluate(object input, string expression)
        {
            return Evaluate(input, expression, false, new char[] { '{', '}' });
        }

        public static string Evaluate(object input, string expression, bool throwOnError, char[] delimiters)
        {
            var trimmed = expression.Trim(delimiters);

            try
            {
                var result = DataBinder.Eval(input, trimmed) ?? string.Empty;

                return result.ToString();
            }
            catch
            {
                if (throwOnError)
                    throw;

                return expression;
            }
        }
    }
}
