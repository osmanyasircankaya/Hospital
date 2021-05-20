using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor unitOfWork;

        public DoctorController(IDoctor unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
         {
            var data = await unitOfWork.Doctors.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Doctors.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetAllByPolId/{polId}")]
        public async Task<IActionResult> GetAllByPolIdAsync(int polId)
        {
            var data = await unitOfWork.Doctors.GetDoctorsByPolIdAsync(polId);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Doctor Doctor)
        {
            var data = await unitOfWork.Doctors.AddAsync(Doctor);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Doctors.DeleteAsync(id);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Doctor Doctor)
        {
            var data = await unitOfWork.Doctors.UpdateAsync(Doctor);
            return Ok(data);
        }
    }
}