using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserManagement_GymBookings.Data;
using UserManagement_GymBookings.Extensions;
using UserManagement_GymBookings.Models;
using UserManagement_GymBookings.Models.ViewModel;
using UserManagement_GymBookings.Repositories;

namespace UserManagement_GymBookings.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork ouw;

        //private readonly ApplicationUserGymRepository applicationUserGymRepository;
        //private readonly GymClassRepository gymClassRepository;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork ouw)
        {
            _context = context;
            _userManager = userManager;
            this.mapper = mapper;
            this.ouw = ouw;
            //applicationUserGymRepository = new ApplicationUserGymRepository(context);
            //gymClassRepository = new GymClassRepository(context);
        }

        // GET: GymClasses
        //[Authorize(Roles = "Member")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(IndexViewModel viewModel = null)
        {
            var userId = _userManager.GetUserId(User);
            var model = new IndexViewModel();
            if (!User.Identity.IsAuthenticated)
            {
                
                model = mapper.Map<IndexViewModel>(await ouw.GymClassRepository.GetAllAsync());
                //{
                //    GymClasses = ouw.GymClassRepository.GetWithBookingsAsync().Result
                //                                        .Select(g => new GymClassViewModel
                //                                        {
                //                                            Id = g.Id,
                //                                            Name = g.Name,
                //                                            Duration = g.Duration,
                //                                            // Attending = g.AttendingMembers.Any(a => a.ApplicationUserID == userID)
                //                                        })
                //    //        //GymClasses = await _context.GymClass.Include(g => g.AttendingMembers)
                //    //        //                                    .Select(g => new GymClassViewModel
                //    //        //                                    {
                //    //        //                                        Id = g.Id,
                //    //        //                                        Name = g.Name,
                //    //        //                                        Duration = g.Duration,
                //    //        //                                       // Attending = g.AttendingMembers.Any(a => a.ApplicationUserID == userID)
                //    //        //                                    })
                //    //        //                                    .ToListAsync()
                //};
            }
            if(viewModel.ShowHistory)
            {
            model = mapper.Map<IndexViewModel>(await ouw.GymClassRepository.GetHistoryAsync(), opt => opt.Items.Add("Id", userId));

            }

            if(User.Identity.IsAuthenticated && !viewModel.ShowHistory)
            {
                model = mapper.Map<IndexViewModel>(await ouw.GymClassRepository.GetWithBookingsAsync(), opt => opt.Items.Add("Id", userId));

            }

                //return View(model);
                //}
                //var userId = _userManager.GetUserId(User);

            //return View(await _context.GymClass.ToListAsync());

            return View(model);
        }

     

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await ouw.GymClassRepository.GetAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // GET: GymClasses/Create
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            //if (Request.IsAjax())
            //    return PartialView("CreatePartial") ;

            return Request.IsAjax() ? PartialView("CreatePartial") : View();

            //return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                ouw.GymClassRepository.Add(gymClass);
                await ouw.CompleteAsync();

                if(Request.IsAjax())
                {
                    
                    return PartialView("GymClassPartial", gymClass); //
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        //TODO
       //[IsAjax]
        public ActionResult Fetch()
        {
            return PartialView("CreatePartial");
        }

        // GET: GymClasses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await ouw.GymClassRepository.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ouw.GymClassRepository.Update(gymClass);
                    await ouw.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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
            return View(gymClass);
        }

        public async Task<ActionResult> NewEdit(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }

            var gymClass = await ouw.GymClassRepository.FindAsync(id);

            if(await TryUpdateModelAsync(gymClass, "", g => g.Name, g => g.Duration))
            {
                try
                {
                    await ouw.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!GymClassExists(gymClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await ouw.GymClassRepository.GetAsync(id);
                //_context.GymClass
                //.FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await ouw.GymClassRepository.FindAsync(id);
            ouw.GymClassRepository.Remove(gymClass);
            await ouw.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return ouw.GymClassRepository.Any(id);
            //return _context.GymClass.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<ActionResult> BookingToggle(int? id)
        {
            if (id == null) return BadRequest();

            var userId = _userManager.GetUserId(User);
            
            ApplicationUserGymClass attending = await ouw.AppUserRepo.GetAttending(id, userId);
            //var gymClass = await _context.GymClass.Include(g => g.AttendingMembers).FirstOrDefaultAsync(a => a.Id == id);


            //var attending = gymClass.AttendingMembers.FirstOrDefault(a => a.ApplicationUserID == user);


            //var findMember = await _context.AttendingMember.FindAsync(user, id);

            if (attending  == null)
            {
                var booking = new ApplicationUserGymClass
                {
                    ApplicationUserID = userId,
                    GymClassID = (int)id,
                 
                    
                };

                ouw.AppUserRepo.Add(booking);
                //_context.AttendingMember.Add(booking);

            }
            else
            {
                ouw.AppUserRepo.Remove(attending);
               // _context.AttendingMember.Remove(findMember);
            }

            await ouw.CompleteAsync();
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<ActionResult> Bookings()
        {
            var userId = _userManager.GetUserId(User);

            //var model = await ouw.GymClassRepository.GetHistoryAsync();

            var model = mapper.Map<IndexViewModel>(await ouw.AppUserRepo.GetBookingsAsync(userId), opt => opt.Items.Add("Id", userId));


            //var model = _context.AttendingMember.IgnoreQueryFilters()
            //    .Where(u => u.ApplicationUserID == userId)
            //    .Select(a => a.GymClass);

            return View(nameof(Index), model);
        }
    }
}
