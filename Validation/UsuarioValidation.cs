using API_Farmacia.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Farmacia.Validation
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation() { 
           RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Informe o nome do Usuario")
            .Length(3,50).WithMessage("O nome deverá ter entre 3 a 50 caracteres");

            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Informe o e-mail do Usuario")
              .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(x => x.Senha)
              .NotEmpty().WithMessage("Informe a senha")
                .Length(6, 50).WithMessage("A senha deverá ter entre 6 a 50 caracteres");

            RuleFor(x => x.FarmaciaID)
              .GreaterThan(0)
              .WithMessage("Farmacia ID não pode ser vazio");
        }

    }
}
