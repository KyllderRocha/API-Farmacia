using API_Farmacia.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Farmacia.Validation
{
    public class RemedioValidation : AbstractValidator<Remedio>
    {
        public RemedioValidation() { 
           RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Informe o nome do Usuario")
            .Length(3,50).WithMessage("O nome deverá ter entre 3 a 50 caracteres");

            RuleFor(x => x.Validade)
                .NotEmpty().WithMessage("Informe a data de validade do remedio");

            RuleFor(x => x.FarmaciaID)
              .GreaterThan(0)
              .WithMessage("Farmacia ID não pode ser vazio");

            RuleFor(x => x.CategoriaID)
              .GreaterThan(0)
              .WithMessage("Categoria ID não pode ser vazio");
        }

    }
}
