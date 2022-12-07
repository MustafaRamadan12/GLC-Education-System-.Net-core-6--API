using GLC.Core.IUnitOfWork;
using GLC.Core.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GLC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        public readonly IUnitOfWork _UnitOfWork;
        public SubjectController(IUnitOfWork UnitOfWork)
        {
            this._UnitOfWork = UnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            try
            {
                List<SubjectResource> subjects = (List<SubjectResource>)await _UnitOfWork.subject.GetAllAsync();
                if (subjects != null)
                {
                    return Ok(subjects);
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
                SubjectResource subject = (SubjectResource)await _UnitOfWork.subject.DeleteAsync(id);
                if (subject != null)
                {
                    await _UnitOfWork.CompleteAsync();
                    return Ok(subject);
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
            SubjectResource subject = (SubjectResource)await _UnitOfWork.subject.FindAsync(n => n.SubjectId == id);
            if (subject != null)
                return Ok(subject);
            else return BadRequest();
        }

        [HttpGet("FindAllByTeacherID/{TeacherID}")]
        public async Task<IActionResult> FindByTeacherID(Guid TeacherId)
        {
           List<SubjectResource >subject = (List<SubjectResource>)await _UnitOfWork.subject.Get_All_TeacherId(TeacherId);
            if (subject != null)
                return Ok(subject);
            else return BadRequest();
        }

        [HttpPost("Update/{id}/{SubjectObj}")]
        public async Task<IActionResult> Update(Guid id, SubjectResource p)
        {
            await _UnitOfWork.subject.UpdateAsync(id, p);
            if (p != null)
            {
                await _UnitOfWork.CompleteAsync();
                return Ok(await _UnitOfWork.subject.GetAllAsync());
            }
            else return BadRequest();
        }

        [HttpPut("Add/{SubjectObj}")]
        public async Task<IActionResult> Add(SubjectResource p)
        {
            await _UnitOfWork.subject.AddAsync(p);
            if (p != null)
            {
                await _UnitOfWork.CompleteAsync();
                return Ok(await _UnitOfWork.subject.GetAllAsync());
            }
            else return BadRequest();
        }

       
    }
}
