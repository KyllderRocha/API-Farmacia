using API_Farmacia.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Farmacia.Validation
{
    public class CategoriaValidation : AbstractValidator<CategoriaRemedio>
    {
        public CategoriaValidation() { 
           RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Informe o nome da categoria")
            .Length(3,50).WithMessage("O nome deverá ter entre 3 a 50 caracteres");
        }

    }
}
