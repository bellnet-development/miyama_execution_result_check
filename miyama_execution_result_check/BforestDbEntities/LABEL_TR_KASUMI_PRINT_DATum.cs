using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace miyama_execution_result_check.BforestDbEntities;

[PrimaryKey("IMPORT_DATE", "IMPORT_NUMBER", "PAYING_CORPORATION_CODE", "INVOICE_NUMBER", "STORE_CODE", "ORDERING_CUSTOMER_CODE", "ORDER_DATE", "DELIVERY_DATE")]
[Table("LABEL_TR_KASUMI_PRINT_DATA")]
public partial class LABEL_TR_KASUMI_PRINT_DATum
{
    [Key]
    public DateTime IMPORT_DATE { get; set; }

    [Key]
    public int IMPORT_NUMBER { get; set; }

    [Key]
    [StringLength(50)]
    public string PAYING_CORPORATION_CODE { get; set; } = null!;

    [StringLength(50)]
    public string? ORDERING_COMPANY_NAME { get; set; }

    [StringLength(50)]
    public string? ORDERING_COMPANY_NAME_KANA { get; set; }

    [Key]
    [StringLength(50)]
    public string INVOICE_NUMBER { get; set; } = null!;

    [StringLength(50)]
    public string? SPECIAL_SALE_PLAN_NUMBER { get; set; }

    [StringLength(50)]
    public string? DIRECT_DELIVERY_DESTINATION_CODE { get; set; }

    [StringLength(50)]
    public string? ORDERING_CENTER_NAME { get; set; }

    [StringLength(50)]
    public string? ORDERING_CENTER_NAME_KANA { get; set; }

    [Key]
    [StringLength(50)]
    public string STORE_CODE { get; set; } = null!;

    [StringLength(50)]
    public string? ORDERING_STORE_NAME { get; set; }

    [StringLength(50)]
    public string? ORDERING_STORE_NAME_KANA { get; set; }

    [Key]
    [StringLength(50)]
    public string ORDERING_CUSTOMER_CODE { get; set; } = null!;

    [StringLength(50)]
    public string? BILLING_CUSTOMER_NAME { get; set; }

    [StringLength(50)]
    public string? BILLING_CUSTOMER_NAME_KANA { get; set; }

    [StringLength(50)]
    public string? CUSTOMER_CODE { get; set; }

    [StringLength(50)]
    public string? CUSTOMER_NAME { get; set; }

    [StringLength(50)]
    public string? CUSTOMER_NAME_KANA { get; set; }

    [StringLength(50)]
    public string? DELIVERY { get; set; }

    [StringLength(50)]
    public string? DEPARTMENT_CODE { get; set; }

    [StringLength(50)]
    public string? DEPARTMENT_NAME { get; set; }

    [Key]
    [StringLength(50)]
    public string ORDER_DATE { get; set; } = null!;

    [Key]
    [StringLength(50)]
    public string DELIVERY_DATE { get; set; } = null!;

    [StringLength(50)]
    public string? SPECIAL_SALE_START_DATE { get; set; }

    [StringLength(50)]
    public string? SPECIAL_SALE_END_DATE { get; set; }

    [StringLength(50)]
    public string? PRODUCT_CATEGORY { get; set; }

    [StringLength(50)]
    public string? PRODUCT_CATEGORY_NAME { get; set; }

    [StringLength(50)]
    public string? ORDER_CATEGORY { get; set; }

    [StringLength(50)]
    public string? ORDER_CATEGORY_NAME { get; set; }

    [StringLength(50)]
    public string? INVOICE_CATEGORY { get; set; }

    [StringLength(50)]
    public string? TAX_CATEGORY { get; set; }

    [StringLength(50)]
    public string? TAX_CATEGORY_NAME { get; set; }

    [StringLength(50)]
    public string? TAX_RATE { get; set; }

    [StringLength(50)]
    public string? CATEGORY_NAME { get; set; }

    [StringLength(50)]
    public string? SPECIAL_SALE_CATEGORY_NAME { get; set; }

    [StringLength(50)]
    public string? LOGISTICS_CATEGORY { get; set; }

    [StringLength(50)]
    public string? LOGISTICS_CATEGORY_NAME { get; set; }

    [StringLength(50)]
    public string? TOTAL_COST_AMOUNT { get; set; }

    [StringLength(50)]
    public string? TOTAL_SELLING_PRICE_AMOUNT { get; set; }

    [StringLength(50)]
    public string? INVOICE_LINE_NUMBER { get; set; }

    [StringLength(50)]
    public string? JAN_CODE { get; set; }

    [StringLength(50)]
    public string? PRODUCT_CODE { get; set; }

    [StringLength(50)]
    public string? PRODUCT_NAME { get; set; }

    [StringLength(50)]
    public string? PRODUCT_NAME_KANA { get; set; }

    [StringLength(50)]
    public string? UNIT_PRICE { get; set; }

    [StringLength(50)]
    public string? COST_AMOUNT { get; set; }

    [StringLength(50)]
    public string? SELLING_PRICE { get; set; }

    [StringLength(50)]
    public string? SELLING_PRICE_AMOUNT { get; set; }

    [StringLength(50)]
    public string? QTY { get; set; }

    [StringLength(50)]
    public string? IRI_QTY { get; set; }

    [StringLength(50)]
    public string? NUMBER_OF_CASES { get; set; }

    [StringLength(50)]
    public string? ORDER_UNIT_CODE { get; set; }

    [StringLength(50)]
    public string? ORDER_UNIT_NAME { get; set; }

    [StringLength(50)]
    public string? FIXED_PRICE_CATEGORY { get; set; }

    [StringLength(50)]
    public string? PROCESSING_DATE_AND_TIME { get; set; }

    [StringLength(50)]
    public string? LOGISTICS_CENTER_CATEGORY { get; set; }

    [StringLength(50)]
    public string? LOGISTICS_CENTER_NAME { get; set; }

    public int? PRINT_FLG { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CRT_DATETIME { get; set; }
}
