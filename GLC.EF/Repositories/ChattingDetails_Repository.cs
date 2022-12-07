using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLC.Core.Resources;
using GLC.Core.IRepositories;
using GLC.Cores.Models;
namespace GLC.EF.Repositories
{
    public class ChattingDetails_Repository:GenericRepository<ChatingDetails,ChattingDetailsResource>,IChatinggDetales
    {
        protected readonly GLCDbContext _context;
        protected readonly IMapper _mapper;
        public ChattingDetails_Repository(GLCDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
