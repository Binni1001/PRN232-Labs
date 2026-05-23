using Microsoft.AspNetCore.Mvc.Formatters;
using ContentNegotiationDemo.Models;
using System.Text;

namespace ContentNegotiationDemo.CustomFormatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(System.Text.Encoding.UTF8);
            SupportedEncodings.Add(System.Text.Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(IEnumerable<Blog>).IsAssignableFrom(type) ||
                typeof(Blog).IsAssignableFrom(type))
            {
                return true;
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Blog> blogs)
            {
                buffer.AppendLine("Name,Description");

                foreach (var blog in blogs)
                {
                    buffer.AppendLine($"{blog.Name},{blog.Description}");
                }
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, Blog blog)
        {
            buffer.AppendLine("Name,Description");
            buffer.AppendLine($"{blog.Name},{blog.Description}");
        }
    }
}
