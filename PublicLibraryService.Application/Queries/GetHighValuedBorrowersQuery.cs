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
    public class GetHighValuedBorrowersQuery : IRequest<List<HighValuedBorrower>>
    {
        public TopBorrowersRequest Filter { get; set; }

        public GetHighValuedBorrowersQuery(TopBorrowersRequest topBorrowersRequest)
        {
            Filter = topBorrowersRequest;
        }
    }

}
