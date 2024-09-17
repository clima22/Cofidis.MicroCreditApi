using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Cofidis.MicroCreditApi.WebApi
{
    public static class LogBuildReqResp
    {
        public static string BuildReqInput<T>(HttpContext context, T input)
        {
            var log = "Input:" + JsonSerializer.Serialize(input, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All)
            });
            return log;
        }
        public static string BuildResOutput<T>(HttpContext context, T output)
        {
            var log = "Output:" + JsonSerializer.Serialize(output, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All)
            });
            return log;
        }
    }
}
