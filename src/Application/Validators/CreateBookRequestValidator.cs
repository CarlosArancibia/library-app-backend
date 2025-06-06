using FluentValidation;
using Application.DTOs.Requests;

namespace Application.Validators
{
    public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título es obligatorio")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("El autor es obligatorio")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres");

            RuleFor(x => x.PublishYear)
                .InclusiveBetween(1500, DateTime.Now.Year)
                .WithMessage("El año debe estar entre 1500 y el año actual");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("El género es obligatorio");
        }
    }
}
