
using UnityEngine;

internal class ElementTypeExtensions
{
    internal static Color GetElementColor(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.IGNIS:
                return Color.red; // Red
            case ElementType.GLACIES:
                return Color.cyan; // Cyan
            case ElementType.NATURA:
                return new Color(19f/225f, 112 / 225f, 29/ 225f); // Dark Green
            case ElementType.VENTUS:
                return Color.green; // Green
            case ElementType.LAPIS:
                return new Color(140/225f, 106/225f, 22/225f); // Brown
            case ElementType.AQUA:
                return Color.blue; // Blue
            case ElementType.FULGUR:
                return Color.magenta; // Magenta
            case ElementType.CORPORALIS:
                return Color.white; // White
            case ElementType.ILLUSION:
                return Color.gray; // Gray
            case ElementType.LUX:
                return Color.yellow; // Yellow
            case ElementType.TENEBRIS:
                return Color.black; // Black
            case ElementType.VESANIA:
                return new Color(178 / 225f, 14 / 225f, 95 / 225f); // Pink magenta
             default:
                return Color.clear;
        }
    }
}