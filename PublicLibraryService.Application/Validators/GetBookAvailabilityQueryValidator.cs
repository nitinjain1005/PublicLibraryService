using FluentValidation;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Queries;

namespace PublicLibraryService.Application.Validators
{
    public class GetBookAvailabilityQueryValidator : AbstractValidator<GetBookAvailabilityQuery>
    {
        public GetBookAvailabilityQueryValidator(BookAvailabilityRequestValidator bookAvailabilityRequestValidator)
        {
            RuleFor(x => x.Filter).SetValidator(bookAvailabilityRequestValidator);
        }
    }

    public class BookAvailabilityRequestValidator : AbstractValidator<BookAvailabilityRequest>
    {
        public BookAvailabilityRequestValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book Id should be greater than zero");

        }
    }
}


