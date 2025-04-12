using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetMessagesTake(string p);  
        List<Message> GetMessagesSend(string p);
        void MessageAdd(Message message);
        void MessageUpdate(Message message);
        void MessageDelete(Message message);
        Message GetByID(int id);
    }
}
