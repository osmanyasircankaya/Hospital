using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppoitmentController : ControllerBase
    {
        private readonly IAppoitment unitOfWork;

        public AppoitmentController(IAppoitment unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Appoitments.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Appoitments.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetAllByPatientId/{patientId}")]
        public async Task<IActionResult> GetAllByPatientIdAsync(string patientId)
        {
            var data = await unitOfWork.Appoitments.GetAppoitmentsByPatientIdAsync(patientId);
            if (data == null) return Ok();
            return Ok(data);
        }
        
        [HttpPut]
        public async Task<IActionResult> Add(Appoitment Appoitment)
        {
            var data = await unitOfWork.Appoitments.AddAsync(Appoitment);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Appoitments.DeleteAsync(id);
            return Ok(data);
        }
        //[HttpPut]
        //public async Task<IActionResult> Update(Appoitment Appoitment)
        //{
        //    var data = await unitOfWork.Appoitments.UpdateAsync(Appoitment);
        //    return Ok(data);
        //}

    }
}