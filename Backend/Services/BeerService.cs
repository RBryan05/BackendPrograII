using Backend.Controllers;
using Backend.DTOs;
using Backend.Models;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : IBeerService
    {
        private StoreContext _storeContext;

        public BeerService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<BeerDto> Delete(int id)
        {
            var beer =  await _storeContext.Beers.FindAsync(id);
            if (beer == null)
            {
                return null;
            }
            _storeContext.Remove(beer);
            await _storeContext.SaveChangesAsync();
            var beerDto = new BeerDto()
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Al = beer.Al
            };
            return beerDto;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            return await _storeContext.Beers.Select(x => new BeerDto
            {
                Id = x.BeerId,
                Al = x.Al,
                BrandId = x.BrandId,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);
            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Al = beer.Al,
                    BrandId = beer.BrandId,
                    Name = beer.Name
                };
                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto> Insert(BeerInsertDto beerInsertDto)
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
            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _storeContext.Beers.FindAsync(id);
            if (beer == null)
            {
                return null;
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
            return beerDto;
        }
    }
}
