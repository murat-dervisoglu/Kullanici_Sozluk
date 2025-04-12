using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContentManager : IContentService
    {
        IContentDal _contentDal;
        IWriterDal _writerDal;
        IHeadingDal _headingDal;

        public ContentManager(IContentDal contentDal, IWriterDal? writerDal=null, IHeadingDal? headingDal=null)
        {
            _contentDal = contentDal;
            _writerDal = writerDal;
            _headingDal = headingDal;
        }

        public List<Content> GetListForFind(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return _contentDal.List(); 
            }
            return _contentDal.List(x => x.ContentValue.Contains(p));
        }

        public void ContentAdd(Content content)
        {
            _contentDal.Insert(content);
        }

        public void ContentDelete(Content content)
        {
            throw new NotImplementedException();
        }

        public void ContentUpdate(Content content)
        {
            throw new NotImplementedException();
        }

        public Content GetById(int id)
        {
            return _contentDal.Get(x => x.ContentID == id);
        }

        public Heading GetByIDForContent(int id)
        {        
            var heading = _headingDal.Get(x => x.HeadingID == id);
            return heading;
        }

        public List<Content> GetList()
        {
            var contents = _contentDal.List();

            // Eğer Heading verisi yoksa doldurun
            var headings = _headingDal.List();
            var writerlist =_writerDal.List();

            foreach (var content in contents)
            {
                content.Heading = headings.FirstOrDefault(h => h.HeadingID == content.HeadingID) ?? new Heading
                {
                    HeadingName = "Başlık Bulunamadı"
                };
            }
            foreach (var content in contents)
            {
                content.Writer = writerlist.FirstOrDefault(h => h.WriterID == content.WriterID) ?? new Writer
                {
                    WriterName = "Yazar Bulunamadı"
                };
            }
            return contents;
        }

        public List<Content> GetListByHeadingID(int id)
        {
            var contents = _contentDal.List(x => x.HeadingID == id);
            var writers = _writerDal.List();
            var haeading = _headingDal.List();

            foreach (var content in contents)
            {
                content.Writer = writers.FirstOrDefault(w => w.WriterID == content.WriterID) ?? new Writer
                {
                    WriterName = "Yazar Bulunamadı"
                };
                content.Heading = haeading.FirstOrDefault(h => h.HeadingID == content.HeadingID);
            }
        
            return contents;
        }

        public List<Content> GetListByWriter(int id)
        {
            return _contentDal.GetContentsWithHeading(x => x.WriterID == id);
        }

        
    }
}
