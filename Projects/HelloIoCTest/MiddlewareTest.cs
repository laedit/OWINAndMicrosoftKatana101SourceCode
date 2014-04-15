using HelloIoC;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloIoCTest
{
    [TestClass]
    public class MiddlewareTest
    {
        [TestMethod]
        public async Task For7HoursMessageMustBeAnteMeridiem()
        {
            var clock = MockRepository.GenerateMock<IClock>();
            clock.Stub(x => x.TimeNow).Return(new DateTime(1900, 1, 1, 7, 0, 0));

            var startup = new Startup(clock);

            using (var server = TestServer.Create(startup.Configuration))
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync("/");

                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.AreEqual("Ante Meridiem", responseBody);
            }
        }

        [TestMethod]
        public async Task For14HoursMessageMustBePostMeridiem()
        {
            var clock = MockRepository.GenerateMock<IClock>();
            clock.Stub(x => x.TimeNow).Return(new DateTime(1900, 1, 1, 14, 0, 0));

            var startup = new Startup(clock);

            using (var server = TestServer.Create(startup.Configuration))
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync("/");

                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.AreEqual("Post Meridiem", responseBody);
            }
        }
    }
}