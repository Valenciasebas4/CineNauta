﻿using Cine_Nauta.DAL;
using Cine_Nauta.DAL.Entities;
using Cine_Nauta.Helpers;
using Cine_Nauta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cine_Nauta.Controllers
{
    public class FunctionsController : Controller
    {

        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;

        public FunctionsController(DataBaseContext context, IDropDownListHelper dropDownListHelper)
        {
            _context = context;
            _dropDownListHelper = dropDownListHelper;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Functions
                 .Include(c => c.Movie)
                 .Include(c => c.Room)
                .ToListAsync());
        }



        public async Task<IActionResult> Create()
        {

            AddFunctionViewModel addFunctionViewModel = new()
            {
                Movies = await _dropDownListHelper.GetDDLMoviesAsync(),
                Rooms = await _dropDownListHelper.GetDDLRoomsAsync(),
            };

            return View(addFunctionViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddFunctionViewModel addFunctionViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    Function function = new()
                    {
                        Price= addFunctionViewModel.Price,
                        CreatedDate = DateTime.Now,
                        Movie = await _context.Movies.FindAsync(addFunctionViewModel.MovieId),
                        Room = await _context.Rooms.FindAsync(addFunctionViewModel.RoomId),


                    };





                    _context.Add(function);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una función con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }


            addFunctionViewModel.Movies = await _dropDownListHelper.GetDDLMoviesAsync();
            addFunctionViewModel.Rooms = await _dropDownListHelper.GetDDLRoomsAsync();
            return View(addFunctionViewModel);
        }


    }
}
