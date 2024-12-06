using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Helpers;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MediConnectBackend.Repository
{
    public class PastAppointmentRepository : IPastAppointmentRepository
    {
        private readonly ApplicationDBContext _context;
        public PastAppointmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<PastAppointment>> GetAllPastAppointmentsByDoctorIdAsync(PastAppointmentQueryObject query)
        {
            var pastAppointmentQuery = _context.PastAppointments.AsQueryable();

            if(!string.IsNullOrEmpty(query.DoctorId))
            {
                pastAppointmentQuery = pastAppointmentQuery.Where(pa => pa.DoctorId == query.DoctorId);
            }
            if(query.IsDescending)
            {
                pastAppointmentQuery = pastAppointmentQuery.OrderByDescending(pa => pa.AppointmentDateTime).ThenByDescending(pa => pa.AppointmentDateTime);
            }
            else
            {
                pastAppointmentQuery = pastAppointmentQuery.OrderBy(pa => pa.AppointmentDateTime).ThenBy(pa => pa.AppointmentDateTime);
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            var pastAppointments = await pastAppointmentQuery.Skip(skip).Take(query.PageSize).Include(pa => pa.Doctor).ToListAsync();

            return pastAppointments;
        }

        public async Task<List<PastAppointment>> GetAllPastAppointmentsByPatientIdAsync(PastAppointmentQueryObject query)
        {
            var pastAppointmentQuery = _context.PastAppointments.AsQueryable();

            if(!string.IsNullOrEmpty(query.PatientId))
            {
                pastAppointmentQuery = pastAppointmentQuery.Where(pa => pa.PatientId == query.PatientId);
            }
            if(query.IsDescending)
            {
                pastAppointmentQuery = pastAppointmentQuery.OrderByDescending(pa => pa.AppointmentDateTime).ThenByDescending(pa => pa.AppointmentDateTime);
            }
            else
            {
                pastAppointmentQuery = pastAppointmentQuery.OrderBy(pa => pa.AppointmentDateTime).ThenBy(pa => pa.AppointmentDateTime);
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            var pastAppointments = await pastAppointmentQuery.Skip(skip).Take(query.PageSize).Include(pa => pa.Patient).ToListAsync();

            return pastAppointments;
        }

        public async Task<PastAppointment> CreatePastAppointmentAsync(PastAppointment pastAppointment)
        {
            await _context.PastAppointments.AddAsync(pastAppointment);
            await _context.SaveChangesAsync();
            return pastAppointment;
        }
    }
}