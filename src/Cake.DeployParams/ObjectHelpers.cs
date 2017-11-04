using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cake.DeployParams
{
    public static class ObjectHelpers
    {
        public static void Add<TParams>(this IDictionary<string, IList<Func<TParams, IDictionary<string, object>>>> dictionary, string name, string key, Func<TParams, object> parameters)
        {
            IList<Func<TParams, IDictionary<string, object>>> funcList;
            if (!dictionary.TryGetValue(key, out funcList))
            {
                funcList = new List<Func<TParams, IDictionary<string, object>>>();
                funcList.Add(_ => new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
        {
          {
            name,
            (object) key
          }
        });
            }
            funcList.Add(_ => parameters(_).ToDictionary());
            dictionary[key] = funcList;
        }

        public static IDictionary<string, object> ToDictionary(this object parameters)
        {
            PropertyInfo[] properties = parameters.GetType().GetProperties();
            Func<PropertyInfo, object> elementSelector = x => x.GetValue(parameters, null);
            StringComparer cultureIgnoreCase = StringComparer.InvariantCultureIgnoreCase;
            return ((IEnumerable<PropertyInfo>)properties).ToDictionary(x => x.Name, elementSelector, cultureIgnoreCase);
        }
    }
}
