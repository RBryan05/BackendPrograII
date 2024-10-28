using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("Debe poseer entre 2 a 20 caracteres.");
            RuleFor(x => x.BrandId).NotNull().WithMessage("Debe ingresar una marca");
            RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("La marca debe esta registrada");
            RuleFor(x => x.Al).NotEmpty().WithMessage(x => $"El nivel de alcohol ({x.Al}) debe ser legar");
        }
    }
}
