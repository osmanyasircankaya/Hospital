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
        [HttpGet("GetMinimumDay")]
        public async Task<IActionResult> GetMinimumByDate()
        {
            var data = await unitOfWork.Appointments.GetMinimumAppointmentDay();
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetMaximumDate")]
        public async Task<IActionResult> GetMaximumByDate()
        {
            var data = await unitOfWork.Appointments.GetMaximumAppointmentDay();
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
        [HttpGet("GetOfPatientsDetailByDataRange/{patientId}/{day}")]

        //Hastanın belirli tarih aralığındaki randevularına ait bilgileri içeren metod. Son 30 günkü randevuları almak için day parametresine 30 verirseniz. Bugün ve 30 gün öncesi arasındaki verileri getirir.
        public async Task<IActionResult> GetOfPatientsDetailByDataRange(string patientId, int day)
        {
            var data = await unitOfWork.Appointments.GetAppointmentsDetailByDateRange(patientId, _dateTimeParse(day));
            if (data == null) return Ok();
            return Ok(data);
        }

        //Doktorların belirli günler aralığındaki randevu sayısını getirir. Son 30 günkü randevu sayısını almak için day parametresine 30 verirseniz. Bugün ve 30 gün öncesi arasındaki verileri getirir.
        [HttpGet("GetSizeByDoctorId/{doctorId}/{day}")]
        public async Task<IActionResult> GetSizeByDoctorId(int doctorId, int day)
        {
            var data = await unitOfWork.Appointments.GetAppointmentsSizeByDoctorId(doctorId, _dateTimeParse(day));
            return Ok(data);
        }

        [HttpGet("GetAllSortedByDate")]
        public async Task<IActionResult> GetAllSortedByDate()
        {
            var data = await unitOfWork.Appointments.GetAppointmentsCountOrderByDate();
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

        [HttpGet("GetCountByHours/{startHour}/{endHour}")]
        public async Task<IActionResult> GetCountByHours(int startHour, int endHour)
        {
            var data = await unitOfWork.Appointments.GetAppointmentCountByHours(startHour, endHour);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("GetWeekDayByAppointmentCount")]
        public async Task<IActionResult> GetWeekDayByCount()
        {
            var data = await unitOfWork.Appointments.GetWeekDayByAppointmentCount();
            if (data == null) return Ok();
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