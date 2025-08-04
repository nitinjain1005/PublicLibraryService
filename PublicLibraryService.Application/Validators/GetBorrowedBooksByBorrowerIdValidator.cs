using FluentValidation;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Queries;

namespace PublicLibraryService.Application.Validators
{
    public class GetBorrowedBooksByBorrowerIdQueryValidator : AbstractValidator<GetBorrowedBooksByBorrowerIdQuery>
    {
        public GetBorrowedBooksByBorrowerIdQueryValidator(BorrowedBookRequestValidator borrowedBookRequestValidator)
        {
            RuleFor(x => x.Filter).SetValidator(borrowedBookRequestValidator);
        }
    }
    public class BorrowedBookRequestValidator : AbstractValidator<BorrowedBooksRequest>
    {
        public BorrowedBookRequestValidator()
        {
            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("FromDate must not be in the future.");

            RuleFor(x => x.BorrowerId)
                .GreaterThan(0).WithMessage("BorrowerId must be a positive number.");
        }
    }
}
