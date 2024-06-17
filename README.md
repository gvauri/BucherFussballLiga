# Overview

BucherFussballLiga is a console application for managing and displaying a football league table. The program reads match data from text files, processes the data to update team statistics, and sorts the league table based on various criteria.

# Features

- Reads match results from a text file.
- Updates team statistics including points, wins, losses, draws, goals scored, and goals conceded.
- Sorts and displays the league table based on specified criteria:
    - Name
    - Points
    - Goal Difference
    - Number of Wins

# Input File Format

The match data should be provided in a text file with the following format:

```
TeamA ScoreA:ScoreB TeamB
``` 

# Usage
## Command Line Parameters

1. Folder:<br>
    The first parameter is the name of the folder containing match result files. The files should be named in the format dayXX.txt where XX are digits.
2. Last Matchday (optional):<br>
    The second parameter specifies the last matchday to include. If not provided, all matchdays are included.
3. Sorting Criteria (optional):<br>
    The third parameter specifies the sorting criteria for the league table. If not provided, the default sorting criteria is by points.

## Sorting Criteria Options

-  Sort by Points
-  Sort by Goal Difference
-  Sort by Number of Wins
-  Sort by Name

# Example

To run the program with match data from files in a foldr and sort by points:<br>
```
dotnet run <folder>
```

To run the program with match data from files in the "matches" folder, include matches up to the second matchday, and sort by goal difference:<br>
```
dotnet run <folder> 2 GoalDifference
```


# Output

The program displays the league table in the console with the following columns:

- Rank
- Name
- Points
- Wins
- Losses
- Draws
- Goals For (GF)
- Goals Against (GA)
- Goal Difference (GD)


# Running the Application

1. Ensure you have .NET 8 installed on your system. 

2. Clone the repository:
```
git clone git@github.com:gvauri/BucherFussballLiga.git
```

3. Navigate to the project directory:
```
cd BucherFussballLiga
```
4. Place your match data files in the project directory under in a folder.

5. Run the application using dotnet run with the appropriate parameters:
```
dotnet run <folder> [<lastMatchday>] [<sortingCriteria>]
```

# Running Unit Tests
## Windows

Unit tests for this project are implemented using NUnit and Visual Studio on Windows.<br>

1. Open the Solution in Visual Studio<br>
    - Launch Visual Studio.<br>
    - Navigate to File > Open > Project/Solution.<br>
    - Browse to and select the FussballLiga.nUnitTest.csproj file.<br>

2. Run Unit Tests<br>
    - In Visual Studio's Test Explorer, you can see all the tests.<br>
    - Click on Run All to execute all tests.<br>

## Linux

For Linux-based systems, you can run the tests using the dotnet test command in the terminal.

1. Navigate to Test Project
```sh
cd FussballLiga.nUnitTest
```

2. Run Tests

Execute the following command to run the tests:
```sh
dotnet test
```
This command will discover and run all the unit tests in the project.

# Dependencies

.NET 8.0 SDK
