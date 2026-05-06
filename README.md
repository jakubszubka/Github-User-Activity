# GitHubUserActivityCLI

A simple .NET CLI tool to fetch and analyze a GitHub user's recent public activity/events.
You can access the repository here: https://github.com/jakubszubka/Github-User-Activity

## Features

- Fetches GitHub user events via the public GitHub API
- Saves raw JSON response to a file
- Parses events into structured `GitHubEvent` objects
- Displays event statistics (count, unique types)
- Formats and displays recent activity

## Installation

1. Clone or download this repository
2. Build with .NET:
   ```bash
   dotnet build
   ```

## Usage

```bash
dotnet run <github-username>
```

**Example:**
```bash
dotnet run octocat
```

### Output

The tool will:
1. Print a personalized greeting
2. Fetch events from GitHub API
3. Save raw JSON to `<project-root>/<username>_events.json`

Sample output:
Hello, octocat!
The amount of items in events: 30
Event types found: PushEvent, CreateEvent, WatchEvent


## How It Works

1. **API Request**: Hits `https://api.github.com/users/{username}/events`
2. **JSON Parsing**: Uses `System.Text.Json` to extract `type`, `repo.url`, and `created_at`
3. **Data Processing**: Creates `GitHubEvent` objects and finds unique event types
4. **File Output**: Saves full API response to solution root directory

## GitHubEvent Model

```csharp
public class GitHubEvent
{
    public string Type { get; set; }
    public string RepoUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## Requirements

- .NET 6.0 or higher
- No authentication required (uses public GitHub API)

## Files Generated

- `<username>_events.json` - Raw GitHub API response saved in project root

## Limitations

- Only shows public events
- Limited to 30 most recent events (GitHub API default)
- No pagination support
- Basic error handling only

## Extending the Tool

1. Add more event properties to `GitHubEvent`
2. Implement `DisplayEvents()` for formatted output
3. Add pagination with `?page=` parameter
4. Support private events with GitHub token

## License

MIT License - feel free to use and modify!
