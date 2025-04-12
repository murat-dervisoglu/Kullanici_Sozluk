using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommunicationService
    {
        List<Communication> GetList();
        void CommunicationAdd(Communication communication); 
        void CommunicationDelete(Communication communication);
        void CommunicationUpdate(Communication communication);
        Communication GetByID (int id);
    }
}
