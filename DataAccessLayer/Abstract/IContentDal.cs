using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstract
{
    public interface IContentDal : IRepository<Content>
    {
        List<Content> GetContentsWithHeading(Expression<Func<Content, bool>> filter = null);
    }
}
