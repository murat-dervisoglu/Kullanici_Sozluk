using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Concrete
{
    public class HeadingManager : IHeadingService
    {
        IHeadingDal _headingDal;
        ICategoryDal _categoryDal;
        IWriterDal _writerDal;
        IContentDal _contentDal;

        public HeadingManager(IHeadingDal headingDal, ICategoryDal? categoryDal= null, IWriterDal? writerDal = null, IContentDal? contentDal =null)
        {
            _headingDal = headingDal;
            _categoryDal = categoryDal;
            _writerDal = writerDal;
            _contentDal = contentDal;
        }

        public Heading GetById(int id)
        {
            return _headingDal.Get(x => x.HeadingID == id);
        }

        public List<Heading> GetHeadingList()
        {

            var headings = _headingDal.List();

            var categories = _categoryDal.List();

            var writers = _writerDal.List();   

            foreach (var heading in headings)
            {
                // Null kontrolü ekliyoruz
                heading.Category = categories.FirstOrDefault(c => c.CategoryID == heading.CategoryID) ?? new Category
                {
                    CategoryName = "Kategori Yok"
                };
                heading.Writer = writers.FirstOrDefault(c => c.WriterID == heading.WriterID) ?? new Writer
                {
                    WriterName = "yazar bulunamadı"
                };

            }
            return headings;
        }

        public List<Heading> GetHeadingList2()
        {
            return _headingDal.GetHeadingsWithDetails();
        }

        public List<Heading> GetHeadingListByWriter(int id)
        {
            return _headingDal.GetHeadingsWithDetails(x => x.WriterID == id);
        }

        public List<Heading> getlist()
        {
            var headings = _headingDal.List();
            return headings;
        }

        public void HeadingAdd(Heading heading)
        {
            _headingDal.Insert(heading);
        }

        public void HeadingRemove(Heading heading)
        {
            _headingDal.Update(heading);
        }

        public void HeadingUpdate(Heading heading)
        {
            _headingDal.Update(heading);
        }
        public void UpStatu(Heading heading)
        {
            _headingDal.Update(heading);
        }

    }
}


