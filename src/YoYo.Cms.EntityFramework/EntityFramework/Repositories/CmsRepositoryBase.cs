using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace YoYo.Cms.EntityFramework.Repositories
{
    public abstract class CmsRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<CmsDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected CmsRepositoryBase(IDbContextProvider<CmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class CmsRepositoryBase<TEntity> : CmsRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected CmsRepositoryBase(IDbContextProvider<CmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
