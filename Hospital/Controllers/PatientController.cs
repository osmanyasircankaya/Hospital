using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient unitOfWork;

        public PatientController(IPatient unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Patients.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await unitOfWork.Patients.GetPatientByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Upsert(Patient Patient)
        {
            var data = await unitOfWork.Patients.UpsertAsync(Patient);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Patients.DeleteAsync(id);
            return Ok(data);
        }
    }
}