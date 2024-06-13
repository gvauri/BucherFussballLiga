using System.Text.RegularExpressions;
namespace BucherFussballLiga
{
  public class LeagueTable
  {
      private Dictionary<string, TeamStats> _teams = new Dictionary<string, TeamStats>();

      public void ReadFile(string file)
      {
          var lines = File.ReadAllLines(file);
          foreach (var line in lines)
          {
              var match = ParseLine(line);
              //UpdateTeamStats(match);
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
              case SortCriteria.Name:
                  sortedTeams = sortedTeams.OrderBy(team => team.Name).ToList();
                  break;
              case SortCriteria.Points:
                  sortedTeams = sortedTeams.OrderByDescending(t => t.Points).ToList();
                  break;
              case SortCriteria.GoalDifference:
                  sortedTeams = sortedTeams.OrderByDescending(t => t.GoalDifference).ToList();
                  break;
              case SortCriteria.NumberOfWins:
                sortedTeams = sortedTeams.OrderByDescending(t => t.Wins).ToList();
                break;
              default:
                  sortedTeams = sortedTeams.OrderByDescending(t => t.Points).ToList();
                  break;
           }

          DisplayTable(sortedTeams);
      }

      private void DisplayTable(List<TeamStats> sortedTeams)
      {
          Console.WriteLine("{0, -5} {1, -20} {2, -7} {3, -15} {4, -20} {5, -15} {6, -10}", "Rank", "Name", "Points", "W/L/D", "Goals Scored/Conceded", "Goal Difference", "Description");
          for (int i = 0; i < sortedTeams.Count; i++)
          {
              var team = sortedTeams[i];
              Console.WriteLine("{0, -5} {1, -20} {2, -7} {3, -15} {4, -20} {5, -15} {6, -10}", i + 1, team.Name, team.Points, $"{team.Wins}/{team.Losses}/{team.Draws}", $"{team.GoalsScored}/{team.GoalsConceded}", team.GoalDifference, "");
          }
      }
  }
}
