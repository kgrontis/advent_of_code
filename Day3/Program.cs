using System.Text;

//string[] lines = ["467..114..", "...*......", "..35..633.", "......#...", "617*......", ".....+.58.", "..592.....", "......755.", "...$.*....", ".664.598.."];
var lines = File.ReadAllLines("input.txt");

var width = lines[1].Length;
var height = lines[0].Length;
char[,] chars = new char[width, height];

for (int i = 0; i < width; i++)
{
    for (int j = 0; j < height; j++)
    {
        chars[i, j] = lines[i][j];
    }
}

var currentNum = 0;
int sum = 0;
bool hasNeighbor = false;
//Part 1
for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        var character = chars[i, j];
        if (char.IsDigit(character))
        {
            var value = character - '0';
            currentNum = currentNum * 10 + value;
            if (!hasNeighbor)
            {
                hasNeighbor = HasNeighbor(chars, i, j);
            }
        }
        else
        {
            if (hasNeighbor)
            {
                sum += currentNum;
            }
            hasNeighbor = false;
            currentNum = 0;
        }
    }
}

//Part 2
sum = 0;
for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        var character = chars[i, j];
        if (character == '*')
        {
            var neighbors = FindStarsNeighbors(chars, i, j);
            if (neighbors.Count == 2)
            {
                var gearRatio = neighbors[0] * neighbors[1];
                sum += gearRatio;
            }
        }
    }
}

Console.WriteLine(sum);

static bool IsSpecialCharacter(char c)
{
    var isSpecial = !char.IsDigit(c) && !(c == '.');
    return isSpecial;
}


bool HasNeighbor(char[,] chars, int w, int h)
{
    var maxIndex = chars.GetLength(0) - 1;
    var hasNeighbor = false;
    if (!hasNeighbor && w > 0)
    {
        char up = chars[w - 1, h];
        hasNeighbor = IsSpecialCharacter(up);
    }
    if (!hasNeighbor && w > 0 && h < maxIndex)
    {
        char upRight = chars[w - 1, h + 1];
        hasNeighbor = IsSpecialCharacter(upRight);
    }
    if (!hasNeighbor && h > 0)
    {
        char left = chars[w, h - 1];
        hasNeighbor = IsSpecialCharacter(left);
    }
    if (!hasNeighbor && h < maxIndex)
    {
        char right = chars[w, h + 1];
        hasNeighbor = IsSpecialCharacter(right);
    }
    if (!hasNeighbor && w > 0 && h > 0)
    {
        char upLeft = chars[w - 1, h - 1];
        hasNeighbor = IsSpecialCharacter(upLeft);
    }
    if (!hasNeighbor && w < maxIndex && h > 0)
    {
        char downLeft = chars[w + 1, h - 1];
        hasNeighbor = IsSpecialCharacter(downLeft);
    }
    if (!hasNeighbor && w < maxIndex)
    {
        char down = chars[w + 1, h];
        hasNeighbor = IsSpecialCharacter(down);
    }
    if (!hasNeighbor && w < maxIndex && h < maxIndex)
    {
        char downRight = chars[w + 1, h + 1];
        hasNeighbor = IsSpecialCharacter(downRight);
    }
    return hasNeighbor;
}


List<int> FindStarsNeighbors(char[,] chars, int height, int width)
{
    var maxIndex = chars.GetLength(0) - 1;
    List<int> neighbors = [];

    var hasNeighbor = false;

    if (height > 0)
    {
        char up = chars[height - 1, width];
        hasNeighbor = char.IsDigit(up);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height - 1, maxIndex);
            var num = ExtractNumberAtIndex(s, width);
            neighbors.Add(num);
        }
    }
    if (height > 0 && width < maxIndex)
    {
        char upRight = chars[height - 1, width + 1];
        hasNeighbor = char.IsDigit(upRight);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height - 1, maxIndex);
            var num = ExtractNumberAtIndex(s, width + 1);
            neighbors.Add(num);
        }
    }
    if (width > 0)
    {
        char left = chars[height, width - 1];
        hasNeighbor = char.IsDigit(left);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height, maxIndex);
            var num = ExtractNumberAtIndex(s, width - 1);
            neighbors.Add(num);
        }
    }
    if (width < maxIndex)
    {
        char right = chars[height, width + 1];
        hasNeighbor = char.IsDigit(right);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height, maxIndex);
            var num = ExtractNumberAtIndex(s, width + 1);
            neighbors.Add(num);
        }
    }
    if (height > 0 && width > 0)
    {
        char upLeft = chars[height - 1, width - 1];
        hasNeighbor = char.IsDigit(upLeft);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height - 1, maxIndex);
            var num = ExtractNumberAtIndex(s, width - 1);
            neighbors.Add(num);
        }
    }
    if (height < maxIndex && width > 0)
    {
        char downLeft = chars[height + 1, width - 1];
        hasNeighbor = char.IsDigit(downLeft);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height + 1, maxIndex);
            var num = ExtractNumberAtIndex(s, width - 1);
            neighbors.Add(num);
        }
    }
    if (height < maxIndex)
    {
        char down = chars[height + 1, width];
        hasNeighbor = char.IsDigit(down);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height + 1, maxIndex);
            var num = ExtractNumberAtIndex(s, width);
            neighbors.Add(num);
        }
    }
    if (height < maxIndex && width < maxIndex)
    {
        char downRight = chars[height + 1, width + 1];
        hasNeighbor = char.IsDigit(downRight);
        if (hasNeighbor)
        {
            var s = GetRowValue(chars, height + 1, maxIndex);
            var num = ExtractNumberAtIndex(s, width + 1);

            neighbors.Add(num);

        }
    }
    return neighbors.Distinct().ToList();
}


string GetRowValue(char[,] chars, int index, int width)
{
    StringBuilder sb = new();
    for (int i = 0; i <= width; i++)
    {
        sb.Append(chars[index, i]);
    }
    return sb.ToString();
}

static int ExtractNumberAtIndex(string input, int index)
{
    if (index >= 0 && index < input.Length)
    {
        int startIndex = index;
        while (startIndex >= 0 && char.IsDigit(input[startIndex]))
        {
            startIndex--;
        }

        int endIndex = index + 1;
        while (endIndex < input.Length && char.IsDigit(input[endIndex]))
        {
            endIndex++;
        }

        if (startIndex < endIndex - 1)
        {
            var numAsString = input.Substring(startIndex + 1, endIndex - startIndex - 1);
            _ = int.TryParse(numAsString, out var value);
            return value;
        }
    }
    return default;
}