using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace miyama_execution_result_check.BforestDbEntities;

[Keyless]
[Table("MONITORING_M_CONTROLS")]
public partial class MONITORING_M_CONTROL
{
    [StringLength(5)]
    public string SYSTEM_ID { get; set; } = null!;

    [StringLength(50)]
    public string CLASS_DIV { get; set; } = null!;

    [StringLength(50)]
    public string END_DIV { get; set; } = null!;

    [StringLength(200)]
    public string? MONITORING_PATH { get; set; }

    [StringLength(50)]
    public string? MONITORING_FILE { get; set; }

    public string? MAIL_TO { get; set; }

    public string? MAIL_CC { get; set; }

    public string? MAIL_SUBJECT { get; set; }

    public string? MAIL_BODY { get; set; }

    [StringLength(200)]
    public string? BACK_PATH { get; set; }

    public string? MEMO { get; set; }
}
