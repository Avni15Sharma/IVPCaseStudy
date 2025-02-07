using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class EquityEquity
{
    public int SecurityId { get; set; }

    public string SecurityName { get; set; } = null!;

    public string? SecurityDescription { get; set; }

    public bool? HasPosition { get; set; }

    public bool? IsActive { get; set; }

    public int? RoundLotSize { get; set; }

    public string? BloombergUniqueName { get; set; }

    public string? Cusip { get; set; }

    public string? Isin { get; set; }

    public string? Sedol { get; set; }

    public string? BloombergTicker { get; set; }

    public string? BloombergUniqueId { get; set; }

    public string? BloombergGlobalId { get; set; }

    public string? BloombergTickerAndExchange { get; set; }

    public bool? IsAdr { get; set; }

    public string? AdrUnderlyingTicker { get; set; }

    public string? AdrUnderlyingCurrency { get; set; }

    public int? SharesPerAdr { get; set; }

    public DateOnly? IpoDate { get; set; }

    public string? PriceCurrency { get; set; }

    public int? SettleDays { get; set; }

    public long? SharesOutstanding { get; set; }

    public double? VotingRightsPerShare { get; set; }

    public long? _20DayAverageVolume { get; set; }

    public double? Beta { get; set; }

    public double? ShortInterest { get; set; }

    public double? YtdReturn { get; set; }

    public double? _90DayPriceVolatility { get; set; }

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

    public string? IssueCountry { get; set; }

    public string? Exchange { get; set; }

    public string? Issuer { get; set; }

    public string? IssueCurrency { get; set; }

    public string? TradingCurrency { get; set; }

    public string? BloombergIndustrySubGroup { get; set; }

    public string? BloombergIndustryGroup { get; set; }

    public string? BloombergIndustrySector { get; set; }

    public string? CountryOfIncorporation { get; set; }

    public string? RiskCurrency { get; set; }

    public double? OpenPrice { get; set; }

    public double? ClosePrice { get; set; }

    public long? Volume { get; set; }

    public double? LastPrice { get; set; }

    public double? AskPrice { get; set; }

    public double? BidPrice { get; set; }

    public double? PeRatio { get; set; }

    public DateOnly? DeclaredDate { get; set; }

    public DateOnly? ExDate { get; set; }

    public DateOnly? RecordDate { get; set; }

    public DateOnly? PayDate { get; set; }

    public double? Amount { get; set; }

    public string? Frequency { get; set; }

    public string? DividendType { get; set; }
}
