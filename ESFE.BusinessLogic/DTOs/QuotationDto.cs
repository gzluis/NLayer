using ESFE.Entities;

namespace ESFE.BusinessLogic.DTOs;

public class CreateQuotationRequest
{
    public long QuotationId { get; set; }

    public string? ClientName { get; set; }

    public string? ClientPhone { get; set; }

    public string? SellerName { get; set; }

    public int? UserId { get; set; }

    public string? PaymentMethodName { get; set; }

    public long QuotationNumber { get; set; }

    public int? ValidityDays { get; set; }

    public DateTime? QuotationRegistration { get; set; }

    public decimal? Total { get; set; }

    public bool QuotationStatus { get; set; }

    public virtual List<CreateQuotationDetailRequest> QuotationDetails { get; set; } = new List<CreateQuotationDetailRequest>();
}

public class CreateQuotationDetailRequest
{
    public long QuotationDetailId { get; set; }

    public long? QuotationId { get; set; }

    public long? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public decimal? Subtotal { get; set; }
    public virtual ProductResponse? Product { get; set; }
}



public class QuotationResponse
{
    public long QuotationId { get; set; }

    public string? ClientName { get; set; }

    public string? ClientPhone { get; set; }

    public string? SellerName { get; set; }

    public int? UserId { get; set; }

    public string? PaymentMethodName { get; set; }

    public long QuotationNumber { get; set; }

    public int? ValidityDays { get; set; }

    public DateTime? QuotationRegistration { get; set; }

    public decimal? Total { get; set; }

    public bool QuotationStatus { get; set; }

    public virtual ICollection<QuotationDetailResponse> QuotationDetails { get; set; } = new List<QuotationDetailResponse>();

    public virtual UserResponse? User { get; set; }
}

public partial class QuotationDetailResponse
{
    public long QuotationDetailId { get; set; }

    public long? QuotationId { get; set; }

    public long? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual ProductResponse? Product { get; set; } 
}
