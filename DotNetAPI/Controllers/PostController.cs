using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly IConfiguration _config;

        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = config;
        }

        [HttpGet("Posts")]
        public IEnumerable<Post> GetPosts()
        {
            string sql = $@"select * from Posts";

            return _dapper.LoadData<Post>(sql);
        }

        
        [HttpGet("GetPostSingle/{postId}")]
        public Post GetPostSingle(int postId)
        {
            string sql = $@"select * from Posts where postid = {postId}";

            return _dapper.LoadDataSingle<Post>(sql);
        }

        [HttpGet("PostsByUser/{userId}")]
        public IEnumerable<Post> PostsByUser(int userId)
        {
            string sql = $@"select * from Posts where postid = {userId}";

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> MyPosts()
        {
            // Getting userId from token, User comes from ControllerBase not User Model
            string sql = $@"select * from Posts where postid = {User.FindFirst("userId")?.Value}";

            return _dapper.LoadData<Post>(sql);
        }

        [HttpPost("Post")]
        public IActionResult AddPost(PostToAddDto postToAdd)
        {
            // Getting userId from token, User comes from ControllerBase not User Model
            string sql = $@"Insert into Posts values ({User.FindFirst("userId")?.Value},'{postToAdd.PostTitle}','{postToAdd.PostContent}', GetDate(), GetDate())";
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception ("Failed to create new post");
        }

        [HttpPut("EditPost")]
        public IActionResult EditPost(PostToEditDto postToEdit)
        {
            // Getting userId from token, User comes from ControllerBase not User Model
            string sql = $@"Update Posts set PostTitle = '{postToEdit.PostTitle}', 
                                            PostContent = '{postToEdit.PostContent}',
                                            PostUpdated = GetDate()) 
                                            where postId = {postToEdit.PostId} 
                                            AND userId = {User.FindFirst("userId")?.Value}";

            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception ("Failed to edit post");
        }

        [HttpDelete("DeletePost/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            // Getting userId from token, User comes from ControllerBase not User Model
            string sql = $@"Delete Posts where postId = {postId} AND userId = {User.FindFirst("userId")?.Value}";
                                            
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception ("Failed to delete post");
        }

        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post> PostsBySearch(string searchParam)
        {
            // Getting userId from token, User comes from ControllerBase not User Model
            string sql = $@"select * from Posts where postTitle LIKE '%{searchParam}%'  
                            OR postContent LIKE '%{searchParam}%'";
                        //postid = {User.FindFirst("userId")?.Value}";

            return _dapper.LoadData<Post>(sql);
        }
    }
}