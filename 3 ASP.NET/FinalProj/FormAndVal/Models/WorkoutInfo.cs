using System;
using System.Collections.Generic;

namespace FormAndVal.Models;

public partial class WorkoutInfo : WorkoutInfoViewModel
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;

    public WorkoutInfo() { }

    public WorkoutInfo(WorkoutInfoViewModel wkim)
    {
        Exercise = wkim.Exercise;
        Weight = wkim.Weight;
        Sets = wkim.Sets;
        Reps = wkim.Reps;
        Rest = wkim.Rest;
        Date = wkim.Date;
    }
}
