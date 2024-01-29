using System.Text.RegularExpressions;

const byte RED_CUBES = 12;
const byte GREEN_CUBES = 13;
const byte BLUE_CUBES = 14;
var lines = File.ReadAllLines("input.txt");
byte index = 0;
ushort sumOfIds = 0;
ushort power = 0;
int powerSum = 0;
foreach(var line in lines)
{
    index++;
    var cubes = line.Split(':')[1].Trim();
    var sets = cubes.Split(';').Select(set => set.Trim());
    var impossible = false;

    foreach(var set in sets)
    {
        byte red = GetRedCubes(set);
        byte green = GetGreenCubes(set);
        byte blue = GetBlueCubes(set);
        if(red > RED_CUBES || green > GREEN_CUBES || blue > BLUE_CUBES)
        {
            impossible = true;
            break;
        }
    }
    if(!impossible)
    {
        sumOfIds += index;
    }

    byte maxRed = 0;
    byte maxGreen = 0;
    byte maxBlue = 0;
    foreach(var set in sets)
    {
        byte red = GetRedCubes(set);
        byte green = GetGreenCubes(set);
        byte blue = GetBlueCubes(set);
        maxRed  = maxRed < red ? red : maxRed;
        maxGreen  = maxGreen < green ? green : maxGreen;
        maxBlue  = maxBlue < blue ? blue : maxBlue;
    }
    power = (ushort)(maxRed * maxGreen * maxBlue);
    powerSum += power;
}

Console.WriteLine($"The sum of the IDs of those games is : {sumOfIds}");
Console.WriteLine($"The sum of power for the minimum set of cubes is : {powerSum}");

static byte GetRedCubes(string input)
{
    var redValue = input.Split(',').Where(val => val.Contains("red")).FirstOrDefault();
    
    return GetNumber(redValue ?? string.Empty);
}

static byte GetGreenCubes(string input)
{
    var greenValue = input.Split(',').Where(val => val.Contains("green")).FirstOrDefault();
    
    return GetNumber(greenValue ?? string.Empty);
}

static byte GetBlueCubes(string input)
{
    var blueValue = input.Split(',').Where(val => val.Contains("blue")).FirstOrDefault();
    
    return GetNumber(blueValue ?? string.Empty);
}

static byte GetNumber(string input)
{
    var match = Regex.Match(input, @"\d+");
    if (match.Success)
    {
        return byte.Parse(match.Value);
    }
    return default;
}