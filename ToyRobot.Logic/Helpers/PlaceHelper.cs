using System.Text.RegularExpressions;

namespace ToyRobot.Logic.Helpers;

public static class PlaceHelper
{
    public static bool IsPlaceCommand(string userInput1)
    {
        return Regex.IsMatch(userInput1, @"^PLACE\b\s\d,{1}\d,{1}(?:NORTH|EAST|SOUTH|WEST)$");
    }
}