using GLC.Core.IUnitOfWork;
using GLC.Core.Resources;
using GLC.Cores.Models;
using GLC.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLC_API.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly GLCDbContext _context;
        public readonly IUnitOfWork _UnitOfWork;
        public TeacherController(IUnitOfWork UnitOfWork, GLCDbContext context)
        {
            this._UnitOfWork = UnitOfWork;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            try
            {
             List<TeacherResource> teachers= (List<TeacherResource>)await _UnitOfWork.teachers.GetAllAsync();
             if(teachers!=null)
             {
                return Ok(teachers);
             }
             else return BadRequest();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByID(Guid id)
        {
            try
            {
               TeacherResource teacher = (TeacherResource)await _UnitOfWork.teachers.DeleteAsync(id);
               if (teacher != null)
               {
                  await _UnitOfWork.CompleteAsync();
                  return Ok(teacher);
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
            TeacherResource teacher = (TeacherResource)await _UnitOfWork.teachers.FindAsync(n=>n.TeacherId==id);
            if (teacher != null)
                return Ok(teacher);
            else return BadRequest();
        }

        [HttpGet("FindByName/{Email}")]
        public async Task<IActionResult> FindByName(string Email)
        {
            TeacherResource teacher = (TeacherResource)await _UnitOfWork.teachers.FindAsync(n => n.Email == Email);
            if (teacher != null)
                return Ok(teacher);
            else return BadRequest();
        }

        [HttpPost("{id}/{TeacherObj}")]
        public async Task<IActionResult> Update(Guid id , TeacherResource p)
        {
            await _UnitOfWork.teachers.UpdateAsync(id,p);
            if (p != null)
            {
                await _UnitOfWork.CompleteAsync();
                return Ok(await _UnitOfWork.teachers.GetAllAsync());
            }
            else return BadRequest();
        }

        [HttpPut("Add/{TeacherObj}")]
        public async Task<IActionResult> Add(TeacherResource p)
        {
            await _UnitOfWork.teachers.AddAsync(p);
            if (p != null)
            {
                await _UnitOfWork.CompleteAsync();
                return Ok(await _UnitOfWork.teachers.GetAllAsync());
            }
            else return BadRequest();
        }

        [HttpPut("AddVideo/{VideoObj}")]
        public async Task<IActionResult> AddVideo(Guid TeacherId,VideoResource p,Guid subject)
        {
            if (p != null)
            {
                p.TeacherId = TeacherId;
                p.SubjectId = subject;
                await _UnitOfWork.videos.AddAsync(p);
                await _UnitOfWork.CompleteAsync();
                return Ok(await _UnitOfWork.videos.GetAllAsync());
            }
            else return BadRequest();
        }
        [HttpGet("teachergroups/{id}")]
        public async Task<IActionResult> GetTeacherGroups(Guid id)
        {
            return Ok(await _context.Teachers.Include("Groups").SingleOrDefaultAsync(s => s.TeacherId == id));
        }
    }
}
