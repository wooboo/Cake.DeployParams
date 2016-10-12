namespace Cake.DeployParams
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Text;
    using System.Xml;

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

    public class ParametersDefinition
    {
        private readonly IDictionary<string, IList<Func<dynamic, IDictionary<string, object>>>> environments =
            new Dictionary<string, IList<Func<dynamic, IDictionary<string, object>>>>();

        private readonly IDictionary<string, IList<ParameterEntry>> parameterEntries =
            new Dictionary<string, IList<ParameterEntry>>();

        private readonly IDictionary<string, IList<Func<dynamic, IDictionary<string, object>>>> roles =
            new Dictionary<string, IList<Func<dynamic, IDictionary<string, object>>>>();

        public void AddApplicationSettingsParameter(
            string name, 
            string section, 
            string key = null, 
            string scope = "\\.config")
        {
            this.AddXPathParameter(
                name, 
                $"//applicationSettings/{section}/setting[@name='{key ?? name}']/value/text()", 
                scope);
        }

        public void AddAppSettingsParameter(string name, string key = null, string scope = "\\.config")
        {
            this.AddXPathParameter(name, $"//appSettings/add[@key='{key ?? name}']/@value", scope);
        }

        public void AddConnectionStringParameter(string name, string key = null, string scope = "\\.config")
        {
            this.AddXPathParameter(name, $"//connectionStrings/add[@name='{key ?? name}']/@connectionString", scope);
        }

        public void AddXPathParameter(string name, string match, string scope = "\\.config", string kind = "XmlFile")
        {
            if (!this.parameterEntries.ContainsKey(name))
            {
                this.parameterEntries[name] = new List<ParameterEntry>();
            }

            this.parameterEntries[name].Add(new ParameterEntry(match, scope, kind));
        }

        public void CreateParamsFiles(string filePath)
        {
            foreach (var envItem in this.environments)
            {
                foreach (var roleItem in this.roles)
                {
                    var psparams = GetParameters(roleItem.Key, envItem.Key);
                    this.CreateSetParametersFile(
                        Path.Combine(filePath, $"{roleItem.Key}\\SetParameters.{envItem.Key}.xml"),
                        psparams);
                }
            }

            foreach (var role in this.roles)
            {
                this.CreateParemetersFile(Path.Combine(filePath, $"{role.Key}\\parameters.xml"));
            }
        }

        public void CreateParemetersFile(string filePath)
        {
            var xmlWriter = new XmlTextWriter(filePath, Encoding.UTF8);

            // Set The Formatting
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = 4;

            // Write the XML Decleration
            xmlWriter.WriteStartDocument();

            // Write Root Element
            xmlWriter.WriteStartElement("parameters");

            foreach (var parameter in this.parameterEntries)
            {
                // Write the Document
                xmlWriter.WriteStartElement("parameter");
                xmlWriter.WriteAttributeString("name", parameter.Key);
                foreach (var entry in parameter.Value)
                {
                    xmlWriter.WriteStartElement("parameterEntry");
                    xmlWriter.WriteAttributeString("kind", entry.Kind);
                    xmlWriter.WriteAttributeString("scope", entry.Scope);
                    xmlWriter.WriteAttributeString("match", entry.Match);
                    xmlWriter.WriteEndElement(); // <-- Closing parameterEntry
                }

                xmlWriter.WriteEndElement(); // <-- Closing parameter
            }

            // Write Close Tag for Root Element
            xmlWriter.WriteEndElement(); // <-- Closing RootElement

            // End the XML Document
            xmlWriter.WriteEndDocument();

            // Finish The Document
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        public void CreateSetParametersFile(string filePath, IDictionary<string, object> role)
        {
            var xmlWriter = new XmlTextWriter(filePath, Encoding.UTF8);

            // Set The Formatting
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = 4;

            // Write the XML Decleration
            xmlWriter.WriteStartDocument();

            // Write Root Element
            xmlWriter.WriteStartElement("parameters");

            foreach (var parameter in this.parameterEntries)
            {
                // Write the Document
                xmlWriter.WriteStartElement("setParameter");
                xmlWriter.WriteAttributeString("name", parameter.Key);
                xmlWriter.WriteAttributeString("value", role[parameter.Key].ToString());
                xmlWriter.WriteEndElement(); // <-- Closing parameter
            }

            // Write Close Tag for Root Element
            xmlWriter.WriteEndElement(); // <-- Closing RootElement

            // End the XML Document
            xmlWriter.WriteEndDocument();

            // Finish The Document
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        public void Environment(string name, Func<dynamic, object> parameters)
        {
            this.environments.Add("Env", name, parameters);
        }

        public dynamic GetParameters(string role, string environment)
        {
            var expando = new ExpandoObject();
            var set = this.environments[environment];
            foreach (var func in set)
            {
                var dictionary = func(expando);
                foreach (var o in dictionary)
                {
                    ((IDictionary<string, object>)expando)[o.Key] = o.Value;
                }
            }

            set = this.roles[role];
            foreach (var func in set)
            {
                var dictionary = func(expando);
                foreach (var o in dictionary)
                {
                    ((IDictionary<string, object>)expando)[o.Key] = o.Value;
                }
            }

            return expando;
        }

        public void Role(string name, Func<dynamic, object> parameters)
        {
            this.roles.Add("Role", name, parameters);
        }
    }
}