using Microsoft.AspNetCore.Mvc;
using FitnessTrackerAPI.Models;
using FitnessTrackerAPI.Helpers;

namespace FitnessTrackerAPI.Controllers
{
    [Route("api/workouts")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private const string FilePath = "workouts.json";

        [HttpGet]
        public ActionResult<IEnumerable<Workout>> Get()
        {
            var workouts = JsonFileHelper.Load<Workout>(FilePath);
            return Ok(workouts);
        }

        [HttpPost]
        public ActionResult<Workout> Add(Workout workout)
        {
            var workouts = JsonFileHelper.Load<Workout>(FilePath);
            workout.Id = workouts.Count > 0 ? workouts.Max(w => w.Id) + 1 : 1;
            workout.Datum = DateTime.Now;

            workouts.Add(workout);
            JsonFileHelper.Save(FilePath, workouts);

            return Ok(workout);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var workouts = JsonFileHelper.Load<Workout>(FilePath);
            var workout = workouts.FirstOrDefault(w => w.Id == id);

            if (workout == null)
                return NotFound();

            workouts.Remove(workout);
            JsonFileHelper.Save(FilePath, workouts);

            return NoContent();
        }
    }
}
