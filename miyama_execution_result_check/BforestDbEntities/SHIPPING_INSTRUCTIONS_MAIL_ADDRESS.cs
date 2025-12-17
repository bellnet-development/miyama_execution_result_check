using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace miyama_execution_result_check.BforestDbEntities;

[Keyless]
[Table("SHIPPING_INSTRUCTIONS_MAIL_ADDRESS")]
public partial class SHIPPING_INSTRUCTIONS_MAIL_ADDRESS
{
    [StringLength(10)]
    public string SHIPPING_CLASS { get; set; } = null!;

    [StringLength(50)]
    public string? SHIPPING_CLASS_NAME { get; set; }

    [StringLength(10)]
    public string SHIP_CD { get; set; } = null!;

    [StringLength(50)]
    public string? SHIP_NAME { get; set; }

    [StringLength(50)]
    public string? ESTIMATED_OUTPUT_TIME { get; set; }

    [StringLength(10)]
    public string? SHIPPING_LOCATION { get; set; }

    [StringLength(50)]
    public string? SHIPPING_LOCATION_NAME { get; set; }

    [StringLength(500)]
    public string? SHIPPING_MAIL_TO { get; set; }

    [StringLength(500)]
    public string? SHIPPING_MAIL_CC { get; set; }

    [StringLength(100)]
    public string? PRINT_TO { get; set; }
}
