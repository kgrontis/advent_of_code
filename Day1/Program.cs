using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt");
int sum = 0;
foreach(var line in lines)
{
    var firstDigit = GetFirstDigit(line).ToString();
    var lastDigit = GetLastDigit(line).ToString();
    var num = int.Parse(firstDigit + lastDigit);
    sum += num;
}

Console.WriteLine($"The sum of all calibration values is: {sum}");

static char GetFirstDigit(string input)
{
    Match match = Regex.Match(input, @"\d");
    if (match.Success)
    {
        return match.Value[0];
    }
    return default;
}

static char GetLastDigit(string input)
{
    MatchCollection matches = Regex.Matches(input, @"\d");
    if (matches.Count > 0)
    {
        return matches.Last().Value[0];
    }
    return default;
}