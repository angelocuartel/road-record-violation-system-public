using IDAnalyzer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Helpers;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.Utils.EmailTemplates;
using RoadRecordViolationSystem.Utils.Implementation;
using RoadRecordViolationSystem.Utils.Interface;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static RoadRecordViolationSystem.ViewModels.EmailViewModel;

namespace RoadRecordViolationSystem.Controllers
{
   [Route("api/[controller]")]
   //[Authorize(Roles ="Enforcer")]
    public class EnforcerScanningController : ControllerBase
    {
        private readonly ILicenseDetailsRepository<LicenseDetails> _licenseService;
        private readonly IViolationRepository<Violations> _violationService;
        private readonly ICrudRepository<VehicleInformation> _vehicleInfoService;
        private readonly IViolatorRepository<Violator> _violatorService;
        private readonly ICrudRepository<Accident> _accidentService;
        private readonly ITicketRepository<ViolatorTicketInformation> _ticketService;
        private readonly IEmail _email;
        private readonly IInvolveRepository<AccidentInvolve> _involveService;
        private readonly IFileManager _imageManager;
        private readonly ICrudRepository<Settings> _setting;
        private readonly IOfficerInvolveRepository<OfficerInvolve> _officerInvolve;
        public EnforcerScanningController(ILicenseDetailsRepository<LicenseDetails> licenseService, IViolationRepository<Violations> violationService,
            ICrudRepository<VehicleInformation> vehicleInfoService,IViolatorRepository<Violator> violatorService,
            ICrudRepository<Accident> accidentService, ITicketRepository<ViolatorTicketInformation> ticketService,
            IInvolveRepository<AccidentInvolve> involveService, IFileManager imageManager,ICrudRepository<Settings> setting,
            IEmail email, IOfficerInvolveRepository<OfficerInvolve> officerInvolve)
        {
            _licenseService = licenseService;
            _violationService = violationService;
            _vehicleInfoService = vehicleInfoService;
            _violatorService = violatorService;
            _accidentService = accidentService;
            _ticketService = ticketService;
            _involveService = involveService;
            _email = email;
            _imageManager = imageManager;
            _setting = setting;
            _officerInvolve = officerInvolve;
        }

        [HttpGet]
        [Route("violation-types")]
        public async Task<IActionResult> GetViolationTypes([FromServices] IViolationTypesRepository<ViolationTypes> _vioTypeService)
        {
            var vioTypes = await _vioTypeService.GetAllAsync();
            return Ok(vioTypes);
        }

        [HttpPost]
        [Route("save-signature")]
        public async Task<IActionResult> SaveSignature(string imageBase64,int? ticketInformationId)
        {
            Byte[] bytes = Convert.FromBase64String(imageBase64.Split(',')[1]);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            string path = _imageManager.GeneratePath(image);

            if (ticketInformationId.HasValue)
            {
                var ticketInformation = await _ticketService.GetByIdAsync(ticketInformationId.Value);
                _imageManager.Delete(ticketInformation.ViolatorSignatureImagePath);
                ticketInformation.ViolatorSignatureImagePath = path.ToImageContentPath();
                await _ticketService.UpdateAsync(ticketInformation);
            }
            else
            {
                HttpContext.Session.SetString("SignaturePath", path.ToImageContentPath());
            }

            _imageManager.UploadImage(image, path);


            return Ok(path.ToImageContentPath());
        }

        [HttpPost]
        [Route("save-violation")]
        public async Task<IActionResult> SaveViolation( MotoristViolations model)
        {
            try
            {

                var violatorName = $"{model.Violator.GivenName}-{model.Violator.MiddleName}-{model.Violator.LastName}";
                int violatorId = 0;
                int ticketId = 0;
                model.Violator.AddedBy = User.Identity.Name;

                    if (!_violatorService.NameExist(violatorName))
                    {
                      await  _violatorService.InsertAsync(model.Violator);
                    violatorId = _violatorService.GetLatestId();
                    }
                    else
                    {
                        violatorId = _violatorService.GetDbIdByName(violatorName);
                    }

                if (model.LicenseDetails != null)
                {
                    model.LicenseDetails.AddedBy = User.Identity.Name;

                    if (!_licenseService.LicenseExist(model.LicenseDetails.LicenseNo))
                    {
                        model.LicenseDetails.ViolatorId = violatorId;
                        await _licenseService.InsertAsync(model.LicenseDetails);
                    }
                }

                model.vehicleInformation.ViolatorId = violatorId;
                await _vehicleInfoService.InsertAsync(model.vehicleInformation);
                int vehicleId = _vehicleInfoService.GetLatestId();

                var ticketInfo = new ViolatorTicketInformation()
                {
                    PrintedBy = User.Identity.Name,
                    TotalAmountToBePayed = model.Violations.Sum(i => i.Cost),
                    ViolationCount = model.Violations.Count,
                    QrSecurityHash = QrHash.Generate(), 
                    TicketNo = TicketUtil.GenerateNo(),
                    ViolatorId = violatorId,
                    IsPaid = false,
                    vehicleId = vehicleId,
                    ViolatorSignatureImagePath = HttpContext.Session.GetString("SignaturePath")

            };
                await _ticketService.InsertAsync(ticketInfo);

                ticketId = _ticketService.GetLatestId();

                model.Violations.ForEach(i =>
                {
                    i.TicketId = ticketId;
                });
                await _violationService.InsertRangeAsync(model.Violations);
                model.TicketInformation = ticketInfo;

                var setting = await _setting.GetByIdAsync(1);

                if (!string.IsNullOrEmpty(model.Violator.Email) && setting.EnableEMail)
                {
                    var emailModel = new ViolationViewModel()
                    {
                        ViolatorName = violatorName,
                        Violations = model.Violations,
                        TicketNumber = model.TicketInformation.TicketNo
                    };

                    await _email.SendWithTemplateAsync(model.Violator.Email, "DPOS Violation",emailModel , new ViolationEmailTemplate());
                }

                if (!string.IsNullOrEmpty(model.Violator.PhoneNo) && setting.EnableSms)
                {
                    ItextMoUtil.SendMessage(model.Violator.PhoneNo, new ViolationMessageTemplate(new ItextMoViewModel.ViolationMessageViewmodel() { FullName = violatorName, TicketNumber = model.TicketInformation.TicketNo }));
                }
                    return Ok(model);

                
            }

            catch(Exception ex)
            {
                return BadRequest($"message: {ex.Message} \n exception: {ex.InnerException} \n stacktrace:{ex.StackTrace}");
            }

        }
       

        
        [HttpPost]
        [Route("add-accident")]
        public async Task<IActionResult> AddAccidentRecord( AccidentRecordViewModel accidentDetails)
        {
            try
            {
                accidentDetails.Accident.AddedBy = User.Identity.Name;
                accidentDetails.Accident.AccidentImage = HttpContext.Session.GetString("AccidentPath");
                HttpContext.Session.Remove("AccidentPath");

                var setting = await _setting.GetByIdAsync(1);
                await _accidentService.InsertAsync(accidentDetails.Accident);
                int accidentId = _accidentService.GetLatestId();
                accidentDetails.OfficerInvolves.ForEach(i => i.AccidentId = accidentId);
                await _officerInvolve.InsertRangeAsync(accidentDetails.OfficerInvolves);


                // send email and messages here
                foreach (var i in accidentDetails.Involves)
                {
                    i.AccidentId = accidentId;
                    await _involveService.InsertAsync(i);

                    if (!string.IsNullOrWhiteSpace(i.Email) && setting.EnableEMail)
                    {
                        var accidentEmailModel = new AccidentViewModel()
                        {
                            DriverName = $"{i.GivenName} {i.MiddleName} {i.LastName}",
                            Time = DateTime.UtcNow.ToLongTimeString(),
                            PlaceOfAccident = accidentDetails.Accident.Place
                        };

                        await _email.SendWithTemplateAsync(i.Email, "DPOS ACCIDENT", accidentEmailModel, new AccidentEmailTemplate());
                    }
                    if (!string.IsNullOrWhiteSpace(i.EmergencyContactNo) && setting.EnableSms)
                    {
                        var messageViewModel = new ItextMoViewModel.AccidentMessageViewModel()
                        {
                            Place = accidentDetails.Accident.Place,
                            FullName = $"{i.GivenName} {i.MiddleName} {i.LastName}",
                            Time = DateTime.UtcNow.ToLongTimeString(),
                        };

                        ItextMoUtil.SendMessage(i.EmergencyContactNo, new AccidentMessageTemplate(messageViewModel));
                    }

                }
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("save-accident-image")]
        public async Task<IActionResult> SaveAccidentImage(IFormFile image)
        {
            var path = _imageManager.GeneratePath(image, IFileManager.ExtensionType.Image);
            await _imageManager.UploadIFormFileAsync(image,path);

            HttpContext.Session.SetString("AccidentPath", path.ToImageContentPath());

            return Ok();
        } 

        [HttpPost]
        [Route("send-image")]
        public async Task<IActionResult> SendImage(string imageBase64)
        {
            var coreApi = new CoreAPI("YOUR API KEY HERE", "US");
            coreApi.EnableAuthentication(true, "2");
            coreApi.VerifyExpiry(true);

            try
            {       
               var result = await coreApi.Scan(imageBase64);
                return Ok(result);
            }
            catch(APIException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("get-info")]
        public async Task<IActionResult> GetInfoByQrHash(string hash)
        {
            try
            {
                var ticketInfo = _ticketService.GetDetailsByQrHash(hash);

                if (ticketInfo is null)
                    return BadRequest("No records found");

                var violations = await _violationService.GetAllByTicketNo(ticketInfo.TicketNo);
                var violator = await _violatorService.GetByIdAsync(ticketInfo.ViolatorId);
                var License = _licenseService.GetLicenseByViolatorId(violator.Id);
                var vehicleInfo = await _vehicleInfoService.GetByIdAsync(ticketInfo.vehicleId.Value);

                MotoristViolations model = new MotoristViolations
                {
                    Violator = violator,
                    Violations = violations,
                    TicketInformation = ticketInfo,
                    LicenseDetails = License,
                    vehicleInformation = vehicleInfo

                };

                return Ok(model);
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("save-violation-list")]
        public async Task<IActionResult> SaveViolationViaQr(List<Violations> violations)
        {
            try
            {
                await _violationService.InsertRangeAsync(violations);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


   


        
    }
}
