using Cake.Core;
using Cake.Core.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Cake.DeployParams
{
    public class ParametersDefinition
    {
        private readonly IDictionary<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>> environments = new Dictionary<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>>();
        private readonly IList<RoleConfiguration> roles = new List<RoleConfiguration>();
        private readonly ICakeEnvironment environment;
        private readonly ICakeLog log;

        public ParametersDefinition(ICakeEnvironment environment, ICakeLog log)
        {
            this.environment = environment;
            this.log = log;
        }

        public void CreateParamsFiles(string filePath)
        {
            this.log.Information("Creating parameters files in {0}", filePath);
            foreach (KeyValuePair<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>> environment in this.environments)
            {
                foreach (RoleConfiguration role in this.roles)
                {
                    IDictionary<string, object> parameters = this.GetParameters(role, environment.Key);
                    this.CreateSetParametersFile(Path.Combine(filePath, string.Format("{0}\\SetParameters.{1}.xml", role.Name, environment.Key)), role, parameters);
                }
            }
            foreach (RoleConfiguration role in this.roles)
            {
                this.CreateParemetersFile(role, Path.Combine(filePath, string.Format("{0}\\parameters.xml", role.Name)));
            }
        }

        public void CreateParemetersFile(RoleConfiguration role, string filePath)
        {
            this.log.Debug("Creating {0}", filePath);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 4;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("parameters");
            foreach (ParameterConfiguration parameter in role.Parameters)
            {
                xmlTextWriter.WriteStartElement("parameter");
                xmlTextWriter.WriteAttributeString("name", parameter.Name);
                foreach (ParameterEntry entry in parameter.Entries)
                {
                    xmlTextWriter.WriteStartElement("parameterEntry");
                    xmlTextWriter.WriteAttributeString("kind", entry.Kind);
                    xmlTextWriter.WriteAttributeString("scope", entry.Scope);
                    xmlTextWriter.WriteAttributeString("match", entry.Match);
                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.WriteEndElement();
            }
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Flush();
            xmlTextWriter.Close();
        }

        public void CreateSetParametersFile(string filePath, RoleConfiguration role, IDictionary<string, object> values)
        {
            this.log.Debug("Creating {0}", filePath);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 4;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("parameters");
            foreach (ParameterConfiguration parameter in role.Parameters)
            {
                xmlTextWriter.WriteStartElement("setParameter");
                xmlTextWriter.WriteAttributeString("name", parameter.Name);
                xmlTextWriter.WriteAttributeString("value", values[parameter.Name].ToString());
                xmlTextWriter.WriteEndElement();
            }
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Flush();
            xmlTextWriter.Close();
        }

        public void Env(string name, Func<object, object> parameters)
        {
            this.environments.Add(nameof(Env), name, parameters);
        }

        public IDictionary<string, object> GetParameters(string role, string environment)
        {
            return this.GetParameters(this.roles.Single(o => o.Name == role), environment);
        }

        public IDictionary<string, object> GetParameters(RoleConfiguration role, string environment)
        {
            var data = (IDictionary<string, object>)new ExpandoObject();
            foreach (var dataSetGetter in this.environments[environment])
            {
                var dataSet = dataSetGetter(data);

                foreach (var item in dataSet)
                {
                    var key = item.Key;
                    var value = item.Value;
                    data[key] = value;
                }
            }
            foreach (var parameter in role.Parameters)
            {
                string name = parameter.Name;
                object value = parameter.ValueGetter(data);
                data[name] = value;
            }
            return data;
        }

        public void Role(string name, params Action<RoleConfiguration>[] configurations)
        {
            RoleConfiguration roleConfiguration = new RoleConfiguration(name);
            foreach (Action<RoleConfiguration> configuration in configurations)
                configuration(roleConfiguration);
            this.roles.Add(roleConfiguration);
        }
    }
}
