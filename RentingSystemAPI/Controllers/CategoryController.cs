using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using System.Collections.Generic;

namespace RentingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase

    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CategoryResponse> Get()
        {
            var categories = _categoryService.Get();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        [HttpGet("{id}")]
        public CategoryResponse Get(int id)
        {
            var category = _categoryService.Get(id);

            return _mapper.Map<CategoryResponse>(category);
        }

        [HttpPost]
        public IActionResult Post([FromForm] string name)
        {
            var category = new Category { Name = name };
            _categoryService.Add(category);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch([FromForm] int id, [FromForm] string name)
        {
            return _categoryService.Update(id, name) ? Ok() : (IActionResult)NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }
    }
}