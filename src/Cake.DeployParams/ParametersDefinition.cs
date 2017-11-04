// Decompiled with JetBrains decompiler
// Type: Cake.DeployParams.ParametersDefinition
// Assembly: Cake.DeployParams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A250231-E976-48D4-971A-E7E4C6495A73
// Assembly location: C:\Users\woobo\Downloads\cake.deployparams.0.3.0-unstable0002.nupkg\lib\net45\Cake.DeployParams.dll

using Cake.Core;
using Cake.Core.Diagnostics;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Cake.DeployParams
{
    public class ParametersDefinition
    {
        private static CallSite<Func<CallSite, Func<IDictionary<string, object>, IDictionary<string, object>>, object, object>> _1_2o__10x_1_2p__0;
        private static CallSite<Func<CallSite, object, IEnumerable>> _1_2o__10x_1_2p__5;
        private static CallSite<Func<CallSite, IDictionary<string, object>, object, object, object>> _1_2o__10x_1_2p__4;
        private static CallSite<Func<CallSite, object, object>> _1_2o__10x_1_2p__3;
        private static CallSite<Func<CallSite, object, object>> _1_2o__10x_1_2p__1;
        private static CallSite<Func<CallSite, object, IDictionary<string, object>>> _1_2o__10x_1_2p__7;
        private static CallSite<Func<CallSite, Func<object, object>, object, object>> _1_2o__10x_1_2p__6;
        private static CallSite<Func<CallSite, object, IDictionary<string, object>>> _1_2o__10x_1_2p__8;
        private static CallSite<Func<CallSite, object, IDictionary<string, object>>> _1_2o__10x_1_2p__2;
        private readonly IDictionary<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>> environments = (IDictionary<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>>)new Dictionary<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>>();
        private readonly IList<RoleConfiguration> roles = (IList<RoleConfiguration>)new List<RoleConfiguration>();
        private readonly ICakeEnvironment environment;
        private readonly ICakeLog log;

        public ParametersDefinition(ICakeEnvironment environment, ICakeLog log)
        {
            this.environment = environment;
            this.log = log;
        }

        public void CreateParamsFiles(string filePath)
        {
            LogExtensions.Information(this.log, string.Format("Creating parameters files in {0}", (object)filePath), new object[0]);
            foreach (KeyValuePair<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>> environment in (IEnumerable<KeyValuePair<string, IList<Func<IDictionary<string, object>, IDictionary<string, object>>>>>)this.environments)
            {
                foreach (RoleConfiguration role in (IEnumerable<RoleConfiguration>)this.roles)
                {
                    IDictionary<string, object> parameters = this.GetParameters(role, environment.Key);
                    this.CreateSetParametersFile(Path.Combine(filePath, string.Format("{0}\\SetParameters.{1}.xml", (object)role.Name, (object)environment.Key)), role, parameters);
                }
            }
            foreach (RoleConfiguration role in (IEnumerable<RoleConfiguration>)this.roles)
                this.CreateParemetersFile(role, Path.Combine(filePath, string.Format("{0}\\parameters.xml", (object)role.Name)));
        }

        public void CreateParemetersFile(RoleConfiguration role, string filePath)
        {
            LogExtensions.Debug(this.log, string.Format("Creating {0}", (object)filePath), new object[0]);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 4;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("parameters");
            foreach (ParameterConfiguration parameter in (IEnumerable<ParameterConfiguration>)role.Parameters)
            {
                xmlTextWriter.WriteStartElement("parameter");
                xmlTextWriter.WriteAttributeString("name", parameter.Name);
                foreach (ParameterEntry entry in (IEnumerable<ParameterEntry>)parameter.Entries)
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
            LogExtensions.Debug(this.log, string.Format("Creating {0}", (object)filePath), new object[0]);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 4;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("parameters");
            foreach (ParameterConfiguration parameter in (IEnumerable<ParameterConfiguration>)role.Parameters)
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
            this.environments.Add<IDictionary<string, object>>(nameof(Env), name, (Func<IDictionary<string, object>, object>)parameters);
        }

        public IDictionary<string, object> GetParameters(string role, string environment)
        {
            return this.GetParameters(this.roles.Single<RoleConfiguration>((Func<RoleConfiguration, bool>)(o => o.Name == role)), environment);
        }

        public IDictionary<string, object> GetParameters(RoleConfiguration role, string environment)
        {
            object obj1 = (object)new ExpandoObject();
            foreach (Func<IDictionary<string, object>, IDictionary<string, object>> func in (IEnumerable<Func<IDictionary<string, object>, IDictionary<string, object>>>)this.environments[environment])
            {
                // ISSUE: reference to a compiler-generated field
                if (ParametersDefinition._1_2o__10x_1_2p__0 == null)
                {
                    // ISSUE: reference to a compiler-generated field
                    ParametersDefinition._1_2o__10x_1_2p__0 = CallSite<Func<CallSite, Func<IDictionary<string, object>, IDictionary<string, object>>, object, object>>.Create(Binder.Invoke(CSharpBinderFlags.None, typeof(ParametersDefinition), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
                    {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                    }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj2 = ParametersDefinition._1_2o__10x_1_2p__0.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__0, func, obj1);
                // ISSUE: reference to a compiler-generated field
                if (ParametersDefinition._1_2o__10x_1_2p__5 == null)
                {
                    // ISSUE: reference to a compiler-generated field
                    ParametersDefinition._1_2o__10x_1_2p__5 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(IEnumerable), typeof(ParametersDefinition)));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                foreach (object obj3 in ParametersDefinition._1_2o__10x_1_2p__5.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__5, obj2))
                {
                    // ISSUE: reference to a compiler-generated field
                    if (ParametersDefinition._1_2o__10x_1_2p__4 == null)
                    {
                        // ISSUE: reference to a compiler-generated field
                        ParametersDefinition._1_2o__10x_1_2p__4 = CallSite<Func<CallSite, IDictionary<string, object>, object, object, object>>.Create(Binder.SetIndex(CSharpBinderFlags.None, typeof(ParametersDefinition), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[3]
                        {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                        }));
                    }
                    // ISSUE: reference to a compiler-generated field
                    Func<CallSite, IDictionary<string, object>, object, object, object> target = ParametersDefinition._1_2o__10x_1_2p__4.Target;
                    // ISSUE: reference to a compiler-generated field
                    CallSite<Func<CallSite, IDictionary<string, object>, object, object, object>> p4 = ParametersDefinition._1_2o__10x_1_2p__4;
                    // ISSUE: reference to a compiler-generated field
                    if (ParametersDefinition._1_2o__10x_1_2p__2 == null)
                    {
                        // ISSUE: reference to a compiler-generated field
                        ParametersDefinition._1_2o__10x_1_2p__2 = CallSite<Func<CallSite, object, IDictionary<string, object>>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(IDictionary<string, object>), typeof(ParametersDefinition)));
                    }
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    IDictionary<string, object> dictionary = ParametersDefinition._1_2o__10x_1_2p__2.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__2, obj1);
                    // ISSUE: reference to a compiler-generated field
                    if (ParametersDefinition._1_2o__10x_1_2p__3 == null)
                    {
                        // ISSUE: reference to a compiler-generated field
                        ParametersDefinition._1_2o__10x_1_2p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Key", typeof(ParametersDefinition), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[1]
                        {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                        }));
                    }
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    object obj4 = ParametersDefinition._1_2o__10x_1_2p__3.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__3, obj3);
                    // ISSUE: reference to a compiler-generated field
                    if (ParametersDefinition._1_2o__10x_1_2p__1 == null)
                    {
                        // ISSUE: reference to a compiler-generated field
                        ParametersDefinition._1_2o__10x_1_2p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(ParametersDefinition), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[1]
                        {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                        }));
                    }
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    object obj5 = ParametersDefinition._1_2o__10x_1_2p__1.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__1, obj3);
                    object obj6 = target((CallSite)p4, dictionary, obj4, obj5);
                }
            }
            foreach (ParameterConfiguration parameter in (IEnumerable<ParameterConfiguration>)role.Parameters)
            {
                // ISSUE: reference to a compiler-generated field
                if (ParametersDefinition._1_2o__10x_1_2p__7 == null)
                {
                    // ISSUE: reference to a compiler-generated field
                    ParametersDefinition._1_2o__10x_1_2p__7 = CallSite<Func<CallSite, object, IDictionary<string, object>>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(IDictionary<string, object>), typeof(ParametersDefinition)));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                IDictionary<string, object> dictionary = ParametersDefinition._1_2o__10x_1_2p__7.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__7, obj1);
                string name = parameter.Name;
                // ISSUE: reference to a compiler-generated field
                if (ParametersDefinition._1_2o__10x_1_2p__6 == null)
                {
                    // ISSUE: reference to a compiler-generated field
                    ParametersDefinition._1_2o__10x_1_2p__6 = CallSite<Func<CallSite, Func<object, object>, object, object>>.Create(Binder.Invoke(CSharpBinderFlags.None, typeof(ParametersDefinition), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
                    {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                    }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj2 = ParametersDefinition._1_2o__10x_1_2p__6.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__6, parameter.ValueGetter, obj1);
                dictionary[name] = obj2;
            }
            // ISSUE: reference to a compiler-generated field
            if (ParametersDefinition._1_2o__10x_1_2p__8 == null)
            {
                // ISSUE: reference to a compiler-generated field
                ParametersDefinition._1_2o__10x_1_2p__8 = CallSite<Func<CallSite, object, IDictionary<string, object>>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(IDictionary<string, object>), typeof(ParametersDefinition)));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            return ParametersDefinition._1_2o__10x_1_2p__8.Target((CallSite)ParametersDefinition._1_2o__10x_1_2p__8, obj1);
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
