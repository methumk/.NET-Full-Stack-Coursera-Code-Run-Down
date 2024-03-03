using System.ComponentModel.DataAnnotations;

namespace FormAndVal.Models
{
    public class WorkoutInfoGroupedByDate
    {
        public DateOnly Date { get; set; }
        public List<WorkoutInfo> WorkoutInfoForDate  { get; set; }
    }
}
