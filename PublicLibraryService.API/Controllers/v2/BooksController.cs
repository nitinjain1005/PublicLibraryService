using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;

namespace PublicLibraryService.API.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/[controller]")]

    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///  Get Book List which is most borrowed in the library.
        /// </summary>
        /// <returns></returns>
        [HttpGet("most-borrowed")]
        public async Task<ActionResult<IEnumerable<MostBorrowedBook>>> GetMostBorrowed()
        {
            var query = new GetMostBorrowedBooksQuery();
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Get the availability of a specific book by its ID.
        /// </summary>
        /// <param name="bookAvailabilityRequest"></param>
        /// <returns></returns>
        [HttpGet("availability")]
        public async Task<ActionResult<AvailableBook>> GetBookAvailability([FromQuery] BookAvailabilityRequest bookAvailabilityRequest)
        {
            var result = await _mediator.Send(new GetBookAvailabilityQuery(bookAvailabilityRequest));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Retrieves a list of books borrowed by a specific borrower within a specified date range.
        /// </summary>
        /// <param name="borrowedBooksRequest"></param>
        /// <returns></returns>
        [HttpGet("borrowed-books-by-borrowerid")]
        public async Task<ActionResult<IEnumerable<BorrowedBook>>> GetBorrowedBooksByBorrowerId([FromQuery] BorrowedBooksRequest borrowedBooksRequest)
        {
            var query = new GetBorrowedBooksByBorrowerIdQuery(borrowedBooksRequest);

            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Borrowed books who had borrowed specific BookId.
        /// </summary>
        /// <param name="relatedBorrowedBooksRequest"></param>
        /// <returns></returns>
        [HttpGet("related-borrowed-books/{bookId}")]
        public async Task<ActionResult<IEnumerable<RelatedBorrowedBook>>> GetRelatedBorrowedBooks([FromQuery] RelatedBorrowedBooksRequest relatedBorrowedBooksRequest)
        {
            var result = await _mediator.Send(new GetRelatedBorrowedBooksQuery(relatedBorrowedBooksRequest));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

    }
}
