using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Function
{
    public static class Graphql
    {
        [FunctionName("Graphql")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var server = new Server();
            var query = req.Query["query"];

            var result = await server.QueryAsync(query);
            //var json = new DocumentWriter(indent: true).Write(result);

            return new OkObjectResult(result);
        }
    }
}
