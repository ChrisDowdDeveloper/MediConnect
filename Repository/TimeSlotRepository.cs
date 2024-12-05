using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Dtos.TimeSlot;
using MediConnectBackend.Helpers;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediConnectBackend.Repository
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly ApplicationDBContext _context;

        public TimeSlotRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsByDoctorAsync(string doctorId)
        {
            return await _context.TimeSlots
                .Where(ts => ts.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeSlot>> GetAllAvailableTimeSlotsByDoctorAsync(string doctorId)
        {
            return await _context.TimeSlots
                .Where(ts => ts.DoctorId == doctorId && ts.IsBooked == false)
                .ToListAsync();
        }

        public async Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId)
        {
            var timeSlot = await _context.TimeSlots.FirstOrDefaultAsync(ts => ts.Id == timeSlotId) ?? throw new KeyNotFoundException("Time slot not found.");
            return timeSlot;
        }

        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsByAvailabilityId(int availabilityId)
        {
            return await _context.TimeSlots
                .Where(ts => ts.AvailabilityId == availabilityId)
                .ToListAsync();
        }

        public async Task<TimeSlot> CreateTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();
            return timeSlot;
        }

        public async Task<TimeSlot?> UpdateTimeSlotAsync(int id, UpdateTimeSlotDto dto)
        {
            var timeSlot = await _context.TimeSlots.FirstOrDefaultAsync(ts => ts.Id == id);
            if(timeSlot == null)
            {
                return null;
            }
            timeSlot.StartTime = dto.StartTime;
            timeSlot.EndTime = dto.EndTime;
            timeSlot.IsBooked = dto.IsBooked;
            timeSlot.AppointmentId = dto.AppointmentId;

            await _context.SaveChangesAsync();
            return timeSlot;
        }

        public async Task<bool> DeleteTimeSlotAsync(int id)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(id);
            if (timeSlot == null) return false;

            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
