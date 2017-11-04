// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.MsDeployManager
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Microsoft.Web.Deployment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Cake.DeployParams
{
  public class MsDeployManager : IMsDeployManager
  {
    private readonly ICakeEnvironment environment;
    private readonly ICakeLog log;

    public MsDeployManager(ICakeEnvironment environment, ICakeLog log)
    {
      if (environment == null)
        throw new ArgumentNullException(nameof (environment));
      if (log == null)
        throw new ArgumentNullException(nameof (log));
      this.environment = environment;
      this.log = log;
    }

    public DeploymentChangeSummary Deploy(DeploySettings settings)
    {
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      DeploymentBaseOptions baseOptions1 = new DeploymentBaseOptions();
      DeploymentBaseOptions baseOptions2 = this.GetBaseOptions(settings);
      FilePath sourcePath = settings.SourcePath;
      string str1;
      if (sourcePath == null)
      {
        str1 = (string) null;
      }
      else
      {
        ICakeEnvironment environment = this.environment;
        FilePath filePath = sourcePath.MakeAbsolute(environment);
        str1 = filePath != null ? ((Path) filePath).FullPath : (string) null;
      }
      string path1 = str1;
      FilePath destinationPath = settings.DestinationPath;
      string str2;
      if (destinationPath == null)
      {
        str2 = (string) null;
      }
      else
      {
        ICakeEnvironment environment = this.environment;
        FilePath filePath = destinationPath.MakeAbsolute(environment);
        str2 = filePath != null ? ((Path) filePath).FullPath : (string) null;
      }
      string path2 = str2;
      LogExtensions.Information(this.log, (Verbosity) 2, "CCC", new object[0]);
      baseOptions2.TraceLevel = settings.TraceLevel;
      baseOptions2.Trace += new EventHandler<DeploymentTraceEventArgs>(this.OnTraceEvent);
      if (settings.DestProvider == DeploymentWellKnownProvider.Auto)
        path2 = settings.SiteName;
      DeploymentSyncOptions deploymentSyncOptions = new DeploymentSyncOptions();
      int num1 = !settings.Delete ? 1 : 0;
      deploymentSyncOptions.DoNotDelete = num1 != 0;
      int num2 = settings.WhatIf ? 1 : 0;
      deploymentSyncOptions.WhatIf = num2 != 0;
      DeploymentSyncOptions syncOptions = deploymentSyncOptions;
      if (settings.DeclareParamFile != null)
        syncOptions.DeclaredParameters.Load(((Path) settings.DeclareParamFile.MakeAbsolute(this.environment)).FullPath);
      foreach (SkipRule skipRule in settings.SkipRules)
        syncOptions.Rules.Add((DeploymentRule) new DeploymentSkipRule(skipRule.Name, skipRule.SkipAction, skipRule.ObjectName, skipRule.AbsolutePath, skipRule.XPath));
      LogExtensions.Information(this.log, (Verbosity) 2, "Deploying Website...", new object[0]);
      LogExtensions.Debug(this.log, (Verbosity) 2, string.Format("-siteName '{0}'", (object) settings.SiteName), new object[0]);
      LogExtensions.Debug(this.log, (Verbosity) 2, string.Format("-destination '{0}'", (object) settings.PublishUrl), new object[0]);
      LogExtensions.Debug(this.log, (Verbosity) 2, string.Format("-source '{0}'", (object) path1), new object[0]);
      LogExtensions.Debug(this.log, string.Empty, new object[0]);
      using (DeploymentObject deploymentObject = DeploymentManager.CreateObject(settings.SourceProvider, path1, baseOptions1))
      {
        foreach (KeyValuePair<string, string> parameter in settings.Parameters)
          deploymentObject.SyncParameters[parameter.Key].Value = parameter.Value;
        return deploymentObject.SyncTo(settings.DestProvider, path2, baseOptions2, syncOptions);
      }
    }

    private DeploymentBaseOptions GetBaseOptions(DeploySettings settings)
    {
      DeploymentBaseOptions deploymentBaseOptions = new DeploymentBaseOptions();
      deploymentBaseOptions.ComputerName = settings.PublishUrl;
      deploymentBaseOptions.UserName = settings.Username;
      deploymentBaseOptions.Password = settings.Password;
      deploymentBaseOptions.AuthenticationType = settings.NTLM ? "ntlm" : "basic";
      if (!settings.AllowUntrusted)
        return deploymentBaseOptions;
      ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.OnCertificateValidation);
      return deploymentBaseOptions;
    }

    private bool OnCertificateValidation(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
    {
      return true;
    }

    private void OnTraceEvent(object sender, DeploymentTraceEventArgs e)
    {
      switch (e.EventLevel)
      {
        case TraceLevel.Error:
          LogExtensions.Error(this.log, e.Message, new object[0]);
          break;
        case TraceLevel.Warning:
          LogExtensions.Warning(this.log, e.Message, new object[0]);
          break;
        case TraceLevel.Info:
          LogExtensions.Information(this.log, e.Message, new object[0]);
          break;
        case TraceLevel.Verbose:
          LogExtensions.Verbose(this.log, e.Message, new object[0]);
          break;
      }
    }
  }
}
