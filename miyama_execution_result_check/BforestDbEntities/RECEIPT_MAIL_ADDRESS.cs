using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace miyama_execution_result_check.BforestDbEntities;

[Table("RECEIPT_MAIL_ADDRESS")]
public partial class RECEIPT_MAIL_ADDRESS
{
    [Key]
    [StringLength(10)]
    public string C_CUST_CD { get; set; } = null!;

    [StringLength(100)]
    public string? C_CUST_NM { get; set; }

    [StringLength(500)]
    public string? JYURYO_MAIL_ADDRESS { get; set; }

    [StringLength(1)]
    public string? JYURYO_DIV { get; set; }

    [StringLength(500)]
    public string? NOUHIN_MAIL_ADDRESS { get; set; }

    [StringLength(1)]
    public string? NOUHIN_DIV { get; set; }

    [StringLength(500)]
    public string? SEIKYU_MAIL_ADDRESS { get; set; }

    [StringLength(1)]
    public string? SEIKYU_DIV { get; set; }
}
