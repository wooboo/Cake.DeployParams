// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.ParameterEntry
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

namespace Cake.DeployParams
{
  public class ParameterEntry
  {
    public ParameterEntry(string match, string scope, string kind)
    {
      this.Match = match;
      this.Scope = scope;
      this.Kind = kind;
    }

    public string Kind { get; set; }

    public string Match { get; set; }

    public string Scope { get; set; }
  }
}
