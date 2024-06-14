# Overview

BucherFussballLiga is a console application for managing and displaying a football league table. The program reads match data from a text file, processes the data to update team statistics, and sorts the league table based on various criteria.

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

1. Filename:<br>
    The first parameter is the name of the file containing match results (without the ".txt" extension). The file should be named in the format dayXX where XX are digits.
2. Sorting Criteria:<br>
    The second parameter (optional) specifies the sorting criteria for the league table. If not provided, the default sorting criteria is by points.

## Sorting Criteria Options

-  Sort by Points
-  Sort by Goal Difference
-  Sort by Number of Wins
-  Sort by Name

## Example

To run the program with match data from day01.txt and sort by points:

```
dotnet run day01 1
```

To run the program with match data from day02.txt and sort by goal difference:

```
dotnet run day02 2
```
# Output

The program displays the league table in the console with the following columns:

- Rank
- Name
- Points
- Wins/Losses/Draws
- Goals Scored/Conceded

The sorted column is indicated by a dashed line under the corresponding header.

# Running the Application

1. Ensure you have .NET 8 installed on your system.
2. Clone the repository.
3. Navigate to the project directory.
4. Place your match data files in the project directory.
5. Run the application using dotnet run with the appropriate parameters.
