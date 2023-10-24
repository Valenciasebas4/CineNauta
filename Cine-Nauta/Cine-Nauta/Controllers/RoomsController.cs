using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cine_Nauta.DAL;
using Cine_Nauta.DAL.Entities;
using Cine_Nauta.Models;

namespace Cine_Nauta.Controllers
{
    public class RoomsController : Controller
    {
        private readonly DataBaseContext _context;

        public RoomsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rooms
                  .Include(s => s.Seats)
                  .ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(s => s.Seats)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {

                // Verificar si ya existe un el Número de la sala
                bool Exists = await _context.Rooms
                    .AnyAsync(v => v.NumberRoom == room.NumberRoom );

                if (Exists) // Se valida si el resultado de la variable es true o false
                {
                    // Si la variable es true se envia un mensaje 
                    ModelState.AddModelError(string.Empty, "El número de sala ya existe");
                    TempData["SalaIngresada"] = "No Se ingreso correctamente";
                }
                else
                { // Si la variable es false entonces se crea la clasificación
                    try
                    {
                        TempData["SalaIngresada"] = "Se ingreso correctamente";
                       
                        room.CreatedDate = DateTime.Now;
                        _context.Add(room);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception exception)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);

        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    room.ModifiedDate = DateTime.Now;
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'DataBaseContext.Rooms'  is null.");
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
          return _context.Rooms.Any(e => e.Id == id);
        }

        #region Seat(Asieto)

        [HttpGet]
        public async Task<IActionResult> AddSeat(int? roomId)
        {

            if (roomId == null) return NotFound();

            Room room = await _context.Rooms.FirstOrDefaultAsync(c => c.Id == roomId);

            if (room == null) return NotFound();

            SeatViewModel seatViewModel = new()
            {
                RoomId = room.Id,
            };

            return View(seatViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSeat(SeatViewModel seatViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Seat seat = new()
                    {

                        Room = await _context.Rooms.FirstOrDefaultAsync(c => c.Id == seatViewModel.RoomId),
                        NumberSeat = seatViewModel.NumberSeat,
                        Busy = false,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = null,
                    };

                    _context.Add(seat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { Id = seatViewModel.RoomId });
                    //return RedirectToAction(nameof(Details), new { Id = seatViewModel.RoomId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "Ya existe una silla con el mismo nombre en esta sala.");
                    else
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(seatViewModel);
        }

        #endregion


    }
}
