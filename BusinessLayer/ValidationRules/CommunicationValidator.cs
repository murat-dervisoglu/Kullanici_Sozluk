using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CommunicationValidator : AbstractValidator<Communication>
    {
        public CommunicationValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Mail Adı Boş Olamaz");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Mesaj Başlığı Girin");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj Alanı Boş Olamaz");
            RuleFor(x => x.UserMail).MaximumLength(100).WithMessage("En Fazla 100 Karakter Girebilirsiniz");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("En Fazla 100 Karakter Girebilirsiniz");
            RuleFor(x => x.UserName).MaximumLength(100).WithMessage("En Fazla 100 Karakter Girebilirsiniz");
            RuleFor(x => x.Message).MaximumLength(1000).WithMessage("En Fazla 1.000 Karakter Girebilirsiniz");
        }
    }
}