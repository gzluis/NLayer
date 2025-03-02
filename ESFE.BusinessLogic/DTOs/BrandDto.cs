namespace ESFE.BusinessLogic.DTOs
{
    public class CreateBrandRequest
    {
        public string BrandName { get; set; }
    }

    public class UpdateBrandRequest
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }

    public class BrandResponse
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }
    }
}
