﻿// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.IMsDeployManager
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using Microsoft.Web.Deployment;

namespace Cake.DeployParams
{
    public interface IMsDeployManager
    {
        DeploymentChangeSummary Deploy(DeploySettings settings);
    }
}
