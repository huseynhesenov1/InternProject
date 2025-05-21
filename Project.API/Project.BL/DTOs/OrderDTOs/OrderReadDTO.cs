namespace Project.BL.DTOs.OrderDTOs
{
    public record OrderReadDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public int ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
