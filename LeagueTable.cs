using System.Text.RegularExpressions;
namespace BucherFussballLiga
{
  public class LeagueTable
  {
      private Dictionary<string, TeamStats> _teams = new Dictionary<string, TeamStats>();

      public void ReadFile(string folder, int lastMatchday)
      {
        var files = Directory.GetFiles(folder, "day*.txt")
           .Where(f => int.TryParse(Regex.Match(f, @"\d+").Value, out int day) && day <= lastMatchday)
           .OrderBy(f => f)
           .ToArray();

          foreach (var file in files)
          {
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
              var match = ParseLine(line);
              UpdateTeamStats(match);
            }
          }
      }

      private Match ParseLine(string line)
      {
          string pattern = @"(.+?) (\d+):(\d+) (.+)";
          var result = Regex.Match(line, pattern);
          
          if(result.Success)
          {
            return new Match
            {
              TeamA = result.Groups[1].Value,
              ScoreA = int.Parse(result.Groups[2].Value),
              ScoreB = int.Parse(result.Groups[3].Value),
              TeamB = result.Groups[4].Value
            };
          }
          else
          {
            throw new Exception("Invalid filecontent!");
          }
      }

      private void UpdateTeamStats(Match match)
      {
          if (!_teams.ContainsKey(match.TeamA))
          {
              _teams[match.TeamA] = new TeamStats { Name = match.TeamA };
          }

          if (!_teams.ContainsKey(match.TeamB))
          {
              _teams[match.TeamB] = new TeamStats { Name = match.TeamB };
          }

          var teamAStats = _teams[match.TeamA];
          var teamBStats = _teams[match.TeamB];

          teamAStats.GoalsScored += match.ScoreA;
          teamAStats.GoalsConceded += match.ScoreB;
          teamBStats.GoalsScored += match.ScoreB;
          teamBStats.GoalsConceded += match.ScoreA;

          if (match.ScoreA > match.ScoreB)
          {
              teamAStats.Wins++;
              teamAStats.Points += 3;
              teamBStats.Losses++;
          }
          else if (match.ScoreA < match.ScoreB)
          {
              teamBStats.Wins++;
              teamBStats.Points += 3;
              teamAStats.Losses++;
          }
          else
          {
              teamAStats.Draws++;
              teamBStats.Draws++;
              teamAStats.Points++;
              teamBStats.Points++;
          }
      }

      public void SortAndDisplayTable(SortCriteria sortCriteria)
      {
          var sortedTeams = _teams.Values.ToList();
          switch (sortCriteria)
          {
            case SortCriteria.Points:
            sortedTeams = _teams.Values
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.Wins)
                .ThenBy(t => t.Name)
                .ToList();
            break;

            case SortCriteria.GoalDifference:
            sortedTeams = _teams.Values
                .OrderByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.Points)
                .ThenByDescending(t => t.Wins)
                .ThenBy(t => t.Name)
                .ToList();
            break;

            case SortCriteria.NumberOfWins:
            sortedTeams = _teams.Values
                .OrderByDescending(t => t.Wins)
                .ThenByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenBy(t => t.Name)
                .ToList();
            break;

            case SortCriteria.Name:
            sortedTeams = _teams.Values
                .OrderBy(t => t.Name)
                .ThenByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.Wins)
                .ToList();
            break;

            default:
            sortedTeams = _teams.Values
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.Wins)
                .ThenBy(t => t.Name)
                .ToList();
            break;
          }
          DisplayTable(sortedTeams);
      }

      private void DisplayTable(List<TeamStats> sortedTeams)
      {
          Console.WriteLine("{0,-5} {1,-20} {2,7} {3,7} {4,7} {5,7} {6,7} {7,7} {8,7}", "Rank", "Name", "Points", "Wins", "Losses", "Draws", "GF", "GA", "GD");

          for (int i = 0; i < sortedTeams.Count; i++)
          {
            var team = sortedTeams[i];
            Console.WriteLine("{0,-5} {1,-20} {2,7} {3,7} {4,7} {5,7} {6,7} {7,7} {8,7}",
                i + 1,
                team.Name,
                team.Points,
                team.Wins,
                team.Losses,
                team.Draws,
                team.GoalsScored,
                team.GoalsConceded,
                team.GoalDifference);
          }
      }
  }
}
