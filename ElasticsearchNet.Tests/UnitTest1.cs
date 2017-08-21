using System;
using Xunit;
using Shouldly;
namespace ElasticsearchNet.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = true;
            result.ShouldBeTrue();
        }
    }
}
