using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace miyama_execution_result_check.BforestDbEntities;

[Table("LABEL_M_LABEL_STORE")]
public partial class LABEL_M_LABEL_STORE
{
    [Key]
    [StringLength(5)]
    public string STORE_CODE { get; set; } = null!;

    [StringLength(100)]
    public string? STORE_NAME { get; set; }

    [StringLength(500)]
    public string? LABEL_PATH { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CRT_DATETIME { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UPDATE_DATETIME { get; set; }
}
