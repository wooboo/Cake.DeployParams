// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.DeploySettings
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using Cake.Core.IO;
using Microsoft.Web.Deployment;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cake.DeployParams
{
  public class DeploySettings
  {
    private bool? ntlm;
    private string publishUrl;

    public RemoteAgent AgentType { get; set; } = RemoteAgent.None;

    public bool AllowUntrusted { get; set; } = true;

    public string ComputerName { get; set; }

    public FilePath DeclareParamFile { get; set; }

    public bool Delete { get; set; } = true;

    public FilePath DestinationPath { get; set; }

    public DeploymentWellKnownProvider DestProvider { get; set; } = DeploymentWellKnownProvider.Package;

    public bool NTLM
    {
      get
      {
        if (this.ntlm.HasValue)
          return this.ntlm.Value;
        return this.AgentType != RemoteAgent.WMSvc && this.AgentType != RemoteAgent.None;
      }
      set
      {
        this.ntlm = new bool?(value);
      }
    }

    public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

    public string Password { get; set; }

    public int Port { get; set; } = 8172;

    public string PublishUrl
    {
      get
      {
        if (!string.IsNullOrEmpty(this.publishUrl))
          return this.publishUrl;
        if (this.AgentType == RemoteAgent.WMSvc)
          return DeployUtils.GetWmsvcUrl(this.ComputerName, this.Port, this.SiteName);
        return this.ComputerName;
      }
      set
      {
        this.publishUrl = value;
      }
    }

    public string SiteName { get; set; }

    public List<SkipRule> SkipRules { get; } = new List<SkipRule>();

    public FilePath SourcePath { get; set; }

    public DeploymentWellKnownProvider SourceProvider { get; set; } = DeploymentWellKnownProvider.ContentPath;

    public TraceLevel TraceLevel { get; set; } = TraceLevel.Info;

    public string Username { get; set; }

    public bool WhatIf { get; set; }
  }
}
