using AutoMapper;
using GLC.Core.IRepositories;
using GLC.Cores.Models;
using GLC.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLC.EF.Repositories
{
    public class Video_Repositry:GenericRepository<Video,VideoResource>,IVideo
    {
        protected readonly GLCDbContext _context;
        protected readonly IMapper _mapper;
        public Video_Repositry(GLCDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
