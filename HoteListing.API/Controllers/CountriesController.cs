using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HoteListing.API.Data;
using HoteListing.API.Models.Country;
using AutoMapper;
using HoteListing.API.Contracts;

namespace HoteListing.API.Controllers

    // Visual Studio created this whole class by Add/Controller,
    // selecting API Controller with actions using Entity Framework
    // and selected a Model and Context class

{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesRepository repository, IMapper mapper)
        {
            _countriesRepository = repository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
            var countriesList = await _countriesRepository.GetAllAsync();
            var countryDtos = _mapper.Map<List<GetCountryDTO>>(countriesList);
            return Ok(countryDtos);
        }


        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null) 
            { 
                return NotFound(); 
            }
            return _mapper.Map<CountryDTO>(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
        {

            if (id != updateCountryDTO.Id) { return BadRequest(); }

            // Always check that the item already exists in the DB.
            var country = await _countriesRepository.GetAsync(id);
            if (country == null) { return NotFound(); }

            // Here something a little different is happening,
            // instead of transforming the DTO into it's model class, 
            // we are using an instance of the model class
            // and updating it with the information present in the DTO
            _mapper.Map(updateCountryDTO, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /*
         * To prevent Overposting, I want to use DTO's. Mainly because I dont want to expose my Country Model into the API
         * 
         */

        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO countryDTO)
        {
            // Use the injected Automapper instance _mapper to map dto into actual Country data model class.
            var country = _mapper.Map<Country>(countryDTO);

            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                await _countriesRepository.DeleteAsync(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
