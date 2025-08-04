using FluentValidation;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Queries;

namespace PublicLibraryService.Application.Validators
{
    public class GetBookReadingRateQueryValidator : AbstractValidator<GetBookReadingRateQuery>
    {
        public GetBookReadingRateQueryValidator(BookReadingRateRequestValidator bookReadingRateRequestValidator)
        {
            RuleFor(x => x.Filter).SetValidator(bookReadingRateRequestValidator);
        }
    }

    public class BookReadingRateRequestValidator : AbstractValidator<BookReadingRateRequest>
    {
        public BookReadingRateRequestValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book Id should be greater than zero");

        }
    }
}


