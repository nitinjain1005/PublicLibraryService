using FluentValidation;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Validators
{
    public class GetHighValuedBorrowersQueryValidator : AbstractValidator<GetHighValuedBorrowersQuery>
    {
        public GetHighValuedBorrowersQueryValidator(TopBorrowersRequestValidator topBorrowersRequestValidator)
        {
            RuleFor(x => x.Filter).SetValidator(topBorrowersRequestValidator);
        }

    }
    public class TopBorrowersRequestValidator:AbstractValidator<TopBorrowersRequest>
    {

        public TopBorrowersRequestValidator()
        {
            RuleFor(x => x.FromDate).LessThanOrEqualTo(x => x.ToDate)
                .WithMessage("FromDate must be before or equal to ToDate");
            RuleFor(x => x.FromDate).NotNull().WithMessage("FromDate is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("FromDate cannot be in the future.");
        }
    }
}
