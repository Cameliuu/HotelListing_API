    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using HotelListing_API;
    using HotelListing_API.Contracts;
    using HotelListing_API.Features.Country;
    using Microsoft.AspNetCore.Authorization;

    namespace HotelListing_API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CountryController : ControllerBase
        {   
            private readonly IMapper _mapper;
            private readonly ICountriesRepository _repository;

            public CountryController(AppDbContext context, IMapper mapper, ICountriesRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            // GET: api/Country
            [HttpGet]
            public async Task<ActionResult<IEnumerable<GetCountryModel>>> GetCountries()
            {
                var countries = await _repository.GetAllAsync();
               var records = _mapper.Map<List<GetCountryModel>>(countries);
               return records;
            }

            // GET: api/Country/5
            [HttpGet("{id}")]
            public async Task<ActionResult<GetCountryDetailsModel>> GetCountry(int id)
            {
                var country = await _repository.GetDetails(id);
                
                
                var countryModel = _mapper.Map<GetCountryDetailsModel>(country);


                return countryModel;
            }

            // PUT: api/Country/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            [Authorize(Roles = "Administrator")]
            public async Task<IActionResult> PutCountry(int id, UpdateCountryModel entity)
            {
                if (id != entity.Id)
                {
                    return BadRequest("Invalid ID");
                }

                var country = await _repository.GetAsync(id);
                if (country == null)
                {
                    return NotFound();
                }   
                _mapper.Map(entity, country);

                await _repository.UpdateAsync(country);
                return NoContent();
            }

            // POST: api/Country
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            [Authorize(Roles = "Administrator")]
            public async Task<ActionResult<Country>> PostCountry(CreateCountryModel entity)
            {
                var country = _mapper.Map<Country>(entity);
                await _repository.AddAsync(country);
                return CreatedAtAction("GetCountry", new { id = country.Id }, country);
            }

            // DELETE: api/Country/5
            [HttpDelete("{id}")]
            [Authorize(Roles = "Administrator")]
            public async Task<IActionResult> DeleteCountry(int id)
            {
                var country = await _repository.GetAsync(id);
                if (country == null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync(id);
                return NoContent();
            }

            private async Task<bool> CountryExists(int id)
            {
                return await _repository.Exists(id);
            }
        }
    }
