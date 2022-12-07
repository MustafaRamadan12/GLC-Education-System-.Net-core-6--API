using GLC.Cores.Models;
using GLC.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GLC.Core.IRepositories
{
    public interface ISubject:IGenericRepository<Subject,SubjectResource>
    {
        Task<IEnumerable> Get_All_TeacherId(Guid TeacherID);
    }
}
