using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserActivityCLI.Models;

internal class GitHubEvent
{
    public  string Type { get; set; }
    public  string RepoUrl { get; set; }
    public  DateTime CreatedAt { get; set; }
    //public GitHubEvent(string type, string repoUrl, DateTime createdAt)
    //{
    //    Type = type;
    //    RepoUrl = repoUrl;
    //    CreatedAt = createdAt;
    //}
}
