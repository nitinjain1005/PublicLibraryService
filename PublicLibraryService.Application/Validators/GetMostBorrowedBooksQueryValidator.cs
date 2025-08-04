using FluentValidation;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;
using System.Security.Principal;

namespace PublicLibraryService.Application.Validators
{
    public class GetMostBorrowedBooksQueryValidator : AbstractValidator<GetMostBorrowedBooksQuery>
    {
        public GetMostBorrowedBooksQueryValidator(MostBorrowedBookValidator validationRules)
        {
            RuleFor(x => x)
              .NotNull()
              .WithMessage("Query cannot be null.");
        }
    }
    public class MostBorrowedBookValidator : AbstractValidator<MostBorrowedBook>
    {
        public MostBorrowedBookValidator()
        {
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
