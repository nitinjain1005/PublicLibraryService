using MediatR;
using PublicLibraryService.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Queries
{
    public class GetMostBorrowedBooksQuery : IRequest<List<MostBorrowedBook>> 
    { 
    
    
    }
   
}
