namespace Cake.DeployParams.Tests
{
    using Cake.Core;
    using Cake.Core.Diagnostics;
    using Moq;

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
                e => e.ConnectionString("connectionString", o => $"aaaa{o.Host}bbbb"),
                e => e.AppSettings("Host"),
                e => e.ConfigSection("Hello.World", p => p.Parameter("abc", o => "abc")),
                e => e.Parameter("xyz", o => "xyz" + o.Env),
                e => e.Parameter("xxx", o => "xxx" + o.xyz)
                );

            var parameters = s.GetParameters("APP1", "DEV");

            Assert.Equal("aaaaabbbb", parameters["connectionString"]);
            Assert.Equal("a", parameters["Host"]);
            Assert.Equal("abc", parameters["abc"]);
            Assert.Equal("xyzDEV", parameters["xyz"]);
            Assert.Equal("xxxxyzDEV", parameters["xxx"]);
        }
    }
}