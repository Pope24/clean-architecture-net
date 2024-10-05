using CarRentalSystem.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        T FindInclude(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        T FindNoTraking(Expression<Func<T, bool>> criteria, string[] includes = null);
        T FindNoTraking(Expression<Func<T, bool>> criteria, Expression<Func<T, bool>> criteria2, string[] includes = null);
        T FindNoTraking(Expression<Func<T, bool>> criteria, Expression<Func<T, bool>> criteria2, Expression<Func<T, bool>> criteria3, string[] includes = null);
        T FindNoTraking(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy = null);

        T FindNoTraking(Expression<Func<T, bool>> criteria, Expression<Func<T, bool>> criteria2,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, string[] includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAlike(Expression<Func<T, bool>> criteria, string[] includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        IEnumerable<T> FindAllInclude(string[] includes = null);
        IEnumerable<T> FindAllInclude(int skip, int take, string[] includes = null);
        IEnumerable<T> FindAllInclude(Expression<Func<T, object>> orderBy, int skip, int take, string[] includes = null);
        IQueryable<T> GetAllNolist();
        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        //Task<T> UpdateAsync(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        //Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

        Task<IEnumerable<T>> GetChildsAsync(Expression<Func<T, bool>> include, Expression<Func<T, bool>> criteria);

        bool isExist(Expression<Func<T, bool>> criteria);

        //Task<string> ChildCoa(string HeadName, string HeadCode, string coa);

        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();

        IQueryable<T> FilterData(Func<IQueryable<T>, IQueryable<T>> filterFunc, BaseFilteration parameters);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
        void SaveChanges();
    }
    public static class OrderBy
    {
        public const string Ascending = "ASC";
        public const string Descending = "DESC";
    }
}
