using System;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Common.ExceptionHandling.Custom.Aspnet
{
    public class HtmlExceptionRenderer
    {
        public string Render(Exception ex)
        {
            var str = new StringBuilder();
            var context = HttpContext.Current;
            var inner = ex.InnerException;

            str.AppendLine("<html><body style=\"font-family: Calibri; font-size: 11pt; color: #000;\">");

            str = RenderMessage(str, ex);
            str = RenderDetails(str, ex);
            str = RenderStackTrace(str, ex);

            while (inner != null)
            {
                str = RenderInnerException(str, inner);
                inner = inner.InnerException;
            }

            if (context != null)
            {
                str = RenderSession(str, ex, context);
                str = RenderForm(str, ex, context);
                str = RenderQueryString(str, ex, context);
                str = RenderServerVariables(str, ex, context);
            }

            str.AppendLine("</body></html>");

            return str.ToString();
        }

        private StringBuilder RenderMessage(StringBuilder str, Exception ex)
        {
            return str.AppendLine("<h2>" + HttpUtility.HtmlEncode(ex.Message) + "</h2>");
        }

        private StringBuilder RenderDetails(StringBuilder str, Exception ex)
        {
            str.AppendLine("<br />");
            str.AppendLine("<strong>Server Timestamp:</strong> " + DateTime.Now.ToString() + "<br />");
            str.AppendLine("<strong>Exception Type:</strong> " + ex.GetType().FullName + "<br />");
            str.AppendLine("<br />");
            str.AppendLine("<strong>Target Site:</strong> " + ex.TargetSite.Name + "<br />");

            return str;
        }

        private StringBuilder RenderStackTrace(StringBuilder str, Exception ex)
        {
            str.AppendLine("<strong>Stack Trace:</strong><br />");
            str.AppendLine(ex.StackTrace.Replace(Environment.NewLine, "<br />"));

            return str;
        }

        private StringBuilder RenderInnerException(StringBuilder str, Exception ex)
        {
            str.AppendLine("<br />");
            str.AppendLine("<strong>Message:</strong> " + ex.Message + "<br />");
            str.AppendLine("<strong>Type:</strong>" + ex.GetType().FullName + "<br />");
            if (ex.StackTrace != null)
            {
                str.AppendLine("<strong>Stack Trace:</strong><br />");
                str.AppendLine(ex.StackTrace.Replace(Environment.NewLine, "<br />"));
            }

            return str;
        }

        private StringBuilder RenderSession(StringBuilder str, Exception ex, HttpContext context)
        {
            if (context.Session == null)
                return str;

            object value = null;

            str.AppendLine("<br /><br /><strong>Session Contents</strong><br />");

            foreach (var key in context.Session.Keys)
            {
                value = context.Session[key.ToString()] ?? "(null)";
                str.AppendLine("<strong>" + key.ToString() + ":</strong> " + HttpUtility.HtmlEncode(value.ToString()) + "<br />");
            }

            return str;
        }

        private StringBuilder RenderForm(StringBuilder str, Exception ex, HttpContext context)
        {
            string value;

            str.AppendLine("<br /><br /><strong>Form Variables</strong><br />");

            try
            {
                foreach (var key in context.Request.Form.AllKeys)
                {
                    try
                    {
                        value = HttpUtility.HtmlEncode(context.Request.Form[key]);
                    }
                    catch (Exception x)
                    {
                        value = HttpUtility.HtmlEncode(x.Message);
                    }

                    str.AppendLine("<strong>" + key + ":</strong> " + value + "<br />");
                }
            }
            catch (Exception z)
            {
                str.AppendLine(HttpUtility.HtmlEncode(z.Message));
            }

            return str;
        }

        private StringBuilder RenderQueryString(StringBuilder str, Exception ex, HttpContext context)
        {
            string value;

            str.AppendLine("<br /><br /><strong>QueryString Parameters</strong><br />");

            try
            {
                foreach (var key in context.Request.QueryString.AllKeys)
                {
                    try
                    {
                        value = HttpUtility.HtmlEncode(context.Request.QueryString[key]);
                    }
                    catch (Exception x)
                    {
                        value = HttpUtility.HtmlEncode(x.Message);
                    }

                    str.AppendLine("<strong>" + key + ":</strong> " + value + "<br />");
                }
            }
            catch (Exception z)
            {
                str.AppendLine(HttpUtility.HtmlEncode(z.Message));
            }

            return str;
        }

        private StringBuilder RenderServerVariables(StringBuilder str, Exception ex, HttpContext context)
        {
            string value;

            str.AppendLine("<br /><br /><strong>Server Variables</strong><br />");

            try
            {
                foreach (var key in context.Request.ServerVariables.AllKeys)
                {
                    try
                    {
                        value = HttpUtility.HtmlEncode(context.Request.ServerVariables[key]);
                    }
                    catch (Exception x)
                    {
                        value = HttpUtility.HtmlEncode(x.Message);
                    }

                    str.AppendLine("<strong>" + key + ":</strong> " + value + "<br />");
                }
            }
            catch (Exception z)
            {
                str.AppendLine(HttpUtility.HtmlEncode(z.Message));
            }

            return str;
        }
    }
}