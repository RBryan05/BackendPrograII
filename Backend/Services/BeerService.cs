using Backend.Controllers;
using Backend.DTOs;
using Backend.Models;
using FluentValidation;

namespace Backend.Services
{
    public class BeerService : IBeerService
    {
        public Task<BeerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BeerDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> Insert(BeerInsertDto insertBeerDto)
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> Update(int id, BeerUpdateDto updateBeerDto)
        {
            throw new NotImplementedException();
        }
    }
}
