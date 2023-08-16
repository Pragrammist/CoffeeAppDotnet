using EFCore;

namespace Services.Common
{
    public abstract class ServiceBase
    {
        readonly protected IRepository _dbRepository;
        public ServiceBase(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
    }
}