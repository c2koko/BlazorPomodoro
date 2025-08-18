using Microsoft.EntityFrameworkCore;
using BlazorWeb.Data;
using BlazorWeb.Models;
using System.Security.Principal;

namespace BlazorWeb.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // User-related methods
        public async Task<User> GetUser()
        {
            var userName = WindowsIdentity.GetCurrent().User?.Value;
            var today = DateTime.Now.Date;
            var user = await _context.Users
                .Include(u => u.Notes)
                .FirstOrDefaultAsync(u => u.Name == userName);

            if (user == null)
            {
                user = new User
                {
                    Name = userName,
                    Streak = 1,
                    LastOpen = today
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else if (user.LastOpen.Date < today)
            {
                if (user.LastOpen.Date > today.AddHours(-36))
                {
                    user.Streak++;
                }
                else
                {
                    user.Streak = 1;
                }
                user.LastOpen = today;
                await _context.SaveChangesAsync();
            }

            return user;
        }

        // Note-related methods
        public async Task<List<Note>> GetNotes()
        {
            var userName = WindowsIdentity.GetCurrent().User?.Value;
            var user = await _context.Users
                .Include(u => u.Notes)
                .FirstOrDefaultAsync(u => u.Name == userName);
            return user?.Notes ?? new List<Note>();
        }
        public async Task AddNote(string text)
        {
            var userName = WindowsIdentity.GetCurrent().User?.Value;
            var user = await _context.Users
                .Include(u => u.Notes)
                .FirstOrDefaultAsync(u => u.Name == userName);
            if (user != null)
            {
                var note = new Note
                {
                    Content = text
                };
                user.Notes.Add(note);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteNote(int noteId)
        {
            var userName = WindowsIdentity.GetCurrent().User?.Value;
            var user = await _context.Users
                .Include(u => u.Notes)
                .FirstOrDefaultAsync(u => u.Name == userName);
            if (user != null)
            {
                var note = user.Notes.FirstOrDefault(n => n.Id == noteId);
                if (note != null)
                {
                    user.Notes.Remove(note);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
