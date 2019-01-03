using NUnit.Framework;

namespace Spice.Domain.Tests
{
    public abstract class AbstractBaseDomainTestFixture<T> where T : class
    {
        private T _domainObject;

        protected T DomainObject => _domainObject;

        protected abstract T CreateDomainObject();

        [SetUp]
        protected virtual void SetUp()
        {
            _domainObject = CreateDomainObject();
        }
    }
}