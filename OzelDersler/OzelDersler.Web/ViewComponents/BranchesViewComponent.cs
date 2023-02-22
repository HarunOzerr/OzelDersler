using OzelDersler.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OzelDersler.Web.ViewComponents
{
    public class BranchesViewComponent : ViewComponent
    {
        private readonly IBranchService _branchManager;
        public BranchesViewComponent(IBranchService branchManager)
        {
            _branchManager = branchManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (RouteData.Values["branchurl"] != null)
            {
                ViewBag.SelectedBranch = RouteData.Values["branchurl"];
            }
            var branches = await _branchManager.GetAllAsync();
            return View(branches);
        }
    }
}
