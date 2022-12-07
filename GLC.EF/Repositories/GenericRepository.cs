using AutoMapper;
using GLC.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GLC.EF.Repositories
{
    public class GenericRepository<TEntity, TEntityResource> : IGenericRepository<TEntity, TEntityResource> where TEntity : class where TEntityResource : class
    {
        protected readonly GLCDbContext _context;
        protected readonly IMapper _mapper;
        public GenericRepository(GLCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // adding new object.
        //public async Task AddAsync(TEntity entity)
        //{
        //    await _context.Set<TEntity>().AddAsync(entity);
        //}



        public async Task<TEntityResource> AddAsync(TEntityResource entity)
        {
            var obj = _mapper.Map<TEntityResource, TEntity>(entity);
            await _context.Set<TEntity>().AddAsync(obj);

            return _mapper.Map<TEntity, TEntityResource>(obj);
        }

        public async Task<TEntityResource> DeleteAsync(Guid id)
        {
            var obj = await _context.Set<TEntity>().FindAsync(id);
            if (obj == null)
                throw new NullReferenceException();
            else
            {
                var objectToBeDeleted = _mapper.Map<TEntity, TEntityResource>(obj);
                _context.Set<TEntity>().Remove(obj);
                return objectToBeDeleted;
            }
        }

        //deleting new object.
        //public async Task DeleteAsync(Guid id)
        //{
        //    var obj = await GetByIdAsync(id);
        //    if (obj == null)
        //        throw new NullReferenceException();
        //    else
        //        _context.Set<TEntity>().Remove(obj);
        //}

        // find object according to condition.
        //public async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        //{
        //    var obj = await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        //    if (obj == null)
        //        throw new NullReferenceException();
        //    else
        //        return obj;
        //}

        // get all objects.
        //public async Task<IEnumerable<TEntity>> GetAllAsync()
        //{
        //    var obj = await _context.Set<TEntity>().ToListAsync();
        //    if (obj == null)
        //        throw new NullReferenceException();
        //    else
        //        return obj;

        //}

        // get object by Id.
        //public async Task<TEntity> GetByIdAsync(Guid id)
        //{
        //    var obj = await _context.Set<TEntity>().FindAsync(id);
        //    if (obj == null)
        //        throw new NullReferenceException();
        //    else
        //        return obj;
        //}

        //update existing object.
        //public async Task UpdateAsync(Guid id, TEntity entity)
        //{
        //    var obj = await GetByIdAsync(id);
        //    if (obj == null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    else
        //    {
        //        obj = entity;
        //        _context.Set<TEntity>().Update(obj);
        //    }
        //}


        public async Task<TEntityResource> UpdateAsync(Guid id, TEntityResource entity)
        {
            var obj = await _context.Set<TEntity>().FindAsync(id);
            if (obj == null)
                throw new NullReferenceException();
            else
            {
                var updatedEntity = _mapper.Map<TEntityResource, TEntity>(entity, obj);
                return _mapper.Map<TEntity, TEntityResource>(updatedEntity);
            }

        }

        public async Task<TEntityResource> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var obj = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (obj == null)
                throw new NullReferenceException();
            else
            {
                return _mapper.Map<TEntity, TEntityResource>(obj);
            }
        }
        public async Task<IEnumerable<TEntityResource>> GetAllAsync()
        {

            var objs = await _context.Set<TEntity>().ToListAsync();
            if (objs == null)
                throw new NullReferenceException();
            else
            {
                return _mapper.Map<List<TEntity>, List<TEntityResource>>(objs);
            }
        }

        public async Task<TEntityResource> GetByIdAsync(Guid id)
        {
            var obj = await _context.Set<TEntity>().FindAsync(id);
            if (obj == null)
                throw new NullReferenceException();
            else
            {
                return _mapper.Map<TEntity, TEntityResource>(obj);
            }

        }

    }
}