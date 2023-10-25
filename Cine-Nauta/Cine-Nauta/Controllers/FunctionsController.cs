using Cine_Nauta.DAL;
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
                        FunctionDate = addFunctionViewModel.FunctionDate,
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
                        ModelState.AddModelError(string.Empty, "Ya existe una función de la pelicula en la misma fecha y sala");
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







        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {

            if (Id == null) return NotFound();

            Function function = await _context.Functions.FindAsync(Id);
            if (function == null) return NotFound();

            EditFunctionViewModel editFunctionViewModel = new()
            {
                Id = function.Id,
                Price = function.Price,
                FunctionDate = function.FunctionDate,
                CreatedDate = DateTime.Now,
                MovieId = function.MovieId,
                RoomId = function.RoomId,
                Movies = await _dropDownListHelper.GetDDLMoviesAsync(),               
                Rooms = await _dropDownListHelper.GetDDLRoomsAsync(),
                

            };

            return View(editFunctionViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? Id, EditFunctionViewModel editFunctionViewModel)
        {
            if (Id != editFunctionViewModel.Id) return NotFound();

            try
            {
                Function function = await _context.Functions.FindAsync(editFunctionViewModel.Id);

                //Aquí sobreescribo para luego guardar los cambios en BD
                function.FunctionDate = editFunctionViewModel.FunctionDate;
                function.Price = editFunctionViewModel.Price;              
                function.Movie = await _context.Movies.FindAsync(editFunctionViewModel.MovieId);
                function.Room = await _context.Rooms.FindAsync(editFunctionViewModel.RoomId);
                function.ModifiedDate = DateTime.Now;

                _context.Update(function);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    ModelState.AddModelError(string.Empty, "Ya existe una función de la pelicula en la misma fecha y sala");
                else
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            await FillDropDownListLocation(editFunctionViewModel);
            return View(editFunctionViewModel);
        }

        // DropDownListLocation es la lista desplegable de las peliculas y salas
        private async Task FillDropDownListLocation(EditFunctionViewModel editFunctionViewModel)
        {
            editFunctionViewModel.Movies = await _dropDownListHelper.GetDDLMoviesAsync();
            editFunctionViewModel.Rooms = await _dropDownListHelper.GetDDLRoomsAsync();
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null) return NotFound();

            // Cargar la película y los datos relacionados (Género y Clasificación)
            Function function = await _context.Functions
                .Include(m => m.Movie)
                .Include(m => m.Room)
                .FirstOrDefaultAsync(p => p.Id == Id);
            //Movie movie = await _context.Movies.FirstOrDefaultAsync(p => p.Id == Id);

            if (function == null) return NotFound();

            return View(function);
        }

    }
}
