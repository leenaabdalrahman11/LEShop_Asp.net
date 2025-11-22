using LEShop.DL.Data;
using LEShop.DL.DTO.Requests;
using LEShop.DL.DTO.Response;
using LEShop.PL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Xml.Linq;

namespace LEShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CategoriesController(ApplicationDbContext context, IStringLocalizer<SharedResources> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        public IActionResult index()
        {
            var categories = _context.Categories.Include(c=>c.Translations).ToList();
            var response = categories.Adapt<List<CategoryResponse>>();
            return Ok(new {message= _localizer["Success"].Value, response });
        }
        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();  
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok();
        }


    }
}