using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolyclinicController : ControllerBase
    {
        private readonly IPolyclinic unitOfWork;

        public PolyclinicController(IPolyclinic unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Polyclinics.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Polyclinics.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Upsert(Polyclinic Polyclinic)
        {
            var data = await unitOfWork.Polyclinics.UpsertAsync(Polyclinic);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Polyclinics.DeleteAsync(id);
            return Ok(data);
        }
        [HttpGet("GetPolyclinicByAppointmentCount")]
        public async Task<IActionResult> GetByAppointmentCount()
        {
            var data = await unitOfWork.Polyclinics.GetPolyclinicByAppointmentCount();
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}