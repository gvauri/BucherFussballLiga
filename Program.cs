namespace BucherFussballLiga
{
      public enum SortCriteria
    { 
      Points, 
      GoalDifference, 
      NumberOfWins, 
      Name,
      NULL
    }
  internal class Program
  {
    // Main method parameters <folder> [<lastMatchday>] [<sortingCriteria>]
    static void Main(string[] args)
    {
      if (args.Length < 1 || args.Length > 3)
      {
        Console.WriteLine("Usage: BucherFussballLiga <folder> [<lastMatchday>] [<sortingCriteria>]");
        Environment.Exit(0);
      }

      string folder = args[0];
      int lastMatchday = args.Length >= 2 && int.TryParse(args[1], out int day) ? day : int.MaxValue;
      SortCriteria sortCriteria = args.Length == 3 && Enum.TryParse(args[2], out SortCriteria criteria) ? criteria : SortCriteria.Points;

      var leagueTable = new LeagueTable();
      leagueTable.ReadFile(folder, lastMatchday);
      leagueTable.SortAndDisplayTable(sortCriteria);
    }
  }
}
