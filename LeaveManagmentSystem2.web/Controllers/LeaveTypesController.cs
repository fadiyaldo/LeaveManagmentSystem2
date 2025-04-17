using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagmentSystem2.web.Data;
using LeaveManagmentSystem2.web.Models.LeaveTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using LeaveManagmentSystem2.web.Services;

namespace LeaveManagmentSystem2.web.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILeaveTypesService _leaveTypesService;
        private const string NameExistsValidationMessage = "This leave type already exists in the database";

        public LeaveTypesController(ApplicationDbContext context, IMapper mapper, ILeaveTypesService leaveTypesService)
        {
            _context = context;
            _mapper = mapper;
            _leaveTypesService = leaveTypesService;

        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            //var data = await _context.LeaveTypes.ToListAsync();

            // convert dataModel to viewmodel
            //var viewModel = data.Select(d => new IndexVM
            //{
            //    Id = d.Id,
            //    Name = d.Name,
            //    NumberOfDays = d.NumberOfDays
            //});


            // convert dataModel to viewmodel with automapper
            //var viewModel = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);

            var viewModel= await _leaveTypesService.GetAll();

            // return the viewmodel to the view
            return View(viewModel);
            
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var leaveType = await _context.LeaveTypes
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (leaveType == null)
            //{
            //    return NotFound();
            //}
            //var LeaveType= _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);

            var leaveType = await _leaveTypesService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {

            if(await _leaveTypesService.CheckIfLeaveTypeNameExists(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), "Name already exists");
            }
            if (ModelState.IsValid)
            {
                await _leaveTypesService.Create(leaveTypeCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreate);
        }

        

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var leaveType = await _context.LeaveTypes.FindAsync(id);
            //if (leaveType == null)
            //{
            //    return NotFound();
            //}
            //var dataViewModel = _mapper.Map<LeaveTypeEditVM>(leaveType);
            var dataViewModel =await  _leaveTypesService.Get<LeaveTypeEditVM>(id.Value);
            return View(dataViewModel);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await _leaveTypesService.CheckIfLeaveTypeNameExistsForEdit(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypesService.Edit(leaveTypeEdit);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypesService.LeaveTypeExists(leaveTypeEdit.Id))
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
            return View(leaveTypeEdit);
        }

       

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var leaveType = await _context.LeaveTypes
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (leaveType == null)
            //{
            //    return NotFound();
            //}
            //var dataViewModel = _mapper.Map<LeaveTypeDeleteVM>(leaveType);

            var dataViewModel = await _leaveTypesService.Get<LeaveTypeDeleteVM>(id.Value);
            return View(dataViewModel);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var leaveType = await _context.LeaveTypes.FindAsync(id);
            //if (leaveType != null)
            //{
            //    _context.LeaveTypes.Remove(leaveType);
            //}

            //await _context.SaveChangesAsync();
            await _leaveTypesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        
        
    }
}
