using GLC.Cores.Models;
using GLC.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLC.Core.IRepositories;
using AutoMapper;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace GLC.EF.Repositories
{
    public class Subject_Repositary:GenericRepository<Subject,SubjectResource>,ISubject
    {
         protected readonly GLCDbContext _context;
         protected readonly IMapper _mapper;
         public Subject_Repositary(GLCDbContext context, IMapper mapper) : base(context, mapper)
         {
             _context = context;
             _mapper = mapper;
         }

        public async Task<IEnumerable> Get_All_TeacherId(Guid TeacherID)
        {
           List<Subject>subjects= await  _context.Subjects.Where(n=>n.TeacherId==TeacherID).ToListAsync();
            return subjects;
        }
    }
}
