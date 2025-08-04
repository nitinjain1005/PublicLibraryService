using FluentValidation;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Queries;

namespace PublicLibraryService.Application.Validators
{
    public class GetRelatedBorrowedBooksQueryValidator : AbstractValidator<GetRelatedBorrowedBooksQuery>
    {
        public GetRelatedBorrowedBooksQueryValidator(RelatedBorrowedBooksValidator relatedBorrowedBooksValidator)
        {
            RuleFor(x => x.Filter).SetValidator(relatedBorrowedBooksValidator);
        }
    }

    public class RelatedBorrowedBooksValidator : AbstractValidator<RelatedBorrowedBooksRequest>
    {
        public RelatedBorrowedBooksValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book Id should be greater than zero");

        }
    }
}


