using Cine_Nauta.DAL;
using Cine_Nauta.DAL.Entities;
using Cine_Nauta.Helpers;
using Cine_Nauta.Models;
using Cine_Nauta.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Drawing;

namespace Cine_Nauta.Controllers
{
    public class MoviesController : Controller
    {

        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;

        public MoviesController(DataBaseContext context, IDropDownListHelper dropDownListHelper)
        {
            _context = context;
            _dropDownListHelper = dropDownListHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies
                .Include(m => m.Gender)
                .Include(m => m.Classification)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Gender)
                .Include(m => m.Classification)
                .Include(m => m.Functions)
                .ThenInclude(f => f.Room)  // Cargar la relación Room
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);

        }


        public async Task<IActionResult> DetailsMovie(int? Id)
        {
            if (Id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Gender)
                .Include(m => m.Classification)
                .Include(m => m.Functions)
                .ThenInclude(f => f.Room)  // Cargar la relación Room
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);

        }


        public async Task<IActionResult> Movies()
        {
            return View(await _context.Movies
                .Include(m => m.Functions)
                //.Include(m => m.Hours)// Incluye los horarios relacionados
                .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            
            AddMovieViewModel addMovieViewModel = new()
            {
                Genders = await _dropDownListHelper.GetDDLGendersAsync(),
                Classifications = await _dropDownListHelper.GetDDLClassificationsAsync(),   
            };

            return View(addMovieViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddMovieViewModel addMovieViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    Movie movie = new()
                    {
                        Title = addMovieViewModel.Title,
                        Description = addMovieViewModel.Description,
                        Director = addMovieViewModel.Director,
                        Duration = addMovieViewModel.Duration,
                        CreatedDate = DateTime.Now,
                        Gender = await _context.Genders.FindAsync(addMovieViewModel.GenderId),
                        Classification = await _context.Classifications.FindAsync(addMovieViewModel.ClassificationId),


                    };


                    _context.Add(movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una pelicula con el mismo nombre.");
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


            addMovieViewModel.Genders = await _dropDownListHelper.GetDDLGendersAsync();
            addMovieViewModel.Classifications = await _dropDownListHelper.GetDDLClassificationsAsync();
            return View(addMovieViewModel);
        }


        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            
            if (Id == null) return NotFound();

            Movie movie = await _context.Movies.FindAsync(Id);
            if (movie == null) return NotFound();

            EditMovieViewModel editMovietViewModel = new()
            {
                Id = movie.Id,
                Title = movie.Title,
                LaunchYear = movie.LaunchYear,
                Description = movie.Description,
                Director = movie.Director,  
                Duration = movie.Duration,
                GenderId= movie.GenderId,
                ClassificationId= movie.ClassificationId,
                Genders = await _dropDownListHelper.GetDDLGendersAsync(),
                Classifications = await _dropDownListHelper.GetDDLClassificationsAsync(),
                
            };

            return View(editMovietViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? Id, EditMovieViewModel editMovietViewModel)
        {
            if (Id != editMovietViewModel.Id) return NotFound();

            try
            {
                Movie movie = await _context.Movies.FindAsync(editMovietViewModel.Id);

                //Aquí sobreescribo para luego guardar los cambios en BD
                movie.Title = editMovietViewModel.Title;
                movie.Description = editMovietViewModel.Description;
                movie.LaunchYear = editMovietViewModel.LaunchYear;
                movie.Director = editMovietViewModel.Director;
                movie.Duration = editMovietViewModel.Duration;
                movie.Gender = await _context.Genders.FindAsync(editMovietViewModel.GenderId);
                movie.Classification = await _context.Classifications.FindAsync(editMovietViewModel.ClassificationId);
                movie.ModifiedDate = DateTime.Now;

                _context.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    ModelState.AddModelError(string.Empty, "Ya existe una pelicula con el mismo nombre.");
                else
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            await FillDropDownListLocation(editMovietViewModel);
            return View(editMovietViewModel);
        }


        // DropDownListLocation es la lista desplegable de los generos y clasificaciones
        private async Task FillDropDownListLocation(EditMovieViewModel addMovieViewModel)
        {
            addMovieViewModel.Genders = await _dropDownListHelper.GetDDLGendersAsync();
            addMovieViewModel.Classifications = await _dropDownListHelper.GetDDLClassificationsAsync();
        }

        private bool Exists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }


       
        
        public async Task<IActionResult> Delete(int? Id)
        {
            
            if (Id == null) return NotFound();

            Movie movie = await _context.Movies
                .Include(m => m.Gender)
                .Include(m => m.Classification)
                .Include(m => m.Functions)              
                .FirstOrDefaultAsync(p => p.Id == Id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            if (_context.Movies == null)
                return Problem("Entity set 'DataBaseContext.Movies' is null.");

            var movie = await _context.Movies.FindAsync(Id); //Select * From Movies Where Id = '1'
            if (movie != null) _context.Movies.Remove(movie);

            await _context.SaveChangesAsync(); //Delete From Movies where Id = '1'
            return RedirectToAction(nameof(Index));
        }


        #region Function

        public async Task<IActionResult> IndexFunction()
        {

            return View(await _context.Functions
                 .Include(c => c.Movie)
                 .Include(c => c.Room)
                 .ToListAsync());
        }


        public async Task<IActionResult> AddFunction(int movieId)
        {

            if (movieId == null) return NotFound();

            Movie movie = await _context.Movies
                .FirstOrDefaultAsync(p => p.Id == movieId);

            if (movie == null) return NotFound();



            AddFunctionViewModel addFunctionViewModel = new()
            {
                MovieId = movie.Id,
                Rooms = await _dropDownListHelper.GetDDLRoomsAsync(),
            };

            return View(addFunctionViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFunction(AddFunctionViewModel addFunctionViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Function function = new()
                    {
                        Price = addFunctionViewModel.Price,
                        FunctionDate = addFunctionViewModel.FunctionDate,
                        CreatedDate = DateTime.Now,
                        Movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == addFunctionViewModel.MovieId),
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

            addFunctionViewModel.Rooms = await _dropDownListHelper.GetDDLRoomsAsync();
            return View(addFunctionViewModel);
        }

        
        public async Task<IActionResult> EditFunction(int? functionId)
        {

            if (functionId == null) return NotFound();

            Function function = await _context.Functions
                .Include(f => f.Movie)
                .FirstOrDefaultAsync( f => f.Id == functionId);

            if (function == null) return NotFound();

            EditFunctionViewModel editFunctionViewModel = new()
            {
                
                MovieId = function.Movie.Id,
                Id = function.Id,
                Price = function.Price,
                FunctionDate = function.FunctionDate,
                CreatedDate = DateTime.Now,              
                RoomId = function.RoomId,
                Rooms = await _dropDownListHelper.GetDDLRoomsAsync(),


            };

            return View(editFunctionViewModel);
        }

  


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFunction(int movieId, EditFunctionViewModel editFunctionViewModel)
        {
            if (movieId != editFunctionViewModel.MovieId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    Function function = new()
                    {
                        Id = editFunctionViewModel.Id,
                        Price = editFunctionViewModel.Price,
                        FunctionDate = editFunctionViewModel.FunctionDate,
                        ModifiedDate = editFunctionViewModel.ModifiedDate,
                        Room = await _context.Rooms.FindAsync(editFunctionViewModel.RoomId),
                        
                    };
                 

                  
                    _context.Update(function);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = editFunctionViewModel.MovieId });
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
        

        public async Task<IActionResult> DetailsFunction(int? functionId)
        {
            if (functionId == null) return NotFound();

            // Cargar la película y los datos relacionados (Género y Clasificación)     
            Function function = await _context.Functions
                .Include(m => m.Movie)
                .Include(m => m.Room)
                .FirstOrDefaultAsync(p => p.Id == functionId);
            //Movie movie = await _context.Movies.FirstOrDefaultAsync(p => p.Id == Id);

            if (function == null) return NotFound();

            return View(function);
        }


        public async Task<IActionResult> DeleteFunction(int? functionId)
        {

            if (functionId == null) return NotFound();

            Function function = await _context.Functions
                .Include(m => m.Movie)
                .Include(m => m.Room)
                .FirstOrDefaultAsync(p => p.Id == functionId);
            if (function == null) return NotFound();

            return View(function);
        }

        [HttpPost, ActionName("DeleteFunction")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFunctionConfirmed(int? functionId)
        {
            if (_context.Functions == null) return Problem("Entity set 'DataBaseContext.Functions' is null.");

            Function function = await _context.Functions
                .Include(m => m.Movie)
               .FirstOrDefaultAsync(p => p.Id == functionId);

            if (function != null) _context.Functions.Remove(function);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = function.Movie.Id });
        }
        #endregion





    }
}
