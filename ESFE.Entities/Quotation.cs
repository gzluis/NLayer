using System;
using System.Collections.Generic;

namespace ESFE.Entities;

public partial class Quotation
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

    public virtual ICollection<QuotationDetail> QuotationDetails { get; set; } = new List<QuotationDetail>();

    public virtual User? User { get; set; }
}
