using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactMVC.Server.Models;

public class WorkoutGrouped
{
    public DateOnly Date { get; set; }
    public List<Workout> WorkoutInfoByDate  { get; set; }
}
