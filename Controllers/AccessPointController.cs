using Core.Entities;
using Infrastrusture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;
using AutoMapper;
using Core.DTOs;

namespace AllPrime.EndPoint.Controllers
{
   
    [EnableCorsAttribute("*","*","*")]
    [RoutePrefix("api/accesspoint")]
    public class AccessPointController : ApiController
    {
        //private readonly AccessPointRepository _accessPointRepository;

        // GET api/values
        [Route("GetAccessPoint")]
        [HttpGet]
        public IHttpActionResult GetAccessPoint()
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.AccessPoints.ToList();
                    
                    return Ok(resp);
                }
            }
            catch(Exception ex) 
            {

                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("GetAccessPoint/{id:int:min(1)}")]
        [HttpGet]
        // GET api/values/5
        public IHttpActionResult GetAccessPoint(int id)

        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.AccessPoints.FirstOrDefault(e => e.AP_ID == id);
                    if (resp != null)
                    {
                        return Ok(resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "AccessPoint with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("GetAccessPointByGroup/{id:int:min(1)}")]
        // GET api/values/5
        public IHttpActionResult GetAccessPointByGroup(int id)

        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.AccessPoints.FirstOrDefault(e => e.AP_GROUP_ID == id);
                    if (resp != null)
                    {
                        return Ok(resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "AccessPointGroup with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // POST api/values
        [Route("CreateAccessPoint")]
        [HttpPost]
        public IHttpActionResult CreateAccessPoint(AccessPoints access)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    integ.AccessPoints.Add(access);
                    integ.SaveChanges();

                    var mess = Content(HttpStatusCode.Created, "Submited successfully");
                    //mess.Headers.Location = new Uri(Request.RequestUri + access.AP_ID.ToString());
                    return Ok(mess);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/values/5
        [Route("UpdateAccessPoint/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] AccessPoints access)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.AccessPoints.FirstOrDefault(e => e.AP_ID == id);
                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "AccessPoint with ID " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        resp.AP_IP_ADDR = access.AP_IP_ADDR;
                        resp.AP_COMID = access.AP_COMID;
                        resp.AP_SUBNETMASK = access.AP_SUBNETMASK;
                        resp.AP_GETWAY = access.AP_GETWAY;
                        resp.AP_NAME = access.AP_NAME;
                        resp.AP_ONLINESTATUS = access.AP_ONLINESTATUS;
                        resp.AP_FWVERSION = access.AP_FWVERSION;
                        resp.AP_LASTCOMMUNICATION = access.AP_LASTCOMMUNICATION;
                        resp.AP_CONNECTED_LOCK = access.AP_CONNECTED_LOCK;
                        resp.AP_PHY_ADDR = access.AP_PHY_ADDR;
                        resp.AP_GROUP_ID = access.AP_GROUP_ID;

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
        [Route("DeleteAccessPoint/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.AccessPoints.FirstOrDefault(e => e.AP_ID == id);

                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "AccessPoint with ID " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        integ.AccessPoints.Remove(resp);
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
