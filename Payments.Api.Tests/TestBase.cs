using System;
using AutoFixture;

namespace Payments.Api.Tests
{
    public class TestBase : IDisposable
    {
        protected Fixture Fixture { get; }       

        public TestBase()
        {
            Fixture = new Fixture();            
        }

        public void Dispose()
        {            
        }
    }
}
