using ExerciseApi.Models;
using Newtonsoft.Json;

public class ExerciseService
{
	private readonly List<Exercise> _exercises;

	public ExerciseService()
	{
		var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/exercises.json");
		_exercises = JsonConvert.DeserializeObject<List<Exercise>>(System.IO.File.ReadAllText(jsonFilePath));
	}

	private List<T> ApplyPagination<T>(IEnumerable<T> items, int page, int size)
	{
		return items.Skip((page - 1) * size).Take(size).ToList();
	}

	public List<Exercise> GetAllExercises(int page, int size)
	{
		return ApplyPagination(_exercises, page, size);
	}

	public List<Exercise> GetExercisesByLevelEquipmentMuscle(string level, string[] equipment, string muscle, int page, int size)
	{
		var decodedLevel = Uri.UnescapeDataString(level).Replace("_", " ");
		var decodedEquipment = equipment.Select(e => Uri.UnescapeDataString(e).Replace("_", " ")).ToArray();
		var decodedMuscle = Uri.UnescapeDataString(muscle).Replace("_", " ");

		var filteredExercises = _exercises
			.Where(e => e.Level != null && e.Level.Equals(decodedLevel, StringComparison.OrdinalIgnoreCase) &&
						e.Equipment != null && decodedEquipment.Contains(e.Equipment, StringComparer.OrdinalIgnoreCase) &&
						((e.PrimaryMuscles != null && e.PrimaryMuscles.Contains(decodedMuscle, StringComparer.OrdinalIgnoreCase)) ||
						 (e.SecondaryMuscles != null && e.SecondaryMuscles.Contains(decodedMuscle, StringComparer.OrdinalIgnoreCase))))
			.ToList();

		return ApplyPagination(filteredExercises, page, size);
	}

	public List<Exercise> GetExercisesByLevelEquipmentForce(string level, string[] equipment, string force, int page, int size)
	{
		var decodedLevel = Uri.UnescapeDataString(level).Replace("_", " ");
		var decodedEquipment = equipment.Select(e => Uri.UnescapeDataString(e).Replace("_", " ")).ToArray();
		var decodedForce = Uri.UnescapeDataString(force).Replace("_", " ");

		var filteredExercises = _exercises
			.Where(e => e.Level != null && e.Level.Equals(decodedLevel, StringComparison.OrdinalIgnoreCase) &&
						e.Equipment != null && decodedEquipment.Contains(e.Equipment, StringComparer.OrdinalIgnoreCase) &&
						e.Force != null && e.Force.Equals(decodedForce, StringComparison.OrdinalIgnoreCase))
			.ToList();

		return ApplyPagination(filteredExercises, page, size);
	}

	public List<string> GetAllExerciseNames(int page, int size)
	{
		var exerciseNames = _exercises.Select(e => e.Name).ToList();
		return ApplyPagination(exerciseNames, page, size);
	}

	public List<string> GetAllForces(int page, int size)
	{
		var forces = _exercises.Select(e => e.Force).Where(f => f != null).Distinct().ToList();
		return ApplyPagination(forces, page, size);
	}

	public List<string> GetAllMuscles(int page, int size)
	{
		var primaryMuscles = _exercises.SelectMany(e => e.PrimaryMuscles ?? new List<string>()).Distinct().ToList();
		var secondaryMuscles = _exercises.SelectMany(e => e.SecondaryMuscles ?? new List<string>()).Distinct().ToList();
		var allMuscles = primaryMuscles.Union(secondaryMuscles).ToList();
		return ApplyPagination(allMuscles, page, size);
	}

	public List<Exercise> GetExerciseById(string id)
	{
		var decodedId = Uri.UnescapeDataString(id).Replace("_", " ");
		return _exercises.Where(e => e.Id != null && e.Id.IndexOf(decodedId, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
	}
}
