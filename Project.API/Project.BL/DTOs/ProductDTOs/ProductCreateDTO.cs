namespace Project.BL.DTOs.ProductDTOs
{
    public record  ProductCreateDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
