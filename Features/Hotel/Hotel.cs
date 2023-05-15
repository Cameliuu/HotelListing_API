﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing_API.Features.Hotel;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
    [ForeignKey(nameof(CountryId))]
    public int CountryId { get; set; }
    public Country.Country Country { get; set; }
}