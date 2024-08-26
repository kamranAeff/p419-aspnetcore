using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace ConsoleAppElasticSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            var elasticUri = new Uri("http://localhost:9200");


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();

            Log.Error("Error occured: {Exception}", new Exception("any error handled"));

            Console.ReadKey();
        }
    }
}
