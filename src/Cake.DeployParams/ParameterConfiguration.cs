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

        public IList<ParameterEntry> Entries { get; } = new List<ParameterEntry>();

        public string Name { get; }

        public Func<object, object> ValueGetter { get; }

        public ParameterConfiguration AsApplicationSettings(string section, string key = null, string scope = "\\.config")
        {
            return this.AsXPath(string.Format("//applicationSettings/{0}/setting[@name='{1}']/value/text()", section, key ?? this.Name), scope, "XmlFile");
        }

        public ParameterConfiguration AsAppSettings(string key = null, string scope = "\\.config")
        {
            return this.AsXPath(string.Format("//appSettings/add[@key='{0}']/@value", key ?? this.Name), scope, "XmlFile");
        }

        public ParameterConfiguration AsConnectionString(string key = null, string scope = "\\.config")
        {
            return this.AsXPath(string.Format("//connectionStrings/add[@name='{0}']/@connectionString", key ?? this.Name), scope, "XmlFile");
        }

        public ParameterConfiguration AsXPath(string match, string scope = "\\.config", string kind = "XmlFile")
        {
            this.Entries.Add(new ParameterEntry(match, scope, kind));
            return this;
        }
    }
}
