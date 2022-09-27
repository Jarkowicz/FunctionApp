using System.Collections.Generic;
using GraphQL;
using System.Linq;

namespace Function;

public class Query
{
    [GraphQLMetadata("jedis")]
    public IEnumerable<Jedi> GetJedis()
    {
        return StarWarsDB.GetJedis();
    }

    [GraphQLMetadata("jedi")]
    public Jedi GetJedi(int id)
    {
        return StarWarsDB.GetJedis().SingleOrDefault(j => j.Id == id);
    }

    [GraphQLMetadata("hello")]
    public string GetHello()
    {
        return "World";
    }
}