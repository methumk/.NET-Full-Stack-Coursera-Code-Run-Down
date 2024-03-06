using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactMVC.Server.Models;

public partial class WorkoutDataModel
{
    [Required]
    public string Exercise { get; set; } = null!;

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Weight lifted cannot be negative")]
    public int Weight { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Must have done at least 1 set")]
    public int Sets { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Must have done at least 0 reps")]
    public int Reps { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Rest time cannot be negative")]
    public double? Rest { get; set; }

    [Required]
    public DateOnly Date { get; set; }
}
