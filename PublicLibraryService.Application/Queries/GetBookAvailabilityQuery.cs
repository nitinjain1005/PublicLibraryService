using MediatR;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Queries
{
    public class GetBookAvailabilityQuery : IRequest<AvailableBook>
    {
        public BookAvailabilityRequest  Filter { get; set; }
        public GetBookAvailabilityQuery(BookAvailabilityRequest bookAvailabilityRequest)
        {
            Filter = bookAvailabilityRequest;
        }
    }
}
