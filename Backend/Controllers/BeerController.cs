using Backend.DTOs;
using Backend.Models;
using FluentValidation;
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
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;

        public BeerController(StoreContext storeContext, IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator)
        {
            _storeContext = storeContext;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
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
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
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
        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var beer = await _storeContext.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdateDto.Name;
            beer.Al = beerUpdateDto.Al;
            beer.BrandId = beerUpdateDto.BrandId;

            await _storeContext.SaveChangesAsync();
            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Al = beer.Al
            };
            return Ok(beerDto);
        }
    }
}
