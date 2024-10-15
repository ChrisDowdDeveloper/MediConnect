using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Helpers;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface IPastAppointmentRepository
    {
        Task<List<PastAppointment>> GetAllPastAppointmentsByDoctorIdAsync(PastAppointmentQueryObject query);
        Task<List<PastAppointment>> GetAllPastAppointmentsByPatientIdAsync(PastAppointmentQueryObject query);
        Task<PastAppointment> CreatePastAppointmentAsync(PastAppointment pastAppointment);
        
    }
}