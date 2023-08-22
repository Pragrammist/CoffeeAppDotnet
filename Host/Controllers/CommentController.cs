using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CommentController : Controller
    {
        readonly ICommentService _commentService;
        public CommentController(ICommentService commentService) 
        {
            _commentService = commentService;
        }


        [HttpPost]
        public async  Task<IActionResult> CreateComment() 
        {
            
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment() 
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditComment()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetComment(int id)
        {
            return Ok();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetComments(int id)
        {
            return Ok();
        }

        [HttpPost("answer")]
        public async Task<IActionResult> AnswerToComment(int id)
        {
            return Ok();
        }
    }
}
