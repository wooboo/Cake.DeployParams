// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.DeployUtils
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using System;

namespace Cake.DeployParams
{
    internal static class DeployUtils
    {
        internal const int DefaultPort = 8172;
        internal const string MSDeployHandler = "msdeploy.axd";

        internal static string AppendHandlerIfNotSpecified(string publishUrl)
        {
            if (!publishUrl.EndsWith("msdeploy.axd", StringComparison.OrdinalIgnoreCase))
                publishUrl = !publishUrl.EndsWith("/") ? publishUrl + "/msdeploy.axd" : publishUrl + "msdeploy.axd";
            return publishUrl;
        }

        internal static string GetWmsvcUrl(string computerName, int port, string siteName)
        {
            if (!computerName.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                computerName = DeployUtils.InsertPortIfNotSpecified(computerName, port);
                computerName = DeployUtils.AppendHandlerIfNotSpecified(computerName);
                computerName = string.IsNullOrEmpty(siteName) ? string.Format("https://{0}", (object)computerName) : string.Format("https://{0}?site={1}", (object)computerName, (object)siteName);
            }
            return computerName;
        }

        internal static string InsertPortIfNotSpecified(string publishUrl, int port)
        {
            string[] strArray = publishUrl.Split(':');
            if (strArray.Length == 1)
            {
                int startIndex = publishUrl.IndexOf('/');
                publishUrl = startIndex <= -1 ? publishUrl + ":" + (object)8172 : publishUrl.Insert(startIndex, ":" + (object)port);
            }
            if (strArray.Length > 1)
            {
                int startIndex = strArray[0].IndexOf('/');
                if (startIndex > -1)
                {
                    strArray[0] = strArray[0].Insert(startIndex, ":" + (object)port);
                    publishUrl = string.Join(":", strArray);
                }
            }
            return publishUrl;
        }
    }
}
