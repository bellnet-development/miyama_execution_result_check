using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace miyama_execution_result_check.BforestDbEntities;

public partial class BforestDbContext : DbContext
{
    public BforestDbContext()
    {
    }

    public BforestDbContext(DbContextOptions<BforestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LABEL_M_LABEL_STORE> LABEL_M_LABEL_STOREs { get; set; }

    public virtual DbSet<LABEL_TR_KASUMI_PRINT_DATum> LABEL_TR_KASUMI_PRINT_DATAs { get; set; }

    public virtual DbSet<MONITORING_M_CONTROL> MONITORING_M_CONTROLs { get; set; }

    public virtual DbSet<RECEIPT_MAIL_ADDRESS> RECEIPT_MAIL_ADDRESSes { get; set; }

    public virtual DbSet<SHIPPING_INSTRUCTIONS_MAIL_ADDRESS> SHIPPING_INSTRUCTIONS_MAIL_ADDRESSes { get; set; }

    public virtual DbSet<SHIPPING_STATUS_MEMO> SHIPPING_STATUS_MEMOs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LABEL_M_LABEL_STORE>(entity =>
        {
            entity.HasKey(e => e.STORE_CODE).HasName("PK_M_LABEL_STORE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
