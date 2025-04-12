using EntityLayer.Concrete;

namespace Mvc_Proje.ViewModels
{
    public class HeadingContentViewModel
    {
        public List<Heading> Headings
        {
            get; set;
        }
        public List<Content> Contents
        {
            get; set;
        }
        public List<Writer> Writers
        {
            get; set;
        }
    }
}
