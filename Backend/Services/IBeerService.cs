using Backend.DTOs;

namespace Backend.Services
{
    public interface IBeerService
    {
        Task<IEnumerable<BeerDto>> Get();

        Task<BeerDto> GetById(int id);

        Task<BeerDto> Insert(BeerInsertDto insertBeerDto);

        Task<BeerDto> Update(int  id, BeerUpdateDto updateBeerDto);

        Task<BeerDto> Delete(int id);
    }
}
