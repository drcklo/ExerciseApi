using ExerciseApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
	private readonly ExerciseService _exerciseService;

	public ExercisesController()
	{
		_exerciseService = new ExerciseService();
	}

	[HttpGet]
	public ActionResult<IEnumerable<Exercise>> GetAllExercises(int page = 1, int size = 10)
	{
		var exercises = _exerciseService.GetAllExercises(page, size);
		return Ok(exercises);
	}

	[HttpGet("filter/level-equipment-muscle")]
	public ActionResult<IEnumerable<Exercise>> GetExercisesByLevelEquipmentMuscle(
		[FromQuery] string level,
		[FromQuery] string[] equipment,
		[FromQuery] string muscle,
		int page = 1, int size = 10)
	{
		var exercises = _exerciseService.GetExercisesByLevelEquipmentMuscle(level, equipment, muscle, page, size);
		return Ok(exercises);
	}

	[HttpGet("filter/level-equipment-force")]
	public ActionResult<IEnumerable<Exercise>> GetExercisesByLevelEquipmentForce(
		[FromQuery] string level,
		[FromQuery] string[] equipment,
		[FromQuery] string force,
		int page = 1, int size = 10)
	{
		var exercises = _exerciseService.GetExercisesByLevelEquipmentForce(level, equipment, force, page, size);
		return Ok(exercises);
	}

	[HttpGet("list")]
	public ActionResult<IEnumerable<string>> GetAllExerciseNames(int page = 1, int size = 10)
	{
		var exerciseNames = _exerciseService.GetAllExerciseNames(page, size);
		return Ok(exerciseNames);
	}

	[HttpGet("forces")]
	public ActionResult<IEnumerable<string>> GetAllForces(int page = 1, int size = 10)
	{
		var forces = _exerciseService.GetAllForces(page, size);
		return Ok(forces);
	}

	[HttpGet("muscles")]
	public ActionResult<IEnumerable<string>> GetAllMuscles(int page = 1, int size = 10)
	{
		var muscles = _exerciseService.GetAllMuscles(page, size);
		return Ok(muscles);
	}

	[HttpGet("{id}")]
	public ActionResult<Exercise> GetExerciseById(string id)
	{
		var exercise = _exerciseService.GetExerciseById(id);
		if (exercise == null)
		{
			return NotFound();
		}

		return Ok(exercise);
	}
}
