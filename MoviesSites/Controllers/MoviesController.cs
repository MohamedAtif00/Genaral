using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesSites.Models;
using MoviesSites.Models.JunctionClasses;
using MoviesSites.Models.ViewModel;

namespace MoviesSites.Controllers
{

    public class MoviesController : Controller
    {
        private readonly MoviesDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public MoviesController(MoviesDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.movies == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            var types = Enum.GetValues(typeof(MovieType)).Cast<MovieType>();
            ViewBag.Types = new SelectList(types);

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*MovieViewModel movieModel,List<ActorViewModel> actorModel*/ActorMovieViewModel model)
        {
            // Create a new Movie object
            var movie = new Movie { Id = model.movie.Id, Title = model.movie.Title, ReleaseDate = model.movie.ReleaseDate, Type = (MovieType)model.movie.Type };

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Save the movie's image to the server
                var movieFilePath = Path.Combine(_environment.WebRootPath, "images", model.movie.Image.FileName);
                using (var stream = new FileStream(movieFilePath, FileMode.Create))
                {
                    await model.movie.Image.CopyToAsync(stream);
                }

                // Add the file path to the movie's ImagePath property
                movie.ImagePath = movieFilePath;

                //List<ActorViewModel> amvm = /*(List<ActorViewModel>)*/actorModel;

                // Loop through the actors in the movieModel
                //foreach (var actor in amvm)
                List<ActorViewModel> avm = new List<ActorViewModel> { model.actors1, model.actors2, model.actors3 };
                foreach (var actor in avm) {
                    if (_context.actors.Any(a => a.Name == actor.Name))
                    {
                        var actorful = _context.actors.FirstOrDefault(a => a.Name == actor.Name);
                        var juncClassful = new ActorMovie { Actor = actorful, Movie = movie, ActorId = actorful.Id, MovieId = movie.Id };
                        _context.Add(juncClassful);
                        continue;
                    }

                    var newActor = new Actor { Id = actor.Id, Name = actor.Name };
                    // Save the actor's image to the server
                    var filePath = Path.Combine(_environment.WebRootPath, "images", "actors", actor.Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await actor.Image.CopyToAsync(stream);
                    }

                    // Add the file path to the actor's ImagePath property
                    newActor.ImagePath = filePath;
                    //var actormovie = new ActorMovie { ActorId = actor.actors.Id, MovieId = movie.Id };
                    // Add the actor to the movie object
                    _context.Add(newActor);
                    var juncClass = new ActorMovie { Actor = newActor, Movie = movie, ActorId = newActor.Id, MovieId = movie.Id };
                    _context.Add(juncClass);
                    //movie.ActorsMovies.Add(juncClass);

                }


                // Add the movie to the context
                _context.Add(movie);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Redirect to the index action
                return RedirectToAction(nameof(Index));
            }

            // Return the view with the movie model
            return View(movie);
        }




        //public async Task<IActionResult> Create( MovieViewModel movieModel ,IFormFile file)
        //{
        //    var movie = new Movie { Id = movieModel.Id , Title = movieModel.Title,ReleaseDate = movieModel.ReleaseDate, Type = (MovieType)movieModel.Type };

        //    if (ModelState.IsValid)
        //    {
        //        // Movie section
        //        if (file == null || file.Length == 0)
        //        {
        //            return BadRequest("File not found");
        //        }

        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //        var filePath = Path.Combine(/*Directory.GetCurrentDirectory()*/_environment.WebRootPath, "images", fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //        movie.ImagePath = filePath;

        //        _context.Add(movie);

        //        // actor section 

        //        if (movieModel.Actors != null)
        //        {
        //            foreach (var actor in movieModel.Actors)
        //            {
        //                if (actor.Image != null)
        //                { 
        //                    var filePath = Path.Combine(_environment.WebRootPath, "actors", actor.Image.FileName);
        //                    using (var stream = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        await actor.Image.CopyToAsync(stream);
        //                    }
        //                    var actorModel = new Actor { Name = actor.Name, ImagePath = "/actors/" + actor.Image.FileName };
        //                    movie.ActorMovies.Add(new ActorMovie { Actor = actorModel, Movie = movie });
        //                }
        //            }



        //            await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(movie);
        //}

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.movies == null)
            {
                return NotFound();
            }

            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Type,ImagePath")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.movies == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.movies == null)
            {
                return Problem("Entity set 'MoviesDbContext.movies'  is null.");
            }
            var movie = await _context.movies.FindAsync(id);
            if (movie != null)
            {
                _context.movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[Produces("json")]
        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            try
            {
                string searchString2 = HttpContext.Request.Query["searchString"].ToString();
                var result = _context.actors.Where(a => a.Name.Contains(searchString2))
                                            .Select(p => p).ToList();
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest();
            }
        }
        //public async Task<IActionResult> Search(string searchString)
        //{
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        var actors = await _context.actors.Where(a => a.Name.Contains(searchString))
        //                                            .Select(a => a.Name /*new { id = a.Id, label = a.Name }*/)
        //                                            .ToListAsync();
        //        return Json(actors);
        //    }
        //    return Json(new List<object>());
        //}

        //public async Task<IActionResult> Search(string searchString)
        //{


        //    var actors = _context.actors.FirstOrDefault(a => a.Name.Contains(searchString));


        //    return Json(actors.Id);
        //}

        [HttpGet]
        public IActionResult GetImage(int id)
        {
            var actor = _context.actors.Find(id);
            if (actor != null && actor.ImagePath != null)
            {
                var imagePath = Path.Combine(_environment.WebRootPath, "images", actor.ImagePath);
                //var stream = new FileStream(imagePath, FileMode.Open);
                //return new FileStreamResult(stream, "image/jpeg");
                return Json(imagePath);
            }
            else
            {
                return NotFound();
            }
        }


        private bool MovieExists(int id)
        {
          return _context.movies.Any(e => e.Id == id);
        }
    }
}
