using PGP.Persistence;
using System;

namespace PGP.Application.UnitTests.Common
{
    public class CommonTestBase : IDisposable
    {
        protected readonly PGPDbContext _context;

        public CommonTestBase()
        {
            _context = PGPContextFactory.Create();
        }

        public void Dispose()
        {
            PGPContextFactory.Destroy(_context);
        }
    }
}
