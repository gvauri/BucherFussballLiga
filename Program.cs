using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BucherFussballLiga
{
  internal class Program
  {
    public enum SortCriteria
    { 
      Points, 
      GoalDifference, 
      NumberOfWins, 
      Name,
      NULL
    }
    // Main method with possible parameters
    // args[0] - the filename in the format of "dayXX" 
    // args[1] - sorting criteria (optional, default is points)   
    static void Main(string[] args)
    {
      //check/read parameters
      string fileName = args.Length > 0 ? args[0] + ".txt" : null;
      string pattern = @"^day\d{2}$";
      if(fileName == null || !Regex.IsMatch(args[0], pattern))
      {
        Environment.Exit(0);
      }

      SortCriteria sortCriteria = SortCriteria.NULL;
      if(args.Length == 2)
      {
        switch(args[1])
        {
          case "1":
            sortCriteria = SortCriteria.Points;
            break;
          case "2":
            sortCriteria = SortCriteria.GoalDifference;
            break;
          case "3":
            sortCriteria = SortCriteria.NumberOfWins;
            break;
          case "4":
            sortCriteria =SortCriteria.Name;
            break;
          default:
            throw new Exception("Invalid sorting criteria!");
        }
      }

      var leagueTable = new LeagueTable();
      leagueTable.ProcessFiles(fileName);
      leagueTable.SortAndDisplayTable(sortCriteria);
    }
  }
}
