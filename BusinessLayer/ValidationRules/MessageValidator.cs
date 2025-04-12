using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator: AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı Mailini Girin");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Mesajın Konusunu Girin");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesajınızı Girin");
        }
    }
}
