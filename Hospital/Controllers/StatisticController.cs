using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StatisticController : ControllerBase
    {
        private readonly IStatistic unitOfWork;
        public StatisticController(IStatistic unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("GetAppointmentsCount")]
        public async Task<IActionResult> GetAppointmentsCount()
        {
            var data = await unitOfWork.Statistics.GetAppointmentsCount();
            return Ok(data);
        }
        [HttpGet("GetAppointmentCountByHours/{startHour}/{endHour}")]
        public async Task<IActionResult> GetAppointmentCountByHours(int startHour, int endHour)
        {
            var data = await unitOfWork.Statistics.GetAppointmentCountByHours(startHour, endHour);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetAppointmentsCountOrderByDate")]
        public async Task<IActionResult> GetAppointmentsCountOrderByDate()
        {
            var data = await unitOfWork.Statistics.GetAppointmentsCountOrderByDate();
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetAppointmentsDetailByDateRange/{patientId}/{day}")]
        public async Task<IActionResult> GetAppointmentsDetailByDateRange(string patientId,int day)
        {
            var data = await unitOfWork.Statistics.GetAppointmentsDetailByDateRange(patientId, _dateTimeParse(day));
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetAppointmentsSizeByDoctorId/{doctorId}/{day}")]
        public async Task<IActionResult> GetAppointmentsSizeByDoctorId(int doctorId, int day)
        {
            var data = await unitOfWork.Statistics.GetAppointmentsSizeByDoctorId(doctorId, _dateTimeParse(day));
            return Ok(data);
        }
        [HttpGet("GetDoctorsOrderByAppointmentCount")]
        public async Task<IActionResult> GetDoctorsOrderByAppointmentCount()
        {
            var data = await unitOfWork.Statistics.GetDoctorsOrderByAppointmentCount();
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetMaximumAppointmentDay")]
        public async Task<IActionResult> GetMaximumAppointmentDay()
        {
            var data = await unitOfWork.Statistics.GetMaximumAppointmentDay();
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetMinimumAppointmentDay")]
        public async Task<IActionResult> GetMinimumAppointmentDay()
        {
            var data = await unitOfWork.Statistics.GetMinimumAppointmentDay();
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetPolyclinicNameByAppointmentCount")]
        public async Task<IActionResult> GetPolyclinicNameByAppointmentCount()
        {
            var data = await unitOfWork.Statistics.GetPolyclinicNameByAppointmentCount();
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetWeekDayByAppointmentCount")]
        public async Task<IActionResult> GetWeekDayByAppointmentCount()
        {
            var data = await unitOfWork.Statistics.GetWeekDayByAppointmentCount();
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
