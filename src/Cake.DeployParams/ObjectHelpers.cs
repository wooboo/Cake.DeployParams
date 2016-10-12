namespace Cake.DeployParams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectHelpers
    {
        public static void Add(
            this IDictionary<string, IList<Func<dynamic, IDictionary<string, object>>>> dictionary, 
            string name, 
            string key, 
            Func<dynamic, object> parameters)
        {
            IList<Func<dynamic, IDictionary<string, object>>> item;
            if (!dictionary.TryGetValue(key, out item))
            {
                item = new List<Func<dynamic, IDictionary<string, object>>>();
                item.Add(_ => new Dictionary<string, object> { { name, key } });
            }

            item.Add(_ => ((object)parameters(_)).ToDictionary());

            dictionary[key] = item;
        }

        public static IDictionary<string, object> ToDictionary(this object parameters)
        {
            return parameters.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(parameters, null));
        }
    }
}