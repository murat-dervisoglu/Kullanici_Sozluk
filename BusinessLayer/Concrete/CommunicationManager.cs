using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommunicationManager : ICommunicationService
    {
        ICommunicationDal _communicationdal;

        public CommunicationManager(ICommunicationDal communicationdal)
        {
            _communicationdal = communicationdal;
        }

        public void CommunicationAdd(Communication communication)
        {
            _communicationdal.Insert(communication);
        }

        public void CommunicationDelete(Communication communication)
        {
            _communicationdal.Delete(communication);
        }

        public void CommunicationUpdate(Communication communication)
        {
            _communicationdal.Update(communication);
        }

        public Communication GetByID(int id)
        {
            return _communicationdal.Get(x=>x.CommunicationID == id);
        }

        public List<Communication> GetList()
        {
            return _communicationdal.List();
        }
    }
}
