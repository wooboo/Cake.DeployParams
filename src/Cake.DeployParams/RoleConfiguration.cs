using System;
using System.Collections.Generic;

namespace Cake.DeployParams
{
    public class RoleConfiguration
    {
        public RoleConfiguration(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public IList<ParameterConfiguration> Parameters { get; } = new List<ParameterConfiguration>();

        public ParameterConfiguration Parameter(string name, Func<dynamic, object> valueGetter = null)
        {
            ParameterConfiguration parameterConfiguration = new ParameterConfiguration(name, valueGetter ?? (e => ((IDictionary<string, object>)e)[name]));
            this.Parameters.Add(parameterConfiguration);
            return parameterConfiguration;
        }
    }
}
