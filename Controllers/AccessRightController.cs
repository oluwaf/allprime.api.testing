using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Entities;
using Infrastrusture.Data;
using System.Web.Http.Cors;
using System.Data.Entity;
using AutoMapper;

namespace AllPrime.EndPoint.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [RoutePrefix("api/accessright")]
    public class AccessRightController : ApiController
    {

        //private readonly AccessPointRepository _accessPointRepository;

        // GET api/values
        [Route("GetAccessRights")]
        public IHttpActionResult GetAccessRights()
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Accessright.ToList();

                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("GetAccessRights/{id:int:min(1)}")]
        // GET api/values/5
        public IHttpActionResult GetAccessRights(int id)

        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Accessright.FirstOrDefault(e => e.AccessRightId == id);
                    if (resp != null)
                    {
                        return Ok(resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "AccessRight with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("GetAccessRightsByProfile/{id:int:min(1)}")]
        // GET api/values/5
        public IHttpActionResult GetAccessRightsByProfile(int id)

        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Accessrightaccessprofiles.Select(lk => new
                    {
                        AccessProfileID= lk.AccessProfileId,
                        AccessRightID = lk.AccessRight,
                        _AccessRightName = lk.AccessRight.AccessRightName,
                        AccessCreationDate = lk.AccessRight._CreationDate

                    }).Where(e => e.AccessProfileID == id).ToList();
                    if (resp != null)
                    {
                        return Content(HttpStatusCode.OK, resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "AccessProfile with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // POST api/values
        [Route("CreateAccessRight")]
        [HttpPost]
        public IHttpActionResult CreateAccessPoint([FromBody] Accessright access)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    integ.Accessright.Add(access);
                    integ.SaveChanges();

                    var mess = Request.CreateResponse(HttpStatusCode.Created, access);
                    mess.Headers.Location = new Uri(Request.RequestUri + access.AccessRightId.ToString());
                    return Ok(mess);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/values/5
        [Route("UpdateAccessRight/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Accessright access)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Accessright.FirstOrDefault(e => e.AccessRightId == id);
                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "AccessRight with ID " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        resp.AccessRightName = access.AccessRightName;
                        resp._CreationDate = access._CreationDate;
                        resp._UpdatedDate = access._UpdatedDate;
          
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
        [Route("DeleteAccessRight/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult DeleteAccessRight(int id)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Accessright.FirstOrDefault(e => e.AccessRightId == id);

                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "AccessRight with ID " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        integ.Accessright.Remove(resp);
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
