using ESFE.Entities;

namespace ESFE.BusinessLogic.DTOs;

public class CreateProductRequest
{
    public int? BrandId { get; set; }

    public string? SupplierName { get; set; }

    public string? ProductCode { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public string? ProductImage { get; set; }

    public decimal? PriceUnitPurchase { get; set; }

    public decimal? PriceUnitSale { get; set; }

    public int? Stock { get; set; }

    public bool ProductStatus { get; set; }
}

public class UpdateProductRequest
{
    public long ProductId { get; set; }

    public int? BrandId { get; set; }

    public string? SupplierName { get; set; }

    public string? ProductCode { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public string? ProductImage { get; set; }

    public decimal? PriceUnitPurchase { get; set; }

    public decimal? PriceUnitSale { get; set; }

    public int? Stock { get; set; }

    public bool ProductStatus { get; set; }
}

public class ProductResponse
{
    public long ProductId { get; set; }

    public string? SupplierName { get; set; }

    public string? ProductCode { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductImage { get; set; }

    public decimal? PriceUnitSale { get; set; }

    public int? Stock { get; set; }

    public string BrandName { get; set; } = null!;
}


public class ProductByIdResponse
{
    public long ProductId { get; set; }

    public int? BrandId { get; set; }

    public string? SupplierName { get; set; }

    public string? ProductCode { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public string? ProductImage { get; set; }

    public decimal? PriceUnitPurchase { get; set; }

    public decimal? PriceUnitSale { get; set; }

    public int? Stock { get; set; }

    public bool ProductStatus { get; set; } 
}
