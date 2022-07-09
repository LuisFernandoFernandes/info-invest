using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using trade.application.generics.models;
using trade.application.generics.services;

namespace trade.application.generics.controllers
{
    public class GenericController<TModelo> : ApiController where TModelo : class, IGenericModel
    {

        public IGenericService<TModelo> _service;

        public GenericController(IGenericService<TModelo> service)
        {
            _service = service;
            Thread.CurrentPrincipal = User;
        }


        public IHttpActionResult GenericResult<T>(IGenericResult<T> response, Func<IGenericResult<T>, object> minify = null)
        {
            return Ok(minify != null ? minify(response) : response);
        }

        public IHttpActionResult OkResult<T>(T response, Func<IGenericResult<T>, object> minify = null)
        {
            return GenericResult(new OkResult<T>(response, null), minify);
        }

        public IHttpActionResult InvalidResult(string response, Func<IGenericResult<TModelo>, object> minify = null)
        {
            return GenericResult(new InvalidResult<TModelo>(response), minify);
        }

        #region CRUD
        [HttpPost] //Create
        public virtual async Task<IHttpActionResult> PostIncluirAsync([FromBody] TModelo obj)
        {
            await _service.IncluirAsync(obj);
            return OkResult(obj);
        }

        [HttpGet]
        [Route("{id}")] //GetOne
        public virtual async Task<IHttpActionResult> GetOne([FromUri] string id)
        {
            return OkResult(await _service.AsQueryable().Where(a => a.Id == id).FirstOrDefaultAsync());
        }


        [HttpGet] //GetAll
        public virtual async Task<IHttpActionResult> Get()
        {
            return OkResult(await _service.AsQueryable().ToListAsync());
        }


        [HttpPatch] //Update
        public virtual async Task<IHttpActionResult> Update([FromBody] TModelo obj)
        {
            await _service.AlterarAsync(obj);
            return OkResult(obj);
        }


        [HttpDelete]
        [Route("{id}")] //Delete
        public virtual async Task<IHttpActionResult> Delete([FromUri] string id)
        {
            await _service.ExcluirAsync(id);
            return OkResult<TModelo>(null);
        }

        #endregion

    }
}