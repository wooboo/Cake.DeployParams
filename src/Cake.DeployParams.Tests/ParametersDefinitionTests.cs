namespace Cake.DeployParams.Tests
{
    using Cake.Core;
    using Cake.Core.Diagnostics;
    using Moq;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Xunit;

    public class ParametersDefinitionTests
    {
        [Fact]
        public void Do()
        {
            var cakeEnvMock = new Mock<ICakeEnvironment>();
            var cakeLogMock = new Mock<ICakeLog>();

            var s = new ParametersDefinition(cakeEnvMock.Object, cakeLogMock.Object);
            s.Env("DEV", _ => new { Host = "a" });

            s.Role("APP1", 
                e => e.Parameter("connectionString", o => $"aaaa{o.Host}bbbb").AsConnectionString(),
                e => e.Parameter("Host").AsAppSettings()
                );

            var parameters = s.GetParameters("APP1", "DEV");

            Assert.Equal("aaaaabbbb", parameters["connectionString"]);
            Assert.Equal("a", parameters["Host"]);
        }
    }
}