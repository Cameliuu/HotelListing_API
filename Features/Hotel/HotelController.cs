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
using HotelListing_API.Repository;

namespace HotelListing_API.Features.Hotel
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _repository;


        public HotelController(IMapper mapper, IHotelsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelModel>>> GetHotels()
        { 
            var hotels = await _repository.GetAllAsync();
            var records = _mapper.Map<List<GetHotelModel>>(hotels);
            return Ok(records);
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelModel>> GetHotel(int id)
        {
            var hotel = await _repository.GetAsync(id);
            
            if (hotel == null)
            {
                return NotFound();
            }
            var hotelView = _mapper.Map<GetHotelModel>(hotel);

            return hotelView;
        }

        // PUT: api/Hotel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID Not found");
            }

            var hotel = await _repository.GetAsync(id);

            if (hotel is null)
                return NotFound();
            await _repository.UpdateAsync(hotel);
            return Ok();
        }

        // POST: api/Hotel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelModel entity)
        {
            var hotel = _mapper.Map<Hotel>(entity);
            await _repository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _repository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
