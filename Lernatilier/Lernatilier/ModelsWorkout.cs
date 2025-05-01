namespace FitnessTrackerAPI.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int BenutzerId { get; set; }
        public string Übung { get; set; }
        public int Gewicht { get; set; }
        public int Wiederholungen { get; set; }
        public DateTime Datum { get; set; }
    }
}
