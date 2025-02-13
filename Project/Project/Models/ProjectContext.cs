using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project.Models;

public partial class ProjectContext : DbContext
{
    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BondAttribute> BondAttributes { get; set; }

    public virtual DbSet<BondBond> BondBonds { get; set; }

    public virtual DbSet<BondTab> BondTabs { get; set; }

    public virtual DbSet<EquityAttribute> EquityAttributes { get; set; }

    public virtual DbSet<EquityEquity> EquityEquities { get; set; }

    public virtual DbSet<EquityTab> EquityTabs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=192.168.0.13\\sqlexpress,49753; Database=Project; User Id=sa; Password=sa@12345678; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquityEquity>()
        .Property(e => e.SecurityId)
        .ValueGeneratedOnAdd();  // Allow SQL Server to handle Identity

        modelBuilder.Entity<EquityEquity>()
            .ToTable("Equity_Equities", t => t.HasTrigger("Equity_Insert"));

        

        modelBuilder.Entity<BondBond>()
        .Property(e => e.SecurityId)
        .ValueGeneratedOnAdd();  // Allow SQL Server to handle Identity

        modelBuilder.Entity<BondBond>()
            .ToTable("Bond_Bonds", t => t.HasTrigger("Bond_Insert"));


        modelBuilder.Entity<BondAttribute>(entity =>
        {
            entity.HasKey(e => e.Aid).HasName("Bond_Attributes_AID_PK");

            entity.ToTable("Bond_Attributes");

            entity.Property(e => e.Aid).HasColumnName("AID");
            entity.Property(e => e.Aname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AName");
            entity.Property(e => e.TabId).HasColumnName("TabID");

            entity.HasOne(d => d.Tab).WithMany(p => p.BondAttributes)
                .HasForeignKey(d => d.TabId)
                .HasConstraintName("Bond_Attributes_TabID_FK");
        });

        modelBuilder.Entity<BondBond>(entity =>
        {
            entity.HasKey(e => e.SecurityId);

            entity.ToTable("Bond_Bonds");

            entity.Property(e => e.SecurityId).HasColumnName("Security ID");
            entity.Property(e => e.AskPrice).HasColumnName("Ask Price");
            entity.Property(e => e.AssetType)
                .HasMaxLength(50)
                .HasColumnName("Asset Type");
            entity.Property(e => e.AverageVolume30d).HasColumnName("Average Volume 30D");
            entity.Property(e => e.BidPrice).HasColumnName("Bid Price");
            entity.Property(e => e.BloombergIndustryGroup)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Industry Group");
            entity.Property(e => e.BloombergIndustrySubGroup)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Industry Sub Group");
            entity.Property(e => e.BloombergSector)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Sector");
            entity.Property(e => e.BloombergTicker)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Ticker");
            entity.Property(e => e.BloombergUniqueId)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Unique ID");
            entity.Property(e => e.CallDate).HasColumnName("Call Date");
            entity.Property(e => e.CallPrice).HasColumnName("Call Price");
            entity.Property(e => e.CouponCap).HasColumnName("Coupon Cap");
            entity.Property(e => e.CouponFloor).HasColumnName("Coupon Floor");
            entity.Property(e => e.CouponFrequency)
                .HasMaxLength(50)
                .HasColumnName("Coupon Frequency");
            entity.Property(e => e.CouponRate).HasColumnName("Coupon Rate");
            entity.Property(e => e.CouponType)
                .HasMaxLength(50)
                .HasColumnName("Coupon Type");
            entity.Property(e => e.Cusip)
                .HasMaxLength(20)
                .HasColumnName("CUSIP");
            entity.Property(e => e.FirstCouponDate).HasColumnName("First Coupon Date");
            entity.Property(e => e.FloatSpread).HasColumnName("Float Spread");
            entity.Property(e => e.FormPfAssetClass)
                .HasMaxLength(50)
                .HasColumnName("Form PF Asset Class");
            entity.Property(e => e.FormPfCountry)
                .HasMaxLength(50)
                .HasColumnName("Form PF Country");
            entity.Property(e => e.FormPfCreditRating)
                .HasMaxLength(50)
                .HasColumnName("Form PF Credit Rating");
            entity.Property(e => e.FormPfCurrency)
                .HasMaxLength(10)
                .HasColumnName("Form PF Currency");
            entity.Property(e => e.FormPfInstrument)
                .HasMaxLength(50)
                .HasColumnName("Form PF Instrument");
            entity.Property(e => e.FormPfLiquidityProfile)
                .HasMaxLength(50)
                .HasColumnName("Form PF Liquidity Profile");
            entity.Property(e => e.FormPfMaturity)
                .HasMaxLength(50)
                .HasColumnName("Form PF Maturity");
            entity.Property(e => e.FormPfNaicsCode)
                .HasMaxLength(50)
                .HasColumnName("Form PF NAICS Code");
            entity.Property(e => e.FormPfRegion)
                .HasMaxLength(50)
                .HasColumnName("Form PF Region");
            entity.Property(e => e.FormPfSector)
                .HasMaxLength(50)
                .HasColumnName("Form PF Sector");
            entity.Property(e => e.FormPfSubAssetClass)
                .HasMaxLength(50)
                .HasColumnName("Form PF Sub Asset Class");
            entity.Property(e => e.HasPosition).HasColumnName("Has Position");
            entity.Property(e => e.HighPrice).HasColumnName("High Price");
            entity.Property(e => e.InvestmentType)
                .HasMaxLength(50)
                .HasColumnName("Investment Type");
            entity.Property(e => e.IsCallable).HasColumnName("Is Callable");
            entity.Property(e => e.IsFixToFloat).HasColumnName("Is Fix to Float");
            entity.Property(e => e.IsPutable).HasColumnName("Is Putable");
            entity.Property(e => e.Isin)
                .HasMaxLength(20)
                .HasColumnName("ISIN");
            entity.Property(e => e.IssueCountry)
                .HasMaxLength(50)
                .HasColumnName("Issue Country");
            entity.Property(e => e.IssueCurrency)
                .HasMaxLength(50)
                .HasColumnName("Issue Currency");
            entity.Property(e => e.IssueDate).HasColumnName("Issue Date");
            entity.Property(e => e.Issuer).HasMaxLength(50);
            entity.Property(e => e.LastPrice).HasColumnName("Last Price");
            entity.Property(e => e.LastResetDate).HasColumnName("Last Reset Date");
            entity.Property(e => e.LowPrice).HasColumnName("Low Price");
            entity.Property(e => e.MaturityDate).HasColumnName("Maturity Date");
            entity.Property(e => e.MaximumCallNoticeDays).HasColumnName("Maximum Call Notice Days");
            entity.Property(e => e.MaximumPutNoticeDays).HasColumnName("Maximum Put Notice Days");
            entity.Property(e => e.OpenPrice).HasColumnName("Open Price");
            entity.Property(e => e.PenultimateCouponDate).HasColumnName("Penultimate Coupon Date");
            entity.Property(e => e.PricingFactor).HasColumnName("Pricing Factor");
            entity.Property(e => e.PutDate).HasColumnName("Put Date");
            entity.Property(e => e.PutPrice).HasColumnName("Put Price");
            entity.Property(e => e.ResetFrequency)
                .HasMaxLength(50)
                .HasColumnName("Reset Frequency");
            entity.Property(e => e.RiskCurrency)
                .HasMaxLength(50)
                .HasColumnName("Risk Currency");
            entity.Property(e => e.SecurityDescription)
                .HasMaxLength(50)
                .HasColumnName("Security Description");
            entity.Property(e => e.SecurityName)
                .HasMaxLength(50)
                .HasColumnName("Security Name");
            entity.Property(e => e.Sedol)
                .HasMaxLength(20)
                .HasColumnName("SEDOL");
            entity.Property(e => e.TradingFactor).HasColumnName("Trading Factor");
            entity.Property(e => e.Volatility30d).HasColumnName("Volatility 30D");
            entity.Property(e => e.Volatility90d).HasColumnName("Volatility 90D");
        });

        modelBuilder.Entity<BondTab>(entity =>
        {
            entity.HasKey(e => e.TabId).HasName("Bond_Tabs_TabID_PK");

            entity.ToTable("Bond_Tabs");

            entity.Property(e => e.TabId).HasColumnName("TabID");
            entity.Property(e => e.TabName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EquityAttribute>(entity =>
        {
            entity.HasKey(e => e.Aid).HasName("Equity_Attributes_AID_PK");

            entity.ToTable("Equity_Attributes");

            entity.Property(e => e.Aid).HasColumnName("AID");
            entity.Property(e => e.Aname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AName");
            entity.Property(e => e.TabId).HasColumnName("TabID");

            entity.HasOne(d => d.Tab).WithMany(p => p.EquityAttributes)
                .HasForeignKey(d => d.TabId)
                .HasConstraintName("Equity_Attributes_TabID");
        });

        modelBuilder.Entity<EquityEquity>(entity =>
        {
            entity.HasKey(e => e.SecurityId);

            entity.ToTable("Equity_Equities");

            entity.Property(e => e.SecurityId).HasColumnName("Security ID");
            entity.Property(e => e.AdrUnderlyingCurrency)
                .HasMaxLength(10)
                .HasColumnName("ADR Underlying Currency");
            entity.Property(e => e.AdrUnderlyingTicker)
                .HasMaxLength(50)
                .HasColumnName("ADR Underlying Ticker");
            entity.Property(e => e.AskPrice).HasColumnName("Ask Price");
            entity.Property(e => e.BidPrice).HasColumnName("Bid Price");
            entity.Property(e => e.BloombergGlobalId)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Global ID");
            entity.Property(e => e.BloombergIndustryGroup)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Industry Group");
            entity.Property(e => e.BloombergIndustrySector)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Industry Sector");
            entity.Property(e => e.BloombergIndustrySubGroup)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Industry Sub Group");
            entity.Property(e => e.BloombergTicker)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Ticker");
            entity.Property(e => e.BloombergTickerAndExchange)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Ticker and Exchange");
            entity.Property(e => e.BloombergUniqueId)
                .HasMaxLength(50)
                .HasColumnName("Bloomberg Unique ID");
            entity.Property(e => e.BloombergUniqueName)
                .HasMaxLength(255)
                .HasColumnName("Bloomberg Unique Name");
            entity.Property(e => e.ClosePrice).HasColumnName("Close Price");
            entity.Property(e => e.CountryOfIncorporation)
                .HasMaxLength(50)
                .HasColumnName("Country of Incorporation");
            entity.Property(e => e.Cusip)
                .HasMaxLength(20)
                .HasColumnName("CUSIP");
            entity.Property(e => e.DeclaredDate).HasColumnName("Declared Date");
            entity.Property(e => e.DividendType)
                .HasMaxLength(50)
                .HasColumnName("Dividend Type");
            entity.Property(e => e.ExDate).HasColumnName("Ex Date");
            entity.Property(e => e.Exchange).HasMaxLength(50);
            entity.Property(e => e.FormPfAssetClass)
                .HasMaxLength(50)
                .HasColumnName("Form PF Asset Class");
            entity.Property(e => e.FormPfCountry)
                .HasMaxLength(50)
                .HasColumnName("Form PF Country");
            entity.Property(e => e.FormPfCreditRating)
                .HasMaxLength(50)
                .HasColumnName("Form PF Credit Rating");
            entity.Property(e => e.FormPfCurrency)
                .HasMaxLength(10)
                .HasColumnName("Form PF Currency");
            entity.Property(e => e.FormPfInstrument)
                .HasMaxLength(50)
                .HasColumnName("Form PF Instrument");
            entity.Property(e => e.FormPfLiquidityProfile)
                .HasMaxLength(50)
                .HasColumnName("Form PF Liquidity Profile");
            entity.Property(e => e.FormPfMaturity)
                .HasMaxLength(50)
                .HasColumnName("Form PF Maturity");
            entity.Property(e => e.FormPfNaicsCode)
                .HasMaxLength(50)
                .HasColumnName("Form PF NAICS Code");
            entity.Property(e => e.FormPfRegion)
                .HasMaxLength(50)
                .HasColumnName("Form PF Region");
            entity.Property(e => e.FormPfSector)
                .HasMaxLength(50)
                .HasColumnName("Form PF Sector");
            entity.Property(e => e.FormPfSubAssetClass)
                .HasMaxLength(50)
                .HasColumnName("Form PF Sub Asset Class");
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.HasPosition).HasColumnName("Has Position");
            entity.Property(e => e.IpoDate).HasColumnName("IPO Date");
            entity.Property(e => e.IsActive).HasColumnName("Is Active");
            entity.Property(e => e.IsAdr).HasColumnName("Is ADR");
            entity.Property(e => e.Isin)
                .HasMaxLength(20)
                .HasColumnName("ISIN");
            entity.Property(e => e.IssueCountry)
                .HasMaxLength(50)
                .HasColumnName("Issue Country");
            entity.Property(e => e.IssueCurrency)
                .HasMaxLength(50)
                .HasColumnName("Issue Currency");
            entity.Property(e => e.Issuer).HasMaxLength(50);
            entity.Property(e => e.LastPrice).HasColumnName("Last Price");
            entity.Property(e => e.OpenPrice).HasColumnName("Open Price");
            entity.Property(e => e.PayDate).HasColumnName("Pay Date");
            entity.Property(e => e.PeRatio).HasColumnName("PE Ratio");
            entity.Property(e => e.PriceCurrency)
                .HasMaxLength(10)
                .HasColumnName("Price Currency");
            entity.Property(e => e.RecordDate).HasColumnName("Record Date");
            entity.Property(e => e.RiskCurrency)
                .HasMaxLength(50)
                .HasColumnName("Risk Currency");
            entity.Property(e => e.RoundLotSize).HasColumnName("Round Lot Size");
            entity.Property(e => e.SecurityDescription)
                .HasMaxLength(50)
                .HasColumnName("Security Description");
            entity.Property(e => e.SecurityName)
                .HasMaxLength(50)
                .HasColumnName("Security Name");
            entity.Property(e => e.Sedol)
                .HasMaxLength(20)
                .HasColumnName("SEDOL");
            entity.Property(e => e.SettleDays).HasColumnName("Settle Days");
            entity.Property(e => e.SharesOutstanding).HasColumnName("Shares Outstanding");
            entity.Property(e => e.SharesPerAdr).HasColumnName("Shares Per ADR");
            entity.Property(e => e.ShortInterest).HasColumnName("Short Interest");
            entity.Property(e => e.TradingCurrency)
                .HasMaxLength(50)
                .HasColumnName("Trading Currency");
            entity.Property(e => e.VotingRightsPerShare).HasColumnName("Voting Rights Per Share");
            entity.Property(e => e.YtdReturn).HasColumnName("YTD Return");
            entity.Property(e => e._20DayAverageVolume).HasColumnName("20 Day Average Volume");
            entity.Property(e => e._90DayPriceVolatility).HasColumnName("90 Day Price Volatility");
        });

        modelBuilder.Entity<EquityTab>(entity =>
        {
            entity.HasKey(e => e.TabId).HasName("Equity_Tabs_TabID_PK");

            entity.ToTable("Equity_Tabs");

            entity.Property(e => e.TabId).HasColumnName("TabID");
            entity.Property(e => e.TabName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
