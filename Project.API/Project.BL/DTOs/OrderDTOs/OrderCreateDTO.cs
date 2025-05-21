namespace Project.BL.DTOs.OrderDTOs
{
    public record OrderCreateDTO
    {
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
    }
}
