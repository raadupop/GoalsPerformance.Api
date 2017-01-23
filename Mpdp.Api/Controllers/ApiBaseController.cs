using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;

namespace Mpdp.Api.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class ApiBaseController : ApiController
  {
    protected readonly IEntityBaseRepository<Error> ErrorsRepository;
    protected readonly IUnitOfWork UnitOfWork;

    public ApiBaseController(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
    {
      ErrorsRepository = errorsRepository;
      UnitOfWork = unitOfWork;
    }

    protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
    {
      HttpResponseMessage response;

      try
      {
        response = function.Invoke();
      }
      catch (DbUpdateException ex)
      {
        LogError(ex);
        response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
      }
      catch (Exception ex)
      {
        LogError(ex);
        response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
      }

      return response;
    }

    private void LogError(Exception ex)
    {
      try
      {
        var error = new Error()
        {
          Message = ex.Message,
          StackTrace = ex.StackTrace,
          DateCreated = DateTime.Now
        };

        ErrorsRepository.Add(error);
        UnitOfWork.Commit();
      }
      catch
      {
      }
    }
  }
}
