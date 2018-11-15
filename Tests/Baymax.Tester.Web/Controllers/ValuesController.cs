using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ValuesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Info>> Get()
        {
            var result = _dbContext.Info.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [Route("/api/post")]
        [HttpPost]
        public IActionResult Post([FromBody] Info info)
        {
            _dbContext.Add(info);
            _dbContext.SaveChanges();

            return Ok(new PostRespDto { Id = info.Id });
        }
        
        [Route("/api/post1")]
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(new PostRespDto { Id = 999 });
        }

        [HttpPut]
        [Route("/api/put/{id}")]
        public IActionResult Put(int id, [FromBody] Info info)
        {
            var existInfo = _dbContext.Info.FirstOrDefault(a => a.Id == id);

            if (existInfo == null)
            {
                return NotFound();
            }

            existInfo.Name = info.Name;
            _dbContext.Info.Update(existInfo);
            _dbContext.SaveChanges();

            return Ok(new PutRespDto { Id = info.Id });
        }

        [Route("/api/delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existInfo = _dbContext.Info.FirstOrDefault(a => a.Id == id);

            if (existInfo == null)
            {
                return NotFound();
            }

            _dbContext.Info.Remove(existInfo);
            _dbContext.SaveChanges();

            return NoContent();
        }
        
        [Route("/api/delete1/{id}")]
        [HttpDelete]
        public IActionResult Delete2(int id)
        {
            var existInfo = _dbContext.Info.FirstOrDefault(a => a.Id == id);

            if (existInfo == null)
            {
                return NotFound();
            }

            _dbContext.Info.Remove(existInfo);
            _dbContext.SaveChanges();

            return Ok(new DeleteRespDto { Id = id });
        }
    }

    public class DeleteRespDto
    {
        public int Id { get; set; }
    }

    public class PutRespDto
    {
        public int Id { get; set; }
    }

    public class PostRespDto
    {
        public int Id { get; set; }
    }
}