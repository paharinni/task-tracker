using TaskTracker.Domain.Entities;

namespace TaskTracker.Interfaces;

public interface IGoalService
{
    List<Goal> GetAllGoals();
    Goal GetGoalById(int id);
    void AddGoal(Goal goal);
    void UpdateGoal(int id, Goal updatedGoal);
    void DeleteGoal(int id);
}