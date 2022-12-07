using AutoMapper;
using GLC.Core.IRepositories;
using GLC.Core.Resources;
using GLC.Cores.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GLC.EF.Repositories
{
    public class Teacher_Repositry : GenericRepository<Teacher, TeacherResource>, ITeacher
    {
        protected readonly GLCDbContext _context;
        protected readonly IMapper _mapper;
        public Teacher_Repositry(GLCDbContext context, IMapper mapper):base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
