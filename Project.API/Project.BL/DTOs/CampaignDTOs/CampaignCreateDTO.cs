﻿using Project.Core.Entities;

namespace Project.BL.DTOs.CampaignDTOs
{
    public record CampaignCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DistrictId { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
