using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repositoryContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IOwnerRepository Owner => _owner ?? (_owner = new OwnerRepository(_repositoryContext));

        public IAccountRepository Account => _account ?? (_account = new AccountRepository(_repositoryContext));
    }
}