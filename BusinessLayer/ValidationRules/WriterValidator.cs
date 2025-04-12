using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar Adı Boş Olamaz");
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar Soyadı Boş Olamaz");
            RuleFor(x => x.WriterEmail).NotEmpty().WithMessage("E-mail Alanı Boş Olamaz");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında Boş Olamaz");
            RuleFor(x => x.WriterAbout).MaximumLength(150).WithMessage("150 Karakterden Uzun Olamaz");
        }
    }
}
