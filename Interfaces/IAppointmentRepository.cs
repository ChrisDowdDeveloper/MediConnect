using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Helpers;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface IAppointmentRepository
    {

        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsync(AppointmentQueryObject query);
        Task<List<Appointment>> GetAllAppointmentsByPatientIdAsync(AppointmentQueryObject query);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentById(int id);
    }
}