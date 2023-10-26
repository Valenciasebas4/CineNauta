using Cine_Nauta.DAL;
using Cine_Nauta.DAL.Entities;
using Cine_Nauta.Helpers;
using Cine_Nauta.Models;
using Cine_Nauta.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .Include(m => m.Functions)
                .ToListAsync()
                );
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Functions)
                .Include(m => m.Rooms)
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


       


       

        
        
        public async Task<IActionResult> DetailsMovie(int? Id)
        {
            if (Id == null) return NotFound();

            // Cargar la película y los datos relacionados (Género y Clasificación)
            Movie movie = await _context.Movies
                .Include(m => m.Gender)
                .Include(m => m.Classification)
                .Include(m => m.Functions)
                .FirstOrDefaultAsync(p => p.Id == Id);
            

            if (movie == null) return NotFound();

            return View(movie);
        }
        
        public async Task<IActionResult> Delete(int? Id)
        {
            
            if (Id == null) return NotFound();

            Movie movie = await _context.Movies
                .FirstOrDefaultAsync(p => p.Id == Id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Movie movieModel)
        {
            Movie movie = await _context.Movies
                .FirstOrDefaultAsync(p => p.Id == movieModel.Id);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            

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

        /*
        public async Task<IActionResult> EditFunction(int? Id)
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
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFunction(int? Id, EditFunctionViewModel editFunctionViewModel)
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


        public async Task<IActionResult> DetailsFunction(int? Id)
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


        public async Task<IActionResult> DeleteFunction(int? Id)
        {

            if (Id == null) return NotFound();

            Function function = await _context.Functions
                .Include(m => m.Movie)
                .Include(m => m.Room)
                .FirstOrDefaultAsync(p => p.Id == Id);
            if (function == null) return NotFound();

            return View(function);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Function functionModel)
        {
            Function function = await _context.Functions
               .FirstOrDefaultAsync(p => p.Id == functionModel.Id);

            _context.Functions.Remove(function);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }
        #endregion





    }
}
