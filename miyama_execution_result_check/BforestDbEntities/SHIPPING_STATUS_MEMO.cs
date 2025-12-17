using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace miyama_execution_result_check.BforestDbEntities;

[Keyless]
[Table("SHIPPING_STATUS_MEMO")]
public partial class SHIPPING_STATUS_MEMO
{
    [StringLength(50)]
    public string? 出荷日 { get; set; }

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? 表示順 { get; set; }

    [StringLength(1)]
    public string? C_SHIP_DIV { get; set; }

    [StringLength(50)]
    public string? C_SHIP_DIV_NAME { get; set; }

    [StringLength(50)]
    public string? 出力時間 { get; set; }

    [StringLength(5)]
    public string? C_SERVICE_DIV { get; set; }

    [StringLength(50)]
    public string? C_SERVICE_DIV_NAME { get; set; }

    [StringLength(15)]
    public string? C_CUST_CD { get; set; }

    [StringLength(2)]
    public string? C_CUST_SBNO { get; set; }

    [StringLength(50)]
    public string? C_CUST_SNAME { get; set; }

    [StringLength(15)]
    public string? C_DESTINATION_CD { get; set; }

    [StringLength(2)]
    public string? C_DESTINATION_SBNO { get; set; }

    [StringLength(50)]
    public string? C_DESTINATION_SNAME { get; set; }

    [StringLength(5)]
    public string? N_LT { get; set; }

    [StringLength(50)]
    public string? 備考 { get; set; }

    [StringLength(10)]
    public string? JWまとめ時間 { get; set; }
}
