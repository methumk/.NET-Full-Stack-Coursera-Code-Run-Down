using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactMVC.Server.Models;

public partial class Workout : WorkoutDataModel
{
    [Required]
    public string Id { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    public virtual UserModel User { get; set; } = null!;

    public Workout() {}

    public Workout(WorkoutDataModel wdm) 
    {
        Id = Guid.NewGuid().ToString();
        CopyWorkoutData(wdm);
    }

    public void CopyWorkoutData(WorkoutDataModel wdm)
    {
        Exercise = wdm.Exercise;
        Weight = wdm.Weight;
        Sets = wdm.Sets;
        Reps = wdm.Reps;
        Rest = wdm.Rest;
        Date = wdm.Date;
    }
}
