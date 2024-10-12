using System.Text.Json;
using TaskTracker.Domain.Entities;
using TaskTracker.Interfaces;

namespace TaskTracker.Services;

public class GoalService : IGoalService
{
    private List<Goal> _goals = new List<Goal>();
    private readonly string _jsonFilePath = "goals.json";

    public GoalService()
    {
        LoadGoalsFromFileAsync();
    }
    
    public List<Goal> GetAllGoals()
    {
        return _goals;
    }

    public Goal GetGoalById(int id)
    {
        return _goals.FirstOrDefault(goal => goal.Id == id);
    }

    public async void AddGoal(Goal goal)
    {
        _goals.Add(goal);
        await SaveGoalsToFileAsync();
    }

    public async void UpdateGoal(int id, Goal updatedGoal)
    {
        Goal goal = GetGoalById(id);

        if (goal != null)
        {
            goal.Id = updatedGoal.Id;
            goal.Description = updatedGoal.Description;
            goal.Status = updatedGoal.Status;
            goal.CreatedAt = updatedGoal.CreatedAt;
            goal.UpdatedAt = updatedGoal.UpdatedAt;
            
            await SaveGoalsToFileAsync();
        }
    }

    public async void DeleteGoal(int id)
    {
        _goals.RemoveAll(goal => goal.Id == id);
        await SaveGoalsToFileAsync();
    }
    
    private async Task SaveGoalsToFileAsync()
    {
        try
        {
            string json = JsonSerializer.Serialize(_goals, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_jsonFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    private async Task LoadGoalsFromFileAsync()
    {
        if (File.Exists(_jsonFilePath))
        {
            try
            {
                string json = await File.ReadAllTextAsync(_jsonFilePath);
                _goals = JsonSerializer.Deserialize<List<Goal>>(json) ?? new List<Goal>();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error loading goals: {ex.Message}");
                _goals = new List<Goal>();
            }
        }
        else
        {
            _goals = new List<Goal>();
        }
    }
}