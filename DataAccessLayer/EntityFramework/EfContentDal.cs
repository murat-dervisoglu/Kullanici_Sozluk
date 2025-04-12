using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.EntityFramework
{
    public class EfContentDal : GenericRepository<Content>, IContentDal
    {
        // Heading ile ilişkili verileri yükleyen özel bir metot
        public List<Content> GetContentsWithHeading(Expression<Func<Content, bool>> filter = null)
        {
            using (var context = new Context())
            {
                return filter == null
                    ? context.Contents.Include(c => c.Heading).ToList()
                    : context.Contents.Include(c => c.Heading).Where(filter).ToList();
            }
        }
    }
}

