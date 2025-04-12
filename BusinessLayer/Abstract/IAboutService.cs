using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public  interface IAboutService
    {
        List<About> GetAbout();
        void AboutAdd(About about);
        void AboutRemove(About about);
        void AboutUpdate(About about);
        About GetByID(int id);
    }
}
