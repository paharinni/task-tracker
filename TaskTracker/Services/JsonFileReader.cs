using System.Text.Json;
using TaskTracker.Models;

namespace TaskTracker.Services;

public class JsonFileReader
{
    public static string ReadFileContent(string filePath)
    {
        try
        {
            // Read all text from the specified file
            string content = File.ReadAllText(filePath);
            return content;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The file was not found.");
            return string.Empty; // Return an empty string if file not found
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You do not have permission to read this file.");
            return string.Empty; // Return an empty string if access is denied
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return string.Empty; // Return an empty string for any other errors
        }
    }
}