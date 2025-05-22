namespace Project.BL.DTOs.ProductDistrictPriceDTOs
{
    public record ProductDistrictPriceReadDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DistrictId { get; set; }
        public decimal Price { get; set; }
      
    }
}
