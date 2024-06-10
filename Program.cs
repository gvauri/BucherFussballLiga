using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucherFussballLiga
{
  internal class Program
  {
    // args[0] - the filename in the format of "dayxx" (optional)
    // args[1] - sorting criteria      
    static void Main(string[] args)
    {
      string fileName = args.Length > 0 ? args[0] + ".txt" : null;
      string sortCriteria = args.Length > 1 ? args[1] : "pointsDesc";

      var leagueTable = new LeagueTable();
      leagueTable.ProcessFiles(fileName);
      leagueTable.SortAndDisplayTable(sortCriteria);

    }
  }
}
