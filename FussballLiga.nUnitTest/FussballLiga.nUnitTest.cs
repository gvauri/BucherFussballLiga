using NUnit.Framework;
using BucherFussballLiga;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FussballLiga.nUnitTest
{
    [TestFixture]
    public class LeagueTableTests
    {
        private LeagueTable _leagueTable;

        [SetUp]
        public void Setup()
        {
            _leagueTable = new LeagueTable();
        }

        [Test]
        public void TestParseLine_ValidLine_ReturnsMatchObject()
        {
            var line = "TeamA 2:1 TeamB";
            var expectedMatch = new Match { TeamA = "TeamA", ScoreA = 2, ScoreB = 1, TeamB = "TeamB" };
            var actualMatch = _leagueTable.ParseLine(line);

            Assert.AreEqual(expectedMatch.TeamA, actualMatch.TeamA);
            Assert.AreEqual(expectedMatch.ScoreA, actualMatch.ScoreA);
            Assert.AreEqual(expectedMatch.ScoreB, actualMatch.ScoreB);
            Assert.AreEqual(expectedMatch.TeamB, actualMatch.TeamB);
        }

        [Test]
        public void TestUpdateTeamStats_UpdatesCorrectly()
        {
            var match = new Match { TeamA = "TeamA", ScoreA = 2, ScoreB = 1, TeamB = "TeamB" };
            _leagueTable.UpdateTeamStats(match);

            var teamAStats = GetTeamStats("TeamA");
            var teamBStats = GetTeamStats("TeamB");

            Assert.AreEqual(1, teamAStats.Wins);
            Assert.AreEqual(3, teamAStats.Points);
            Assert.AreEqual(0, teamAStats.Draws);
            Assert.AreEqual(0, teamAStats.Losses);
            Assert.AreEqual(2, teamAStats.GoalsScored);
            Assert.AreEqual(1, teamAStats.GoalsConceded);

            Assert.AreEqual(0, teamBStats.Wins);
            Assert.AreEqual(0, teamBStats.Points);
            Assert.AreEqual(0, teamBStats.Draws);
            Assert.AreEqual(1, teamBStats.Losses);
            Assert.AreEqual(1, teamBStats.GoalsScored);
            Assert.AreEqual(2, teamBStats.GoalsConceded);
        }

        [Test]
        public void TestSortAndDisplayTable_SortsByPointsCorrectly()
        {

            _leagueTable.UpdateTeamStats(new Match { TeamA = "TeamA", ScoreA = 2, ScoreB = 1, TeamB = "TeamB" });
            _leagueTable.UpdateTeamStats(new Match { TeamA = "TeamC", ScoreA = 3, ScoreB = 1, TeamB = "TeamD" });

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw); 

                _leagueTable.SortAndDisplayTable(SortCriteria.Points);
                
                string consoleOutput = sw.ToString();

                Assert.IsTrue(consoleOutput.Contains("TeamA"));
                Assert.IsTrue(consoleOutput.Contains("TeamC"));
                Assert.IsTrue(consoleOutput.Contains("TeamD"));
                Assert.IsTrue(consoleOutput.Contains("TeamB"));
            }
        }

        [Test]
        public void TestReadFile_ReadsFilesCorrectly()
        {
            string testFolderPath = "TestFiles";
            if (!Directory.Exists(testFolderPath))
                Directory.CreateDirectory(testFolderPath);

            File.WriteAllText(Path.Combine(testFolderPath, "day1.txt"), "TeamA 2:1 TeamB\nTeamC 3:1 TeamD");
            File.WriteAllText(Path.Combine(testFolderPath, "day2.txt"), "TeamA 1:1 TeamC\nTeamB 0:2 TeamD");

            _leagueTable.ReadFile(testFolderPath, 2);

            var teamAStats = GetTeamStats("TeamA");
            var teamBStats = GetTeamStats("TeamB");
            var teamCStats = GetTeamStats("TeamC");
            var teamDStats = GetTeamStats("TeamD");

            Assert.AreEqual(1, teamAStats.Wins);
            Assert.AreEqual(1, teamAStats.Draws);
            Assert.AreEqual(0, teamAStats.Losses);

            Assert.AreEqual(0, teamBStats.Wins);
            Assert.AreEqual(0, teamBStats.Draws);
            Assert.AreEqual(2, teamBStats.Losses);

            Assert.AreEqual(1, teamCStats.Wins);
            Assert.AreEqual(1, teamCStats.Draws);
            Assert.AreEqual(0, teamCStats.Losses);

            Assert.AreEqual(1, teamDStats.Wins);
            Assert.AreEqual(0, teamDStats.Draws);
            Assert.AreEqual(1, teamDStats.Losses);
        }

        private TeamStats GetTeamStats(string teamName)
        {
            var field = typeof(LeagueTable).GetField("_teams", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var teams = (Dictionary<string, TeamStats>)field.GetValue(_leagueTable);
            return teams[teamName];
        }


    }
}
