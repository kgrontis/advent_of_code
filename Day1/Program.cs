var digits = new Dictionary<string,int>
{
    {"one", 1},
    {"two", 2},
    {"three", 3},
    {"four", 4},
    {"five", 5},
    {"six", 6},
    {"seven", 7},
    {"eight", 8},
    {"nine", 9},
    {"1", 1},
    {"2", 2},
    {"3", 3},
    {"4", 4},
    {"5", 5},
    {"6", 6},
    {"7", 7},
    {"8", 8},
    {"9", 9},    
};

var lines = File.ReadAllLines("input.txt");

int sum = 0;
foreach(var line in lines)
{
    var firstIndex = line.Length;
    var lastIndex = -1;
    var firstValue = 0;
    var lastValue = 0;
    foreach(var digit in digits)
    {
        var index = line.IndexOf(digit.Key);
        if(index == -1)
        {
            continue;
        }
        if(index < firstIndex)
        {
            firstIndex = index;
            firstValue = digit.Value;
        }
        index = line.LastIndexOf(digit.Key);
        if(index > lastIndex)
        {
            lastIndex = index;
            lastValue = digit.Value;
        }
    }
    var num = firstValue * 10 + lastValue;
    sum += num;
}

Console.WriteLine($"The sum of all calibration values is: {sum}");
