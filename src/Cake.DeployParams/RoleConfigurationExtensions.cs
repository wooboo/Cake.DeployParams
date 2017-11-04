using System;

namespace Cake.DeployParams
{
    public static class RoleConfigurationExtensions
    {
        public static ParameterConfiguration ConnectionString(this RoleConfiguration roleConfiguration, string name, Func<dynamic, object> valueGetter = null)
        {
            return roleConfiguration.Parameter(name, valueGetter).AsConnectionString();
        }
        public static ParameterConfiguration AppSettings(this RoleConfiguration roleConfiguration, string name, Func<dynamic, object> valueGetter = null)
        {
            return roleConfiguration.Parameter(name, valueGetter).AsAppSettings();
        }
        public static void ConfigSection(this RoleConfiguration roleConfiguration, string name,
            params Func<SimpleRoleConfiguration, SimpleRoleConfiguration>[] parameters)
        {
            foreach (var parameter in parameters)
            {
                parameter(new SimpleRoleConfiguration(roleConfiguration, name));
            }
        }
        public class SimpleRoleConfiguration
        {
            private readonly RoleConfiguration roleConfiguration;
            private readonly string sectionName;

            public SimpleRoleConfiguration(RoleConfiguration roleConfiguration, string sectionName)
            {
                this.roleConfiguration = roleConfiguration;
                this.sectionName = sectionName;
            }
            public SimpleRoleConfiguration Parameter(string name, Func<dynamic, object> valueGetter = null)
            {
                this.roleConfiguration.Parameter(name, valueGetter).AsApplicationSettings(sectionName);
                return this;
            }
        }
    }
}
