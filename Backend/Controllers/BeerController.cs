using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _storeContext;

        public BeerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _storeContext.Beers.Select(x => new BeerDto
            {
                Id = x.BeerId,
                Al = x.Al,
                BrandId = x.BrandId,
                Name = x.Name
            }).ToListAsync();

        [HttpGet("id")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Al = beer.Al,
                BrandId = beer.BrandId,
                Name = beer.Name
            };

            return Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer() 
            {
                Name = beerInsertDto.Name,
                BrandId = beerInsertDto.BrandId,
                Al = beerInsertDto.Al
            };
            await _storeContext.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto()
            {
                Id = beer.BeerId,
                Name = beerInsertDto.Name,
                BrandId = beerInsertDto.BrandId,
                Al = beerInsertDto.Al
            };
            return CreatedAtAction(nameof(GetById), new {id = beer.BeerId}, beerDto);
        }
    }
}
