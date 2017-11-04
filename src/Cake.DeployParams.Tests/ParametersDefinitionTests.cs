namespace Cake.DeployParams.Tests
{
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Xunit;

    public class ParametersDefinitionTests
    {
        [Fact]
        public void Do()
        {
            var s = new ParametersDefinition();
            s.Environment("DEV", _ => new { Host = "a" });

            s.Role("APP1", e => new { connectionString = "aaaa" + e.Host + "bbbb" });

            var parameters = s.GetParameters("APP1", "DEV");
            s.AddConnectionStringParameter("connectionString");
            s.CreateParamsFiles(".");
            
            Assert.Equal(parameters.connectionString, "aaaaabbbb");
            Assert.Equal(parameters.Role, "APP1");
            Assert.Equal(parameters.Env, "DEV");
        }
    }
}