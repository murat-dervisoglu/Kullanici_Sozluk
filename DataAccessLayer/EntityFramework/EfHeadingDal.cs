using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfHeadingDal : GenericRepository<Heading>, IHeadingDal
    {
        public List<Heading> GetHeadingsWithDetails(Expression<Func<Heading, bool>> filter = null)
        {
            // Category ve Writer'ı Include ederek çekiyoruz
            return ListWithIncludes(filter, x => x.Category, x => x.Writer);
        }
    }
}
