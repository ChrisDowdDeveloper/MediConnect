using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Helpers;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MediConnectBackend.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDBContext _context;
        public AppointmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> DeleteAppointmentById(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if(appointment == null)
            {
                return false;
            }
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsync(AppointmentQueryObject query)
        {
            var appointmentsQuery = _context.Appointments.AsQueryable();

            if (!string.IsNullOrEmpty(query.DoctorId))
            {
                appointmentsQuery = appointmentsQuery.Where(a => a.DoctorId == query.DoctorId);
            }
            if (query.IsDescending)
            {
                appointmentsQuery = appointmentsQuery.OrderByDescending(a => a.AppointmentDateTime).ThenByDescending(a => a.AppointmentDateTime);
            }
            else
            {
                appointmentsQuery = appointmentsQuery.OrderBy(a => a.AppointmentDateTime).ThenBy(a => a.AppointmentDateTime);
            }
            var skip = (query.PageNumber - 1) * query.PageSize;
            var appointments = await appointmentsQuery.Skip(skip).Take(query.PageSize).ToListAsync();

            return appointments;
        }


        public async Task<List<Appointment>> GetAllAppointmentsByPatientIdAsync(AppointmentQueryObject query)
        {
            var appointmentsQuery = _context.Appointments.AsQueryable();

            if (!string.IsNullOrEmpty(query.PatientId))
            {
                appointmentsQuery = appointmentsQuery.Where(a => a.PatientId == query.PatientId);
            }
            if (query.IsDescending)
            {
                appointmentsQuery = appointmentsQuery.OrderByDescending(a => a.AppointmentDateTime).ThenByDescending(a => a.AppointmentDateTime);
            }
            else
            {
                appointmentsQuery = appointmentsQuery.OrderBy(a => a.AppointmentDateTime).ThenBy(a => a.AppointmentDateTime);
            }
            var skip = (query.PageNumber - 1) * query.PageSize;
            var appointments = await appointmentsQuery.Skip(skip).Take(query.PageSize).ToListAsync();
            
            return appointments;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId) ?? throw new KeyNotFoundException("Appointment not found.");
            return appointment;
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

    }
}