using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Core;
using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Areas.Admin.Models;

namespace OzelDersler.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchManager;

        public BranchController(IBranchService branchManager)
        {
            _branchManager = branchManager;
        }

        public async Task<IActionResult> Index()
        {
            var branches = await _branchManager.GetAllAsync();
            var branchListDto = branches
                .Select(b => new BranchListDto
                {
                    Branch = b
                }).ToList();
            return View(branchListDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BranchAddDto branchAddDto)
        {
            if (ModelState.IsValid)
            {
                var url = Jobs.InitUrl(branchAddDto.Name);
                var branch = new Branch
                {
                    BranchName = branchAddDto.Name,
                    CreatedDate = DateTime.Now,
                    Url= url
                };
                await _branchManager.CreateAsync(branch);
                return RedirectToAction("Index");
            }
            return View(branchAddDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var branch = await _branchManager.GetByIdAsync(id);
            if (branch == null) { return NotFound(); }
            var branchUpdateDto = new BranchUpdateDto
            {
                Id = branch.Id,
                Name = branch.BranchName,
                Url = branch.Url
            };
            return View(branchUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BranchUpdateDto branchUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var branch = await _branchManager.GetByIdAsync(branchUpdateDto.Id);
                if(branch == null) { return NotFound(); }
                branch.BranchName = branchUpdateDto.Name;
                branch.ImageUrl = branchUpdateDto.ImageUrl;
                _branchManager.Update(branch);
                return RedirectToAction("Index");
            }
            return View(branchUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var branch = await _branchManager.GetByIdAsync(id);
            if(branch == null) { return NotFound(); }
            _branchManager.Delete(branch);
            return RedirectToAction("Index");
        }
    }
}
