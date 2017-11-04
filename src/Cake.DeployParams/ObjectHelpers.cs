// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.ObjectHelpers
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

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
        funcList = (IList<Func<TParams, IDictionary<string, object>>>) new List<Func<TParams, IDictionary<string, object>>>();
        funcList.Add((Func<TParams, IDictionary<string, object>>) (_ => (IDictionary<string, object>) new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase)
        {
          {
            name,
            (object) key
          }
        }));
      }
      funcList.Add((Func<TParams, IDictionary<string, object>>) (_ => parameters(_).ToDictionary()));
      dictionary[key] = funcList;
    }

    public static IDictionary<string, object> ToDictionary(this object parameters)
    {
      PropertyInfo[] properties = parameters.GetType().GetProperties();
      Func<PropertyInfo, object> elementSelector = (Func<PropertyInfo, object>) (x => x.GetValue(parameters, (object[]) null));
      StringComparer cultureIgnoreCase = StringComparer.InvariantCultureIgnoreCase;
      return (IDictionary<string, object>) ((IEnumerable<PropertyInfo>) properties).ToDictionary<PropertyInfo, string, object>((Func<PropertyInfo, string>) (x => x.Name), elementSelector, (IEqualityComparer<string>) cultureIgnoreCase);
    }
  }
}
