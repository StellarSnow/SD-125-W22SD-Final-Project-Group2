﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Models.ViewModel;
using SD_340_W22SD_Final_Project_Group6.BLL;
using SD_340_W22SD_Final_Project_Group6.DAL;
using Microsoft.AspNetCore.Identity;

namespace SD_340_W22SD_Final_Project_Group6.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private TicketBusinessLogic _ticketBLL { get; set; }
        private ProjectBusinessLogicLayer _projectBLL { get; set; }
        private UserBusinessLogic _userBLL { get; set; }
        private CommentBusinessLogic _commentBLL { get; set; }
        private TicketWatcherBusinessLogic _ticketWatcherBLL { get; set; }
        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _ticketBLL = new TicketBusinessLogic(new TicketRepository(context));
            _projectBLL = new ProjectBusinessLogicLayer(new ProjectRepository(context));
            _userBLL = new UserBusinessLogic(new UserRepository(context, userManager));
            _commentBLL = new CommentBusinessLogic(new CommentRepository(context)); 
            _ticketWatcherBLL = new TicketWatcherBusinessLogic(new TicketWatcherRepository(context));   
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(_ticketBLL.GetAllTickets());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _ticketBLL.GetAsync((int)id);

            List<SelectListItem> currUsers = new List<SelectListItem>();
            ticket.Project.AssignedTo.ToList().ForEach(t =>
            {
                currUsers.Add(new SelectListItem(t.ApplicationUser.UserName, t.ApplicationUser.Id.ToString()));
            });
            ViewBag.Users = currUsers;

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "ProjectManager")]
        public IActionResult Create(int projId)
        {
            Project currProject = _projectBLL.Get(projId);
            List<SelectListItem> currUsers = new List<SelectListItem>();
            currProject.AssignedTo.ToList().ForEach(t =>
            {
                currUsers.Add(new SelectListItem(t.ApplicationUser.UserName, t.ApplicationUser.Id.ToString()));
            });

            ViewBag.Projects = currProject;
            ViewBag.Users = currUsers;

            return View();

        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ProjectManager")]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,RequiredHours,TicketPriority")] Ticket ticket, int projId, string userId)
        {
            if (ModelState.IsValid)
            { 
                ticket.Project = _projectBLL.GetProject(projId);
                Project currProj = _projectBLL.GetProject(projId);
                ApplicationUser owner = await _userBLL.GetUserAsync(userId);
                ticket.Owner = owner;
                _ticketBLL.Add(ticket);
                currProj.Tickets.Add(ticket);
                _ticketBLL.SaveTicket();
                return RedirectToAction("Index","Projects", new { area = ""});
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "ProjectManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = _ticketBLL.GetTicket((int)id);
            
            if (ticket == null)
            {
                return NotFound();
            }

            List<ApplicationUser> results = await _userBLL.GetUsersWhoAreNotTheTicketOwnerAsync(ticket.Owner);

            List<SelectListItem> currUsers = new List<SelectListItem>();
            results.ForEach(r =>
            {
                currUsers.Add(new SelectListItem(r.UserName, r.Id.ToString()));
            });
            ViewBag.Users = currUsers;

            return View(ticket);
        }

        [Authorize(Roles = "ProjectManager")]
        public async Task<IActionResult> RemoveAssignedUser(string id, int ticketId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ticket currTicket = _ticketBLL.GetTicket(ticketId);
            ApplicationUser currUser = await _userBLL.GetUserAsync(id);
            //To be fixed ASAP
            currTicket.Owner = currUser;
            _ticketBLL.SaveTicket();
            
            return RedirectToAction("Edit", new { id = ticketId });
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ProjectManager")]
        public async Task<IActionResult> Edit(int id,string userId, [Bind("Id,Title,Body,RequiredHours")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser currUser = await _userBLL.GetUserAsync(userId);
                    ticket.Owner = currUser;
                    _ticketBLL.UpdateTicket(ticket);
                    _ticketBLL.SaveTicket();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), new {id = ticket.Id});
            }
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CommentTask(int TaskId, string? TaskText)
        {
            if (TaskId != null || TaskText != null)
            {
                try
                {
                    Comment newComment = new Comment();
                    string userName = User.Identity.Name;
                    ApplicationUser user = _userBLL.GetUserByUserName(userName);
                    Ticket ticket = _ticketBLL.GetTicket(TaskId);

                    newComment.CreatedBy = user;
                    newComment.Description = TaskText;
                    newComment.Ticket = ticket;
                    user.Comments.Add(newComment);
                    _commentBLL.AddComment(newComment);
                    ticket.Comments.Add(newComment);

                    int Id = TaskId;
                    _commentBLL.SaveComment();
                    return RedirectToAction("Details", new {Id});

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateHrs(int id, int hrs)
        {
            if (id != null || hrs != null)
            {
                try
                {
                    Ticket ticket = _ticketBLL.GetTicket(id);
                    ticket.RequiredHours = hrs;
                    _ticketBLL.SaveTicket();
                    return RedirectToAction("Details", new { id });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddToWatchers(int id)
        {
            if (id != null)
            {
                try
                {
                    TicketWatcher newTickWatch = new TicketWatcher();
                    string userName = User.Identity.Name;
                    ApplicationUser user = _userBLL.GetUserByUserName(userName);
                    Ticket ticket = _ticketBLL.GetTicket(id);

                    newTickWatch.Ticket = ticket;
                    newTickWatch.Watcher = user;
                    user.TicketWatching.Add(newTickWatch);
                    ticket.TicketWatchers.Add(newTickWatch);
                    _ticketWatcherBLL.AddTicketWatcher(newTickWatch);

                    _ticketWatcherBLL.SaveTicketWatcher();
                    return RedirectToAction("Details", new { id });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnWatch(int id)
        {
            if (id != null)
            {
                try
                {
                    
                    string userName = User.Identity.Name;
                    ApplicationUser user = _userBLL.GetUserByUserName(userName);
                    Ticket ticket = _ticketBLL.GetTicket(id);
                    
                    TicketWatcher currTickWatch = _ticketWatcherBLL.GetTicketWatcherByTicketAndUserName(ticket, user);
                    _ticketWatcherBLL.DeleteTicket(currTickWatch);
                    ticket.TicketWatchers.Remove(currTickWatch);
                    user.TicketWatching.Remove(currTickWatch);

                    _ticketWatcherBLL.SaveTicketWatcher();
                    return RedirectToAction("Details", new { id });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            if (id != null)
            {
                try
                {
                    Ticket ticket = _ticketBLL.GetTicket(id);
                    ticket.Completed = true;

                    _ticketBLL.SaveTicket();
                    return RedirectToAction("Details", new { id });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnMarkAsCompleted(int id)
        {
            if (id != null)
            {
                try
                {
                    Ticket ticket = _ticketBLL.GetTicket(id);
                    ticket.Completed = false;

                    _ticketBLL.SaveTicket();
                    return RedirectToAction("Details", new { id });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Index");
        }


        // GET: Tickets/Delete/5
        [Authorize(Roles = "ProjectManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketBLL.GetTicket((int)id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ProjectManager")]
        public async Task<IActionResult> DeleteConfirmed(int id, int projId)
        {
            var ticket = _ticketBLL.GetTicket(id);
            Project currProj = _projectBLL.GetProject(projId);
            if (ticket != null)
            {
                currProj.Tickets.Remove(ticket);
                _ticketBLL.DeleteTicket(ticket);
            }

            _ticketBLL.SaveTicket();
            return RedirectToAction("Index", "Projects");
        }

        private bool TicketExists(int id)
        {
          return _ticketBLL.DoesTicketExist(id);
        }
    }
}
