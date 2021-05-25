using System;
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
            var data = await unitOfWork.Appoitments.GetAppointmentsByPatientIdAsync(patientId);
            if (data == null) return Ok();
            return Ok(data);
        }
        //Doktorların belirli günler aralığındaki randevuları
        [HttpGet("GetSizeByDoctorId/{doctorId}/{day}")]
        public async Task<IActionResult> GetSizeByDoctorId(int doctorId,int day)
        {
            var data = await unitOfWork.Appoitments.GetAppointmentsSizeByDoctorId(doctorId, _dateTimeParse(day));
            return Ok(data);
        }

        //Polikliniğe göre belirli gün aralığındaki randevular
        //Sorunlu.
        [HttpGet("GetSizeByPolyclinicId/{polId}/{day}")]
        
        public async Task<IActionResult> GetSizeByPolId(int polId,int day)
        {
            var data = await unitOfWork.Appoitments.GetAppoimentsSizeByPolId(polId, _dateTimeParse(day));
            return Ok(data);
        }
        
        
        [HttpPut]
        public async Task<IActionResult> Upsert(Appointment Appoitment)
        {
            var data = await unitOfWork.Appoitments.UpsertAsync(Appoitment);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Appoitments.DeleteAsync(id);
            return Ok(data);
        }
        [HttpDelete("DeleteAppoitmentByPatientId/{patientId}")]
        public async Task<IActionResult> Delete(string patientId)
        {
            var data = await unitOfWork.Appoitments.DeleteAppointmentByIdAsync(patientId);
            return Ok(data);
        }

        private string _dateTimeParse(int day)
        {
            var dateTime = DateTime.Now.Subtract(TimeSpan.FromDays(day));
            var dateString = dateTime.ToString("yyyy-M-dd hh:mm:ss");
            return dateString;
        }
    }
}