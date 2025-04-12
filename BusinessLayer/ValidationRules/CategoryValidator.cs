using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x =>x.CategoryName).NotEmpty().WithMessage("Kategori Adı Boş Olamaz");
            RuleFor(x =>x.CategoryDescription).NotEmpty().WithMessage("Açıklama Alanı Boş Olamaz");
            RuleFor(x =>x.CategoryName).MaximumLength(500).WithMessage("Kategori Adı 500 Karakterden Uzun Olamaz");
            
        }
    }
}
