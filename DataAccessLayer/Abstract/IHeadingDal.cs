using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IHeadingDal : IRepository<Heading>
    {
        List<Heading> GetHeadingsWithDetails(Expression<Func<Heading, bool>> filter = null);
    }
}
