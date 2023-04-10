using System.Transactions;

namespace JwtApp.Api.Core.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

        void Commit();
    }
}
