using AutoMapper;
using GLC.Core.IUnitOfWork;
using GLC.Core.Resources;
using GLC.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLC_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
        private GLCDbContext _context;
    public StudentsController(IUnitOfWork unitOfWork, GLCDbContext context, IMapper mapper)
    {
      _unitOfWork = unitOfWork;

            _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        return Ok(await _unitOfWork.Students.GetAllAsync());
      }
      catch (NullReferenceException ex)
      {
        throw new NullReferenceException("No Students are found!", ex);
      }
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(Guid id)
    {
      try
      {
        return Ok(await _unitOfWork.Students.GetByIdAsync(id));
      }
      catch (NullReferenceException ex)
      {
        throw new NullReferenceException("No Student is found!", ex);
      }

    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
      try
      {
        var student = await _unitOfWork.Students.FindAsync(s => s.Name == name);

        return Ok(student);
      }
      catch (NullReferenceException ex)
      {
        throw new NullReferenceException("No students found by this name", ex);
      }


    }
    [HttpGet("/emailExist/{email}")]
    public async Task<bool> IsEmailExists(string email)
    {
      try
      {
        var result = await GetByEmail(email);
        Console.WriteLine(email);
        return true;

      }
      catch (NullReferenceException ex)
      {

        return false;
      }

    }

    [HttpGet("/nameExist/{name}")]
    public async Task<bool> IsNameExists(string name)
    {
      try
      {
        var result = await GetByName(name);
        return true;

      }
      catch (NullReferenceException ex)
      {

        return false;
      }

    }



    [HttpGet("/email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
      try
      {
        var student = await _unitOfWork.Students.FindAsync(s => s.Email == email);

        return Ok(student);
      }
      catch (NullReferenceException ex)
      {
        throw new NullReferenceException("No students found by this email", ex);
      }


    }



    [HttpPost("{student}")]

    public async Task<IActionResult> AddStudent([FromBody] StudentResource student)
    {
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
      try
      {
        var student = await _unitOfWork.Students.DeleteAsync(id);
        int x = await _unitOfWork.CompleteAsync();
        return Ok(student);
      }
      catch (NullReferenceException ex)
      {
        throw new NullReferenceException("this student is not found", ex);
      }
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateStudent(Guid id, StudentResource student)
    {
      try
      {
        var studentToBeUpdated = await _unitOfWork.Students.UpdateAsync(id, student);
        await _unitOfWork.CompleteAsync();
        return Ok(studentToBeUpdated);
      }
      catch (NullReferenceException ex)
      {
        throw new NullReferenceException("No Student to be updated.", ex);
      }
    }

    [HttpGet("studentgroup/{id}")]
    public async Task<IActionResult>GetstudentsGroup(Guid id)
    {
            return Ok(await _context.Students.Include("Group").SingleOrDefaultAsync(s=>s.StudentId==id));
    }
  }

}
