// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.ParameterConfiguration
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using System;
using System.Collections.Generic;

namespace Cake.DeployParams
{
  public class ParameterConfiguration
  {
    public ParameterConfiguration(string name, Func<object, object> valueGetter)
    {
      this.Name = name;
      this.ValueGetter = valueGetter;
    }

    public IList<ParameterEntry> Entries { get; } = (IList<ParameterEntry>) new List<ParameterEntry>();

    public string Name { get; }

    public Func<object, object> ValueGetter { get; }

    public ParameterConfiguration AsApplicationSettings(string section, string key = null, string scope = "\\.config")
    {
      return this.AsXPath(string.Format("//applicationSettings/{0}/setting[@name='{1}']/value/text()", (object) section, (object) (key ?? this.Name)), scope, "XmlFile");
    }

    public ParameterConfiguration AsAppSettings(string key = null, string scope = "\\.config")
    {
      return this.AsXPath(string.Format("//appSettings/add[@key='{0}']/@value", (object) (key ?? this.Name)), scope, "XmlFile");
    }

    public ParameterConfiguration AsConnectionString(string key = null, string scope = "\\.config")
    {
      return this.AsXPath(string.Format("//connectionStrings/add[@name='{0}']/@connectionString", (object) (key ?? this.Name)), scope, "XmlFile");
    }

    public ParameterConfiguration AsXPath(string match, string scope = "\\.config", string kind = "XmlFile")
    {
      this.Entries.Add(new ParameterEntry(match, scope, kind));
      return this;
    }
  }
}
