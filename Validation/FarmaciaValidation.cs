using API_Farmacia.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Farmacia.Validation
{
    public class FarmaciaValidation : AbstractValidator<Farmacia>
    {
        public FarmaciaValidation() { 
           RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Informe o nome do Usuario")
            .Length(3,50).WithMessage("O nome deverá ter entre 3 a 50 caracteres");

            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Informe o e-mail do Usuario")
              .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(x => x.Endereco)
           .NotEmpty().WithMessage("Informe o Endereço da Farmacia")
           .Length(10, 70).WithMessage("O Endereço deverá ter entre 10 a 70 caracteres");

        }

    }
}
