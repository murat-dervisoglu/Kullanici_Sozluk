using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IHeadingService
    {
        List<Heading> getlist();
        List<Heading> GetHeadingList();
        List<Heading> GetHeadingListByWriter(int id);
        void HeadingAdd(Heading heading);
        Heading GetById(int id);
        void HeadingRemove(Heading heading);
        void HeadingUpdate (Heading heading);
    }
}
