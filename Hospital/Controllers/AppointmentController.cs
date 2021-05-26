using System;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment unitOfWork;

        public AppointmentController(IAppointment unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Appointments.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Appointments.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetAllByPatientId/{patientId}")]
        public async Task<IActionResult> GetAllByPatientIdAsync(string patientId)
        {
            var data = await unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(patientId);
            if (data == null) return Ok();
            return Ok(data);
        }
        //Doktorların belirli günler aralığındaki randevuları
        [HttpGet("GetSizeByDoctorId/{doctorId}/{day}")]
        public async Task<IActionResult> GetSizeByDoctorId(int doctorId,int day)
        {
            var data = await unitOfWork.Appointments.GetAppointmentsSizeByDoctorId(doctorId, _dateTimeParse(day));
            return Ok(data);
        }

        //Polikliniğe göre belirli gün aralığındaki randevular
        //Sorunlu.
        [HttpGet("GetSizeByPolyclinicId/{polId}/{day}")]
        
        public async Task<IActionResult> GetSizeByPolId(int polId,int day)
        {
            var data = await unitOfWork.Appointments.GetAppoimentsSizeByPolId(polId, _dateTimeParse(day));
            return Ok(data);
        }
        
        
        [HttpPut]
        public async Task<IActionResult> Upsert(Appointment Appointment)
        {
            var data = await unitOfWork.Appointments.UpsertAsync(Appointment);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Appointments.DeleteAsync(id);
            return Ok(data);
        }
        [HttpDelete("DeleteAppointmentByPatientId/{patientId}")]
        public async Task<IActionResult> Delete(string patientId)
        {
            var data = await unitOfWork.Appointments.DeleteAppointmentByIdAsync(patientId);
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