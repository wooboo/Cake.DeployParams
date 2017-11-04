using Cake.Core;
using Cake.Core.Annotations;
using System;

namespace Cake.DeployParams
{
    [CakeAliasCategory("PARAMETERS")]
    public static class ParamsAliases
    {
        private static ParametersDefinition parametersDefinition;

        [CakePropertyAlias]
        public static ParametersDefinition DeployParams(this ICakeContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (ParamsAliases.parametersDefinition == null)
                ParamsAliases.parametersDefinition = new ParametersDefinition(context.Environment, context.Log);
            return ParamsAliases.parametersDefinition;
        }

        [CakeMethodAlias]
        public static string MakeAppUrl(this ICakeContext context, object left, object env, object right)
        {
            string str = "prod";
            if (env.ToString().Equals(str, StringComparison.InvariantCultureIgnoreCase))
                return string.Format("{0}.{1}", left, right);
            return string.Format("{0}.{1}.{2}", left, env, right);
        }
    }
}
