using System;
using AutoFixture;

namespace Payments.Api.Tests
{
    public class TestBase : IDisposable
    {
        protected Fixture Fixture { get; }
        protected PrivateAccessHelper PrivateAccessHelper { get; }

        public TestBase()
        {
            Fixture = new Fixture();
            PrivateAccessHelper = new PrivateAccessHelper();
        }

        public void Dispose()
        {            
        }
    }
}
