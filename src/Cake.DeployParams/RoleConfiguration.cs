// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.RoleConfiguration
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cake.DeployParams
{
  public class RoleConfiguration
  {
        private static CallSite<Func<CallSite, object, IDictionary<string, object>>> _1_2o__7x_1_2p__0;

        public RoleConfiguration(string name)
    {
      this.Name = name;
    }

    public string Name { get; }

    public IList<ParameterConfiguration> Parameters { get; } = (IList<ParameterConfiguration>) new List<ParameterConfiguration>();

    public ParameterConfiguration Parameter(string name, Func<dynamic, object> valueGetter = null)
    {
      ParameterConfiguration parameterConfiguration = new ParameterConfiguration(name, valueGetter ?? (Func<object, object>) (e =>
      {
        // ISSUE: reference to a compiler-generated field
        if (RoleConfiguration._1_2o__7x_1_2p__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RoleConfiguration._1_2o__7x_1_2p__0 = CallSite<Func<CallSite, object, IDictionary<string, object>>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (IDictionary<string, object>), typeof (RoleConfiguration)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        return RoleConfiguration._1_2o__7x_1_2p__0.Target((CallSite) RoleConfiguration._1_2o__7x_1_2p__0, e)[name];
      }));
      this.Parameters.Add(parameterConfiguration);
      return parameterConfiguration;
    }
  }
}
