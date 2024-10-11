using System.Text.Json;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services;

public class GoalService : IGoalService
{
    private List<Goal> _goals = new List<Goal>();
    private readonly string _jsonFilePath = "goals.json";

    public GoalService()
    {
        LoadGoalsFromFile();
    }
    
    public List<Goal> GetAllGoals()
    {
        return _goals;
    }

    public Goal GetGoalById(int id)
    {
        return _goals.FirstOrDefault(goal => goal.Id == id) ?? throw new InvalidOperationException();
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
        SaveGoalsToFile();
    }

    public void UpdateGoal(int id, Goal updatedGoal)
    {
        Goal goal = GetGoalById(id);

        if (goal != null)
        {
            goal.Id = updatedGoal.Id;
            goal.Description = updatedGoal.Description;
            goal.Status = updatedGoal.Status;
            goal.CreatedAt = updatedGoal.CreatedAt;
            goal.UpdatedAt = updatedGoal.UpdatedAt;
            
            SaveGoalsToFile();
        }
    }

    public void DeleteGoal(int id)
    {
        _goals.RemoveAll(goal => goal.Id == id);
        SaveGoalsToFile();
    }
    
    private void SaveGoalsToFile()
    {
        try
        {
            string json = JsonSerializer.Serialize(_goals, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    private void LoadGoalsFromFile()
    {
        if (File.Exists(_jsonFilePath))
        {
            try
            {
                string json = File.ReadAllText(_jsonFilePath);
                _goals = JsonSerializer.Deserialize<List<Goal>>(json) ?? new List<Goal>();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error loading goals: {ex.Message}");
                _goals = new List<Goal>(); // Initialize an empty list on failure
            }
        }
    }
}