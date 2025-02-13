using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class BondBond
{
    public int SecurityId { get; set; }

    public string SecurityName { get; set; } = null!;

    public string? SecurityDescription { get; set; }

    public string? AssetType { get; set; }

    public string? InvestmentType { get; set; }

    public double? TradingFactor { get; set; }

    public double? PricingFactor { get; set; }

    public string? Cusip { get; set; }

    public string? Isin { get; set; }

    public string? Sedol { get; set; }

    public string? BloombergTicker { get; set; }

    public string? BloombergUniqueId { get; set; }

    public DateOnly? FirstCouponDate { get; set; }

    public double? CouponCap { get; set; }

    public double? CouponFloor { get; set; }

    public string? CouponFrequency { get; set; }

    public double? CouponRate { get; set; }

    public string? CouponType { get; set; }

    public double? FloatSpread { get; set; }

    public bool? IsCallable { get; set; }

    public bool? IsFixToFloat { get; set; }

    public bool? IsPutable { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? LastResetDate { get; set; }

    public DateOnly? MaturityDate { get; set; }

    public int? MaximumCallNoticeDays { get; set; }

    public int? MaximumPutNoticeDays { get; set; }

    public DateOnly? PenultimateCouponDate { get; set; }

    public string? ResetFrequency { get; set; }

    public bool? HasPosition { get; set; }

    public double? Duration { get; set; }

    public double? Volatility30d { get; set; }

    public double? Volatility90d { get; set; }

    public double? Convexity { get; set; }

    public long? AverageVolume30d { get; set; }

    public string? FormPfAssetClass { get; set; }

    public string? FormPfCountry { get; set; }

    public string? FormPfCreditRating { get; set; }

    public string? FormPfCurrency { get; set; }

    public string? FormPfInstrument { get; set; }

    public string? FormPfLiquidityProfile { get; set; }

    public string? FormPfMaturity { get; set; }

    public string? FormPfNaicsCode { get; set; }

    public string? FormPfRegion { get; set; }

    public string? FormPfSector { get; set; }

    public string? FormPfSubAssetClass { get; set; }

    public string? BloombergIndustryGroup { get; set; }

    public string? BloombergIndustrySubGroup { get; set; }

    public string? BloombergSector { get; set; }

    public string? IssueCountry { get; set; }

    public string? IssueCurrency { get; set; }

    public string? Issuer { get; set; }

    public string? RiskCurrency { get; set; }

    public DateOnly? PutDate { get; set; }

    public double? PutPrice { get; set; }

    public DateOnly? CallDate { get; set; }

    public double? CallPrice { get; set; }

    public double? AskPrice { get; set; }

    public double? HighPrice { get; set; }

    public double? LowPrice { get; set; }

    public double? OpenPrice { get; set; }

    public long? Volume { get; set; }

    public double? BidPrice { get; set; }

    public double? LastPrice { get; set; }
}
