
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mc2.CrudTest.Presentation.Client.Controllers
{
    [ApiController]
    [Authorize]
    public class ProgramController : Controller
    {
        private readonly CustomerService _service;
        private readonly IDateTimeProviderService _dateTimeProvider;

        public ProgramController(CustomerService service, IDateTimeProviderService dateTimeProvider)
        {
            _service = service;
            _dateTimeProvider = dateTimeProvider;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AllPrograms")]
        https://localhost:7114/Program/AllPrograms
        public async Task<ActionResult<LinqDataResult<ProgramView>>> AllPrograms(LinqDataRequest request)
        {
            var res = await _service.ItemsViewAsync(request);
            return Ok(res);
        }
   

        [HttpPost("GetMyPrograms")]
        https://localhost:7114/Program/GetMyPrograms
        public async Task<ActionResult<LinqDataResult<ProgramView>>> GetMyPrograms(LinqDataRequest request)
        {
            if (User.IsInRole("Customer"))
            {
                var res = await _service.GetCompanyProgramsByNationalCode(request, UserCompanyNationalID);
                return Ok(res);
            }
            else if (User.IsInRole("Admin") || User.IsInRole("Committee_User") || User.IsInRole("Committee_Admin"))
            {
                var res = await _service.ItemsViewAsync(request);
                return Ok(res);
            }
            return Ok(null);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("GetDetailOfMyProgram")]
        https://localhost:7114/Program/GetDetailOfMyProgram?id=1
        public async Task<ActionResult<ProgramDto>> GetDetailOfMyProgram(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var x = await _service.GetByID(id);
                    return Ok(new ProgramDto(x));
                }
                else
                {
                    var result = await _service.GetByID(id, UserCompanyNationalID);
                    return Ok(new ProgramDto(result));
                }

            }
            catch (ServiceException ex)
            {
                if (ex is ServiceObjectNotFoundException)
                {
                    return StatusCode(500, ex.ToServiceExceptionString());
                }
                if (ex is ObjectCanNotByAccessByYou)
                {
                    return StatusCode(500, ex.ToServiceExceptionString());
                }
                return StatusCode(500, ex.ToServiceExceptionString());
            }

        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPost("Post")]
        public async Task<ActionResult> Post(ProgramDto program)
        {
            Domain.Model.Program c = program.GetProgram();
            if (program.CompanyNationalID == null)
            {
                return BadRequest("Field can not be null: NationalCode");
            }
            if (!User.IsInRole("Admin"))
            {
                if (program.CompanyNationalID != UserCompanyNationalID)
                {
                    return BadRequest("Cutomer users can just change the data related to their program");
                }
            }
            try
            {
                c.CreatedBy = UserName;
                c.CreationTime = _dateTimeProvider.GetNow();
                c.LastModificationTime = c.CreationTime;
                c.LastModifiedBy = UserName;
                var g = await _service.AddByNationalCodeAsync(c, program.CompanyNationalID);
                return Ok(g);
            }
            catch (ServiceException ex)
            {
                if (ex is ServiceModelValidationException)
                {
                    return BadRequest(ex.Message + ", " + (ex as ServiceModelValidationException).JSONFormattedErrors);
                }
                return StatusCode(500, ex.ToServiceExceptionString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPut("Put")]
        public async Task<ActionResult> Put(ProgramDto program)
        {
            var dbLoadedObject = await _service.RetrieveByIdAsync(program.ID);
            if (dbLoadedObject == null)
            {
                return StatusCode(500, "Company object not found: companyID=" + program.ID);
            }
            Domain.Model.Program c = program.GetProgram();
            if (program.CompanyNationalID == null)
            {
                throw new Exception("Field can not be null: NationalCode");
            }
            if (!User.IsInRole("Admin"))
            {
                if (dbLoadedObject.Company.NationalCode != UserCompanyNationalID)
                {
                    throw new Exception("Cutomer users can just add programs to their program");
                }
            }
            try
            {
                c.LastModificationTime = _dateTimeProvider.GetNow();
                c.LastModifiedBy = UserName;
                await _service.ModifyAsync(c);
                return Ok();
            }
            catch (ServiceException ex)
            {
                if (ex is ServiceModelValidationException)
                {
                    return BadRequest(ex.Message + ", " + (ex as ServiceModelValidationException).JSONFormattedErrors);
                }
                return StatusCode(500, ex.ToServiceExceptionString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoveByIdAsync(id);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.ToServiceExceptionString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        Api For Dropdown
        [HttpGet]
        [Route("GetAllRequestCountTypes")]
        https://localhost:7114/Program/GetAllOwnershipCompanyTypes
        public async Task<ActionResult<IEnumerable<RequestCountTypeDto>>> GetAllRequestCountTypes()
        {
            var rtn = await _service.GetAllRequestCountTypesAsync();
            return Ok(rtn.Select(uu => new RequestCountTypeDto(uu)).ToList());
        }

        [HttpGet]
        [Route("GetAllProgramTypes")]
        https://localhost:7114/Program/GetAllProgramTypes
        public async Task<ActionResult<IEnumerable<ProgramTypeDto>>> GetAllProgramTypes()
        {
            var rtn = await _service.GetAllProgramTypeAsync();
            return Ok(rtn.Select(uu => new ProgramTypeDto(uu)).ToList());
        }

        [HttpGet]
        [Route("GetAllProgramTypeKinds")]
        https://localhost:7114/Program/GetAllProgramTypeKinds
        public async Task<ActionResult<IEnumerable<ProgramTypeKindDto>>> GetAllProgramTypeKinds()
        {
            var rtn = await _service.GetAllProgramTypeKindAsync();
            return Ok(rtn.Select(uu => new ProgramTypeKindDto(uu)).ToList());
        }


        [HttpGet]
        [Route("GetProgramTypeKindFromProgramType")]
        https://localhost:7114/Program/GetProgramTypeKindFromProgramType
        public async Task<ActionResult<GetProgramTypeKindFromProgramTypeDto>> GetProgramTypeKindFromProgramType(int programTypeID)
        {
            var rtn = await _service.GetProgramTypeKindFromProgramTypeAsync(programTypeID);
            return Ok(new GetProgramTypeKindFromProgramTypeDto()
            {
                ProgramTypeKindID = rtn.ProgramTypeKindID
            });
        }

        [HttpGet]
        [Route("GetAllProgramTypesAsync")]
        public async Task<ActionResult> GetAllProgramTypesAsync()
        {
            return Ok((await _service.GetAllProgramTypes()));
        }
    }
}
