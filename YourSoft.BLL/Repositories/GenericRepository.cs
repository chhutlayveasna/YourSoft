using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using YourSoft.BLL.Contracts;
using YourSoft.BLL.Models;
using YourSoft.DAL.Data;

namespace YourSoft.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
        {
            if (queryParameters.Page == 0)
                queryParameters.Page = 1;

            var totalSize = (queryParameters.SearchBy != "") ? await _context.Set<T>().Where(x => EF.Property<object>(x, queryParameters.SearchBy).Equals(queryParameters.Search)).CountAsync() : await _context.Set<T>().CountAsync();
            var pageCount = (double)((decimal)totalSize / Convert.ToDecimal(queryParameters.PageSize));

            var data = new List<TResult>();

            if (queryParameters.SortBy != "" && queryParameters.SortBy.ToLower() == "asc")
            {
                if (queryParameters.SearchBy != "")
                {
                    data = await _context.Set<T>()
                    .OrderBy(x => EF.Property<object>(x, queryParameters.OrderBy))
                    //.Where(x => EF.Property<object>(x, queryParameters.SearchBy).Contains(queryParameters.Search))
                    .Where(x => EF.Property<object>(x, queryParameters.SearchBy).Equals(queryParameters.Search))
                    .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize)
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
                else
                {
                    data = await _context.Set<T>()
                    .OrderBy(x => EF.Property<object>(x, queryParameters.OrderBy))
                    .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize)
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
            }
            else if (queryParameters.SortBy != "" && queryParameters.SortBy.ToLower() == "desc")
            {
                if (queryParameters.SearchBy != "")
                {
                    data = await _context.Set<T>()
                    .OrderByDescending(x => EF.Property<object>(x, queryParameters.OrderBy))
                    .Where(x => EF.Property<object>(x, queryParameters.SearchBy).Equals(queryParameters.Search))
                    .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize)
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
                else
                {
                    data = await _context.Set<T>()
                    .OrderByDescending(x => EF.Property<object>(x, queryParameters.OrderBy))
                    .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize)
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
            }
            else
            {
                if (queryParameters.SearchBy != "")
                {
                    data = await _context.Set<T>()
                    .Where(x => EF.Property<object>(x, queryParameters.SearchBy).Equals(queryParameters.Search))
                    .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize)
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
                else
                {
                    data = await _context.Set<T>()
                    .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize)
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
            }

            return new PagedResult<TResult>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "success",
                Data = data,
                Page = queryParameters.Page,
                PageSize = queryParameters.PageSize,
                PageCount = (int)Math.Ceiling(pageCount),
                TotalRecords = totalSize,
                HasNext = (queryParameters.Page < (int)Math.Ceiling(pageCount)) ? true : false,
                HasPrevious = (queryParameters.Page > 1) ? true : false,
                FirstPage = GetUri("FirstPage", (queryParameters.Page < (int)Math.Ceiling(pageCount)) ? true : false, (queryParameters.Page > 1) ? true : false, queryParameters.Page, queryParameters.PageSize, (int)Math.Ceiling(pageCount)),
                LastPage = GetUri("LastPage", (queryParameters.Page < (int)Math.Ceiling(pageCount)) ? true : false, (queryParameters.Page > 1) ? true : false, queryParameters.Page, queryParameters.PageSize, (int)Math.Ceiling(pageCount)),
                NextPage = GetUri("NextPage", (queryParameters.Page < (int)Math.Ceiling(pageCount)) ? true : false, (queryParameters.Page > 1) ? true : false, queryParameters.Page, queryParameters.PageSize, (int)Math.Ceiling(pageCount)),
                PreviousPage = GetUri("PreviousPage", (queryParameters.Page < (int)Math.Ceiling(pageCount)) ? true : false, (queryParameters.Page > 1) ? true : false, queryParameters.Page, queryParameters.PageSize, (int)Math.Ceiling(pageCount)),
            };
        }

        private Uri GetUri(string action, bool hasNext, bool hasPrevious, int page, int pageSize, int pageCount)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Port = (int)request.Host.Port;
            uriBuilder.Path = request.Path.ToString();
            if (action == "NextPage")
            {
                if (hasNext)
                {
                    uriBuilder.Query = "?page=" + (page + 1) + "&pageSize=" + pageSize;
                    return uriBuilder.Uri;
                }
            }
            else if (action == "PreviousPage")
            {
                if (hasPrevious)
                {
                    uriBuilder.Query = "?page=" + (page - 1) + "&pageSize=" + pageSize;
                    return uriBuilder.Uri;
                }
            }
            else if (action == "FirstPage")
            {
                uriBuilder.Query = "?page=1&pageSize=" + pageSize;
                return uriBuilder.Uri;
            }
            else if (action == "LastPage")
            {
                uriBuilder.Query = "?page=" + pageCount + "&pageSize=" + pageSize;
                return uriBuilder.Uri;
            }
            return null;
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
                return null;

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
