using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Persistance.Context;

namespace JwtApp.Api.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JwtContext _context;

        public UnitOfWork(JwtContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
