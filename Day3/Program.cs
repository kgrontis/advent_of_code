//string[] lines = ["467..114..", "...*......", "..35..633.", "......#...", "617*......", ".....+.58.", "..592.....", "......755.", "...$.*....", ".664.598.."];
var lines = File.ReadAllLines("input.txt");

var width = lines[0].Length;
var height = lines.Length;
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
for (int i = 0; i < width; i++)
{
    for (int j = 0; j < height; j++)
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