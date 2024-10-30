using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
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

        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync(string doctorId, DateTime date)
        {
            return await _context.TimeSlots
                .Where(ts => ts.DoctorId == doctorId &&
                             ts.StartDateTime.Date == date.Date &&
                             !ts.IsBooked)
                .ToListAsync();
        }

        public async Task<TimeSlot> GetTimeSlotByIdAsync(int id)
        {
            return await _context.TimeSlots
                .Include(ts => ts.Doctor) // Include related Doctor entity if needed
                .FirstOrDefaultAsync(ts => ts.Id == id);
        }

        public async Task AddAsync(TimeSlot timeSlot)
        {
            await _context.TimeSlots.AddAsync(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(id);
            if (timeSlot != null)
            {
                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();
            }
        }
    }
}
