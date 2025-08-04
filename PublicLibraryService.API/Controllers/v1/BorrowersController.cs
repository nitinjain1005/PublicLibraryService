using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;
namespace PublicLibraryService.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class BorrowersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BorrowersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// high valued borrowers in the library with in given time frame.
        /// </summary>
        /// <param name="topBorrowersRequest"></param>
        /// <returns></returns>
        [HttpGet("top-borrowers")]
       
        public async Task<ActionResult<IEnumerable<HighValuedBorrower>>> GetTopBorrowers([FromQuery] TopBorrowersRequest topBorrowersRequest)
        {
            var result = await _mediator.Send(new GetHighValuedBorrowersQuery(topBorrowersRequest));
            if (result == null || !result.Any())
                return NotFound();


            return Ok(result);
        }
        /// <summary>
        /// Books Reading Rate by a specific book ID.
        /// </summary>
        /// <param name="bookReadingRateRequest"></param>
        /// <returns></returns>
        [HttpGet("reading-rate")]
        public async Task<ActionResult<ReadingRatePerUser>> GetReadingRate([FromQuery] BookReadingRateRequest bookReadingRateRequest)
        {
            var result = await _mediator.Send(new GetBookReadingRateQuery(bookReadingRateRequest));
            if (result == null)
                return NotFound();


            return Ok(result);
        }
    }
}
