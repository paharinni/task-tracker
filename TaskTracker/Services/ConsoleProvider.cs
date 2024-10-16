using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Enums;

namespace TaskTracker.Services;

public class ConsoleProvider
{
    public void RunProgram()
    {
        var goalService = new GoalService();
        var running = true;

        while (running)
        {
            Console.WriteLine("\nSelect an operation:");
            Console.WriteLine("1 - List All Tasks");
            Console.WriteLine("2 - Get Task by ID");
            Console.WriteLine("3 - Add New Task");
            Console.WriteLine("4 - Update Task");
            Console.WriteLine("5 - Delete Task");
            Console.WriteLine("0 - Exit");
            Console.Write("Enter choice: ");
            var choice = Console.ReadLine();

            string? description;
            Status status;
            DateTime updatedAt;

            switch (choice)
            {
                case "1":
                    var goals = goalService.GetAllGoals();
                    Console.WriteLine("\nTasks:");
                    foreach (var goal in goals)
                        Console.WriteLine(
                            $"ID: {goal.Id}, Description: {goal.Description}, Status: {goal.Status}, CreatedAt: {goal.CreatedAt}, UpdatedAt: {goal.UpdatedAt}");
                    break;

                case "2":
                    Console.Write("Enter Task ID: ");
                    if (int.TryParse(Console.ReadLine(), out var id))
                    {
                        var goal = goalService.GetGoalById(id);

                        if (goal != null)
                        {
                            Console.WriteLine(
                                $"ID: {goal.Id}, Description: {goal.Description}, Status: {goal.Status}, CreatedAt: {goal.CreatedAt}, UpdatedAt: {goal.UpdatedAt}");
                        }
                        else
                        {
                            Console.WriteLine("Task not found.");
                        }
                    }

                    break;

                case "3":
                    Console.Write("Enter Task Description: ");
                    description = Console.ReadLine();

                    Console.Write("Enter Task Status (To do(0), In progress(1), Done(2)): ");
                    status = (Status)Enum.Parse(typeof(Status),
                        Console.ReadLine() ?? throw new InvalidOperationException());

                    var createdAt = DateTime.Now;

                    updatedAt = DateTime.Now;

                    goalService.AddGoal(new Goal
                    {
                        Id = goalService.GetAllGoals().Count + 1,
                        Description = description,
                        Status = status,
                        CreatedAt = createdAt,
                        UpdatedAt = updatedAt
                    });
                    Console.WriteLine("Task added successfully.");
                    break;

                case "4":
                    Console.Write("Enter Task ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        var goal = goalService.GetGoalById(id);
                        if (goal != null)
                        {
                            Console.Write("Enter new Task Description: ");
                            description = Console.ReadLine();

                            Console.Write("Enter new Task Status (To do(0), In progress(1), Done(2)): ");
                            status = (Status)Enum.Parse(typeof(Status),
                                Console.ReadLine() ?? throw new InvalidOperationException());

                            createdAt = goal.CreatedAt;
                            updatedAt = DateTime.Now;

                            goalService.UpdateGoal(id,
                                new Goal
                                {
                                    Id = goalService.GetAllGoals().Last().Id + 1, Description = description,
                                    Status = status, CreatedAt = createdAt, UpdatedAt = updatedAt
                                });
                            Console.WriteLine("Task updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Task not found.");

                        }
                    }

                    break;

                case "5":
                    Console.Write("Enter Task ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        var goal = goalService.GetGoalById(id);
                        if (goal != null)
                        {
                            goalService.DeleteGoal(id);
                            Console.WriteLine("Task deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Task not found.");
                        }
                    }

                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}