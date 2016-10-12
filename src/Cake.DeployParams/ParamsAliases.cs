namespace Cake.DeployParams
{
    using System;
    using System.CodeDom;

    using Cake.Core;
    using Cake.Core.Annotations;

    [CakeAliasCategory("PARAMETERS")]
    public static class ParamsAliases
    {
        private static readonly ParametersDefinition parametersDefinition = new ParametersDefinition();

        [CakeMethodAlias]
        public static void Env(this ICakeContext context, string name, Func<dynamic, object> parameters)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            parametersDefinition.Environment(name, parameters);
        }

        [CakeMethodAlias]
        public static void Role(this ICakeContext context, string name, Func<dynamic, object> parameters)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            parametersDefinition.Role(name, parameters);
        }

        [CakeMethodAlias]
        public static void CreateParamsFiles(this ICakeContext context, string path)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            parametersDefinition.CreateParamsFiles(path);
        }

    }
}