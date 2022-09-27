using GraphQL;
using GraphQL.Types;
 using Newtonsoft.Json;
//using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using GraphQL.Execution;

namespace Function;

public class Server
{
    private ISchema _schema { get; set; }
    public Server()
    {
        _schema = Schema.For(@"
          type Jedi {
            id: ID
            name: String
            side: String
          }

          input JediInput {
            name: String
            side: String
            id: ID
          }

          type Mutation {
            addJedi(input: JediInput): Jedi
            updateJedi(input: JediInput ): Jedi
            removeJedi(id: ID): String
          }

          type Query {
              jedis: [Jedi]
              jedi(id: ID): Jedi
              hello: String
          }
        ", _ => 
        {
            _.Types.Include<Query>();
            _.Types.Include<Mutation>();
        }
        );
    }

    public async Task<string> QueryAsync(string query) 
    {
      var result = await new DocumentExecuter().ExecuteAsync(_ =>
      {
        _.Schema = _schema;
        _.Query = query;
      }).ConfigureAwait(false);

      if(result.Errors != null) 
      {
        return result.Errors[0].Message;
      } 
      else 
      {
        try
        {
            //return JsonSerializer.Serialize(result.Data);
            //var json = new DocumetnWriter
            return JsonConvert.SerializeObject(((RootExecutionNode)result.Data).SubFields[0].Result, Formatting.Indented,
                        new JsonSerializerSettings()
                        { 
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
        }
        catch (System.Exception e)
        {
            Debug.WriteLine(e.Message);
            throw;
        }
      }
    }
}