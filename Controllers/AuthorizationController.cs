using Core.Entities;
using Infrastrusture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AllPrime.EndPoint.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [RoutePrefix("api/authorization")]
    public class AuthorizationController : ApiController
    {


        //private readonly AccessPointRepository _accessPointRepository;

        // GET api/values
        [Route("GetAuthorization")]
        public IHttpActionResult GetAuthorization()
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Authorizations.ToList();

                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("GetAuthorization/{id:int:min(1)}")]
        // GET api/values/5
        public IHttpActionResult GetAuthorization(int id)

        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Authorizations.FirstOrDefault(e => e.AccessServiceId == id);
                    if (resp != null)
                    {
                        return Ok(resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Authorization with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

       
        // POST api/values
        [Route("CreateAuthorization")]
        [HttpPost]
        public IHttpActionResult CreateAccessPoint([FromBody] Authorizations access)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    integ.Authorizations.Add(access);
                    integ.SaveChanges();

                    var mess = Request.CreateResponse(HttpStatusCode.Created, access);
                    mess.Headers.Location = new Uri(Request.RequestUri + access.AccessServiceId.ToString());
                    return Ok(mess);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/values/5
        [Route("UpdateAuthorization/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Authorizations access)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Authorizations.FirstOrDefault(e => e.AccessServiceId == id);
                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Authorization with ID " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        resp.Id = access.Id;
                        resp.Descrip = access.Descrip;

                        integ.SaveChanges();

                        return Ok(resp);
                    }

                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/values/5
        [Route("DeleteAuthorization/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult DeleteAuthorization(int id)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Authorizations.FirstOrDefault(e => e.AccessServiceId == id);

                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Authorization with ID " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        integ.Authorizations.Remove(resp);
                        integ.SaveChanges();

                        return Ok(resp);
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }

        }
    }
}
