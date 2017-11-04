// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.SkipRule
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

namespace Cake.DeployParams
{
  public class SkipRule
  {
    public SkipRule()
    {
    }

    public SkipRule(string name, string skipAction, string objectName, string absolutePath, string xpath = null)
    {
      this.Name = name;
      this.SkipAction = skipAction;
      this.ObjectName = objectName;
      this.AbsolutePath = absolutePath;
      this.XPath = xpath;
    }

    public string AbsolutePath { get; set; }

    public string Name { get; set; }

    public string ObjectName { get; set; }

    public string SkipAction { get; set; }

    public string XPath { get; set; }
  }
}
