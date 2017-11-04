// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.DeploySettingsExtensions
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using Cake.Core.IO;
using System;
using System.Diagnostics;

namespace Cake.DeployParams
{
    public static class DeploySettingsExtensions
    {
        public static DeploySettings AddParameter(this DeploySettings settings, string key, string value)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.Parameters[key] = value;
            return settings;
        }

        public static DeploySettings AddSkipRule(this DeploySettings settings, SkipRule rule)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.SkipRules.Add(rule);
            return settings;
        }

        public static DeploySettings AddSkipRule(this DeploySettings settings, string name, string skipAction, string objectName, string absolutePath, string xpath = null)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.SkipRules.Add(new SkipRule(name, skipAction, objectName, absolutePath, xpath));
            return settings;
        }

        public static DeploySettings FromSourcePath(this DeploySettings settings, string path)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.SourcePath = (FilePath)path;
            return settings;
        }

        public static DeploySettings SetAllowUntrusted(this DeploySettings settings, bool untrusted = true)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.AllowUntrusted = untrusted;
            return settings;
        }

        public static DeploySettings SetDelete(this DeploySettings settings, bool delete = true)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.Delete = delete;
            return settings;
        }

        public static DeploySettings SetPublishUrl(this DeploySettings settings, string url)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.PublishUrl = url;
            return settings;
        }

        public static DeploySettings SetTraceLevel(this DeploySettings settings, TraceLevel level)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.TraceLevel = level;
            return settings;
        }

        public static DeploySettings SetWhatIf(this DeploySettings settings, bool whatIf = true)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.WhatIf = whatIf;
            return settings;
        }

        public static DeploySettings ToDestinationPath(this DeploySettings settings, string path)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.DestinationPath = (FilePath)path;
            return settings;
        }

        public static DeploySettings UseAgentType(this DeploySettings settings, RemoteAgent agentType)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.AgentType = agentType;
            return settings;
        }

        public static DeploySettings UseComputerName(this DeploySettings settings, string name)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.ComputerName = name;
            return settings;
        }

        public static DeploySettings UseNTLM(this DeploySettings settings, bool ntlm = true)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.NTLM = ntlm;
            return settings;
        }

        public static DeploySettings UsePassword(this DeploySettings settings, string password)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.Password = password;
            return settings;
        }

        public static DeploySettings UsePort(this DeploySettings settings, int port)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.Port = port;
            return settings;
        }

        public static DeploySettings UseSiteName(this DeploySettings settings, string name)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.SiteName = name;
            return settings;
        }

        public static DeploySettings UseUsername(this DeploySettings settings, string username)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            settings.Username = username;
            return settings;
        }
    }
}
