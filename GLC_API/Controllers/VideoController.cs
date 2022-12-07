using GLC.Core.IUnitOfWork;
using GLC.Core.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GLC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        public readonly IUnitOfWork _UnitOfWork;
        public VideoController(IUnitOfWork UnitOfWork)
        {
            this._UnitOfWork = UnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            try
            {
                List<VideoResource> videos = (List<VideoResource>)await _UnitOfWork.videos.GetAllAsync();
                if (videos != null)
                {
                    return Ok(videos);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByID(Guid id)
        {
            try
            {
                VideoResource video = (VideoResource)await _UnitOfWork.videos.DeleteAsync(id);
                if (video != null)
                {
                    await _UnitOfWork.CompleteAsync();
                    return Ok(video);
                }
                else return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FindById/{id}")]
        public async Task<IActionResult> FindByID(Guid id)
        {
            VideoResource video = (VideoResource)await _UnitOfWork.videos.FindAsync(n => n.VideoId == id);
            if (video != null)
                return Ok(video);
            else return BadRequest();
        }

        [HttpGet("FindByLink/{Link}")]
        public async Task<IActionResult> FindByName(string Link)
        {
            VideoResource video = (VideoResource)await _UnitOfWork.videos.FindAsync(n => n.Link == Link);
            if (video != null)
                return Ok(video);
            else return BadRequest();
        }

        [HttpPost("{id}/{VideoObj}")]
        public async Task<IActionResult> Update(Guid id, VideoResource p)
        {
            await _UnitOfWork.videos.UpdateAsync(id, p);
            if (p != null)
            {
                await _UnitOfWork.CompleteAsync();
                return Ok(await _UnitOfWork.videos.GetAllAsync());
            }
            else return BadRequest();
        }

        [HttpPut("Add/{VideoObj}")]
        public async Task<IActionResult> Add(VideoResource p)
        {
            await _UnitOfWork.videos.AddAsync(p);
            if (p != null)
            {
                await _UnitOfWork.CompleteAsync();
                return Ok(await _UnitOfWork.videos.GetAllAsync());
            }
            else return BadRequest();
        }
    }
}
