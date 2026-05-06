
using GitHubUserActivityCLI.Models;
using System.Text.Json;

if (args.Length != 1)
{
    Console.WriteLine("Please provide a single username.");

} else
{
    var username = args[0];

    // Greeting the user
    Console.WriteLine($"Hello, {username}!");

    // Fetching the user's GitHub activity
    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubActivityCli/1.0");

    // GitHub API endpoint for user events
    var githubUserUri = $"https://api.github.com/users/{username}/events";
    var response = await httpClient.GetAsync(githubUserUri);
    var responseJson = await response.Content.ReadAsStringAsync();

    // Saving the response to a file in the solution directory
    var projectDirectory = AppContext.BaseDirectory;          // bin/Debug/...
    var solutionDirectory = Path.Combine(projectDirectory, "..", "..", "..");
    await File.WriteAllTextAsync($"{solutionDirectory}/{username}_events.json", responseJson);

    // Processing the response and generating a list of GitHubEvent objects
    var events = GenerateListOfEvents(responseJson);

    // Displaying the events and some statistics
    Console.WriteLine($"The amount of items in events: {events.Count}");
    Console.WriteLine($"Event types found: {string.Join(", ", FindTypes(events))}");
}

List<GitHubEvent> GenerateListOfEvents(string responseJson)
{
    List<GitHubEvent> events = new();

    using var doc = JsonDocument.Parse(responseJson);

    var root = doc.RootElement;
    if (root.ValueKind == JsonValueKind.Array)
    {
        foreach (var item in root.EnumerateArray())
        {
            // access properties dynamically
            events.Add(new GitHubEvent
            {
                Type = item.GetProperty("type").GetString() ?? "type missing",
                RepoUrl = item.GetProperty("repo").GetProperty("url").GetString() ?? "repo url missing",
                CreatedAt = item.GetProperty("created_at").GetDateTime()
            });
        }
    }
    return events;
}

List<string> FindTypes (List<GitHubEvent> events)
{
    List<string> types = new();
    foreach (var evt in events)
    {
        if (!types.Contains(evt.Type))
        {
            types.Add(evt.Type);
        }
    }
    return types;
}

void DisplayEvents(List<GitHubEvent> events, int amount)
{
    int iterator = 0;
    foreach (var evt in events)
    {
        Console.WriteLine(FormatActivity(evt));
        if (iterator++ > amount) break; // limit to first amount of events
    }
}

string FormatActivity(GitHubEvent evt)
{
    return $"{evt.Type} event occurred at {evt.CreatedAt} in repository {evt.RepoUrl}";
}