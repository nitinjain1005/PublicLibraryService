using MediatR;
using PublicLibraryService.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicLibraryService.Application.Models.Request;

namespace PublicLibraryService.Application.Queries
{
    public class GetBookReadingRateQuery : IRequest<BookReadingRate>
    {
        public BookReadingRateRequest Filter { get; set; }

        public GetBookReadingRateQuery(BookReadingRateRequest readingRateRequest)
        {
            Filter = readingRateRequest;
        }
    }
}
