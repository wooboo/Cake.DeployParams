// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.ParamsAliases
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using Cake.Core;
using Cake.Core.Annotations;
using System;

namespace Cake.DeployParams
{
  [CakeAliasCategory("PARAMETERS")]
  public static class ParamsAliases
  {
    private static ParametersDefinition parametersDefinition;

    [CakePropertyAlias]
    public static ParametersDefinition DeployParams(this ICakeContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (ParamsAliases.parametersDefinition == null)
        ParamsAliases.parametersDefinition = new ParametersDefinition(context.Environment, context.Log);
      return ParamsAliases.parametersDefinition;
    }

    [CakeMethodAlias]
    public static string MakeAppUrl(this ICakeContext context, object left, object env, object right)
    {
      string str = "prod";
      if (env.ToString().Equals(str, StringComparison.InvariantCultureIgnoreCase))
        return string.Format("{0}.{1}", left, right);
      return string.Format("{0}.{1}.{2}", left, env, right);
    }
  }
}
