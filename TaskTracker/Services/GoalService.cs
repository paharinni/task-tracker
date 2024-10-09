using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services;

public class GoalService : IGoalService
{
    private readonly List<Goal> _goals = new List<Goal>();
    
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
        }
    }

    public void DeleteGoal(int id)
    {
        _goals.RemoveAll(goal => goal.Id == id);
    }
}