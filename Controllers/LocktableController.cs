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
using System.Threading.Tasks;
using Core.Enities;

namespace AllPrime.EndPoint.Controllers
{
    [RoutePrefix("api/locktable")]
    public class LocktableController : ApiController
    {
        [Route("GetDoors")]
        [HttpGet]
        public IHttpActionResult GetDoor()
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    return Ok(integ.Locktable.ToList());
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }

        }

        [Route("GetDoors/{id:int:min(1)}")]
        // GET api/values/5
        public IHttpActionResult GetDoor(int id)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Locktable.FirstOrDefault(e => e.DoorId == id);
                    if (resp != null)
                    {
                        return Content(HttpStatusCode.OK, resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Door with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("GetDoorsByDoorGroup/{id:int:min(1)}")]
        [HttpGet]
        // GET api/values/5
        public IHttpActionResult GetDoorsByDoorGroup(int id)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Doordoorgroups.Select(lk => new
                    { 
                        DoorGroupID = lk.DoorGroupId,
                        doorID = lk.Door.DoorId,
                        Doorname = lk.Door.ROOMNAME,
                        DoorLock = lk.Door.LOCKID

                    }).Where(e => e.DoorGroupID == id).ToList();
                    if (resp != null)
                    {
                        return Content(HttpStatusCode.OK, resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Door with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("GetDoorsByHardware/{id:int:min(1)}")]
        [HttpGet]
        // GET api/values/5
        public IHttpActionResult GetDoorsByHardware(int id)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Hardwares.Select(lk => new
                    {
                        HardwareID = lk.Hardwareid,
                        HardwareName = lk.Name,
                        HardwareDescription = lk.Descrip,
                        DoorID = lk.Locktable.Select(e => new {
                            
                            DoorName = e.ROOMNAME,
                            LockNumber = e._LockNumber,
                            RoomType = e.ROOMTYPEID
                        })

                    }).Where(e => e.HardwareID == id).ToList();
                    if (resp != null)
                    {
                        return Content(HttpStatusCode.OK, resp);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Door with ID " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }


        [Route("GenerateDoors/{id}")]
        [HttpGet]
        // GET api/values/5
        public IHttpActionResult GenerateDoors(int id, [FromBody] DoorParam doorParam)
        {
            try
            {
                var _From = doorParam._From;
                var _Upto = doorParam._Upto;
                var step = doorParam.Step;
                var header = doorParam._Header;
                var _footer = doorParam._Footer;
                using (IntegraContext integ = new IntegraContext())
                {

                    for (int i = _From; i <= _Upto; i++)
                    {
                        //lockt.Add(new Locktable(id,));
                        var Loc = new Locktable
                        {
                            _LockNumber = id,
                            LOCKID = 1,
                            ROOMNAME = i.ToString(),
                            ROOMTYPEID = 1,
                            TIMETABLE = null,
                            AUTOCHANGETABLE = null,
                            AP_ID = 1,
                            CalendarID = null,
                            DsdCalendarID = null,
                            HARDWAREID = 2,
                            ISACTIVE = 1,
                            _CreationDate = DateTime.Now,
                            _UpdatedDate = DateTime.Now,
                            _ApConfig = 0,
                            _ApChannel = 0,
                            _UserDefApAddr = 0,
                            _TimeUnit = 0,
                            _ContactTime = 0,
                            _OnlineStatus = 0,
                            _BatteryStatus = 0,
                            _DoorAlarm = 0,
                            _DoorState = 0,
                            _FwVersion = null,
                            _SignalStrenght = -129,
                            SEQUENCENUMBER = 492074665,
                            Office = 1,
                            Author_Req = "1111111111111111111111111111111111111111111111111111111",
                            Status = 0,
                            Commonpin = "FFFFFFFF",
                            Block = 14692355,
                            Cancel = -1,
                            Prog = -1,
                            Position = 0,
                            Author_Def = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                            Author_Op = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                            Copycounter = 0,
                            Oneshotcounter = 0,
                            Timezone = 31,
                            Officeenabled = 0,
                            Officeopt = 0,
                            Adaenabled = 0,
                            Adaopt = 0,
                            Overrideblocked = 0,
                            Overrideprivacy = 0,
                            Author_Ch = null,
                            Office_Ch = 0,
                            Ada_Ch = 0,
                            Ocid = -1,
                            I2c = -1,
                            Zonein = -1,
                            Zoneout = -1,
                            Areain = 0,
                            Areaout = 0,
                            Serialnumber = null,
                            Reqlevel = 1,
                            Createdtime = null,
                            Initialdate_Ch = null,
                            Expirationdate_Ch = null,
                            W_Onlinestatus = "Unknown",
                            W_Aprotocolconfig = null,
                            W_Ap_Phy_Addr = 0,
                            W_Alive_Data = null,
                            W_Event_Mode_Data = null,
                            W_Polling_Mode_Data = null,
                            W_Signal_Strenght = -129,
                            W_Pending_Tasks = 0,
                            W_Last_Changes = null,
                            W_Com_Type = "Hybrid mode",
                            W_Displayonsecuritymonitor = 1,
                            Alarmnotificationcounter = 0,
                            W_Phyapp_Assigned = 0,
                            Batterylevel = 0,
                            Batterychangedate = DateTime.Now,
                            _AllowedEvt1 = 0,
                            _AllowedEvt2 = 0,
                            _WtimeUnit = 0,
                            _WtimeInterval = 0,
                            _WcontactTime = 0,
                            _IsAchieved = 0,
                            W_Ap_Phy_Addr2 = 0,
                            _Voltage = 0


                        };
                        integ.Locktable.Add(Loc);
                        integ.SaveChanges();
                    }

                }
                    return Content(HttpStatusCode.OK, "Saved successfully");
               
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }


        // POST api/values
        [Route("CreateDoor")]
        [HttpPost]
        public IHttpActionResult CreateDoor(Locktable locktable)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    integ.Locktable.Add(locktable);
                    integ.SaveChanges();

                    var mess = Request.CreateResponse(HttpStatusCode.Created, locktable);
                    mess.Headers.Location = new Uri(Request.RequestUri + locktable.DoorId.ToString());
                    return Ok(mess);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/values/5
        [Route("UpdateDoor/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Locktable locktable)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Locktable.FirstOrDefault(e => e.DoorId == id);
                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Door with ID " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        resp._LockNumber = locktable._LockNumber ;
                        resp.LOCKID = locktable.LOCKID;
                        resp.ROOMNAME = locktable.ROOMNAME;
                        resp.ROOMTYPEID = locktable.ROOMTYPEID;
                        resp.TIMETABLE = locktable.TIMETABLE;
                        resp.AUTOCHANGETABLE = locktable.AUTOCHANGETABLE;
                        resp.AP_ID = locktable.AP_ID;
                        resp.CalendarID = locktable.CalendarID;
                        resp.DsdCalendarID = locktable.DsdCalendarID;
                        resp.HARDWAREID = locktable.HARDWAREID;
                        resp.ISACTIVE = locktable.ISACTIVE;
                        resp._CreationDate = locktable._CreationDate;
                        resp._UpdatedDate = locktable._UpdatedDate;
                        resp._ApConfig = locktable._ApConfig;
                        resp._ApChannel = locktable._ApChannel;
                        resp._UserDefApAddr = locktable._UserDefApAddr;
                        resp._TimeUnit = locktable._TimeUnit;
                        resp._ContactTime = locktable._ContactTime;
                        resp._OnlineStatus = locktable._OnlineStatus;
                        resp._BatteryStatus = locktable._BatteryStatus;
                        resp._DoorAlarm = locktable._DoorAlarm;
                        resp._DoorState = locktable._DoorState;
                        resp._FwVersion = locktable._FwVersion;
                        resp._SignalStrenght = locktable._SignalStrenght;
                        resp.SEQUENCENUMBER = locktable.SEQUENCENUMBER;

                        resp.Office = locktable.Office;
                        resp.Author_Req = locktable.Author_Req;
                        resp.Status = locktable.Status;
                        resp.Commonpin = locktable.Commonpin;
                        resp.Block = locktable.Block;
                        resp.Cancel = locktable.Cancel;
                        resp.Prog = locktable.Prog;
                        resp.Position = locktable.Position;
                        resp.Author_Def = locktable.Author_Def;
                        resp.Author_Op = locktable.Author_Op;
                        resp.Copycounter = locktable.Copycounter;
                        resp.Oneshotcounter = locktable.Oneshotcounter;
                        resp.Timezone = locktable.Timezone;
                        resp.Officeenabled = locktable.Officeenabled;
                        resp.Officeopt = locktable.Officeopt;
                        resp.Adaenabled = locktable.Adaenabled;
                        resp.Adaopt = locktable.Adaopt;
                        resp.Overrideblocked = locktable.Overrideblocked;
                        resp.Overrideprivacy = locktable.Overrideprivacy;
                        resp.Author_Ch = locktable.Author_Ch;
                        resp.Office_Ch = locktable.Office_Ch;
                        resp.Ada_Ch = locktable.Ada_Ch;
                        resp.Ocid = locktable.Ocid;
                        resp.I2c = locktable.I2c;
                        resp.Zonein = locktable.Zonein;
                        resp.Zoneout = locktable.Zoneout;
                        resp.Areain = locktable.Areain;
                        resp.Areaout = locktable.Areaout;
                        resp.Serialnumber = locktable.Serialnumber;
                        resp.Reqlevel = locktable.Reqlevel;
                        resp.Createdtime = locktable.Createdtime;
                        resp.Initialdate_Ch = locktable.Initialdate_Ch;
                        resp.Expirationdate_Ch = locktable.Expirationdate_Ch;
                        resp.W_Onlinestatus = locktable.W_Onlinestatus;
                        resp.W_Aprotocolconfig = locktable.W_Aprotocolconfig;
                        resp.W_Ap_Phy_Addr = locktable.W_Ap_Phy_Addr;
                        resp.W_Alive_Data = locktable.W_Alive_Data;
                        resp.W_Event_Mode_Data = locktable.W_Event_Mode_Data;
                        resp.W_Polling_Mode_Data = locktable.W_Polling_Mode_Data;
                        resp.W_Signal_Strenght = locktable.W_Signal_Strenght;
                        resp.W_Pending_Tasks = locktable.W_Pending_Tasks;
                        resp.W_Last_Changes = locktable.W_Last_Changes;
                        resp.W_Com_Type = locktable.W_Com_Type;
                        resp.W_Displayonsecuritymonitor = locktable.W_Displayonsecuritymonitor;
                        resp.Alarmnotificationcounter = locktable.Alarmnotificationcounter;
                        resp.W_Phyapp_Assigned = locktable.W_Phyapp_Assigned;
                        resp.Batterylevel = locktable.Batterylevel;
                        resp.Batterychangedate = locktable.Batterychangedate;
                        resp._AllowedEvt1 = locktable._AllowedEvt1;
                        resp._AllowedEvt2 = locktable._AllowedEvt2;
                        resp._WtimeUnit = locktable._WtimeUnit;
                        resp._WtimeInterval = locktable._WtimeInterval;
                        resp._WcontactTime = locktable._WcontactTime;
                        resp._IsAchieved = locktable._IsAchieved;
                        resp.W_Ap_Phy_Addr2 = locktable.W_Ap_Phy_Addr2;
                        resp._Voltage = locktable._Voltage;



                        integ.SaveChanges();

                        return Content(HttpStatusCode.OK, resp);
                    }

                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE api/values/5
        [Route("DeleteDoor/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (IntegraContext integ = new IntegraContext())
                {
                    var resp = integ.Locktable.FirstOrDefault(e => e.DoorId == id);

                    if (resp == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Door with ID " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        integ.Locktable.Remove(resp);
                        integ.SaveChanges();

                        return Content(HttpStatusCode.OK, resp);
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
