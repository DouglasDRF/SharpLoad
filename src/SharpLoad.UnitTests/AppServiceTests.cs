using System;
using System.Threading.Tasks;
using Moq;
using SharpLoad.AppService.Abstractions;
using SharpLoad.AppService.Implementations;
using SharpLoad.Core.Enums;
using SharpLoad.Core.Models;
using SharpLoad.Core.Repositories;
using Xunit;

namespace SharpLoad.UnitTests
{
    public class AppServiceTests
    {
        private readonly ILoadTestScriptAppService _appService;
        public AppServiceTests()
        {
            var repositoryMock = new Mock<ILoadTestScriptRepository>();
            var testDummy = new LoadTestScript(1, "test", new Uri("https://google.com"), 100, 1, 1000, TestStatus.Created, null);

            repositoryMock.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(testDummy));
            repositoryMock.Setup(x => x.GetByIdAsync(2)).Returns(Task.FromResult<LoadTestScript>(null));
            repositoryMock.Setup(x => x.DeleteAsync(testDummy)).ReturnsAsync(1);

            _appService = new LoadTestScriptAppService(repositoryMock.Object);
        }

        [Fact]
        public async void ShouldDeleteTestScript()
        {
            var result = await _appService.DeleteAsync(1);
            
            Assert.Equal(1, result);
        }

        [Fact]
        public async void ShouldNotFindTestScriptToDelete()
        {
            var result = await _appService.DeleteAsync(2);
            Assert.Equal(0, result);
        }
    }
}
