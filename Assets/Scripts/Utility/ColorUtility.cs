using UnityEngine;

public class ColorUtility
{
    public static Color GetColorFromAngle(float angle)
    {
        byte red = 0;
        byte green = 0;
        byte blue = 0;

        RedToGreen(angle, ref red, ref green);
        GreenToBlue(angle, ref green, ref blue);
        BlueToRed(angle, ref red, ref blue);

        return new Color32(red, green, blue, 255);
    }
    private static void RedToGreen(float angle, ref byte red, ref byte green)
    {
        if (angle >= 0 && angle < 60)
        {
            red = 255;
            green = (byte)Mathf.Lerp(0, 255, Mathf.InverseLerp(0, 60, angle));
        }
        if (angle >= 60 && angle < 120)
        {
            red = (byte)Mathf.Lerp(255, 0, Mathf.InverseLerp(60, 120, angle));
            green = 255;
        }
    }
    private static void GreenToBlue(float angle, ref byte green, ref byte blue)
    {
        if (angle >= 120 && angle < 180)
        {
            green = 255;
            blue = (byte)Mathf.Lerp(0, 255, Mathf.InverseLerp(120, 180, angle));
        }
        if (angle >= 180 && angle < 240)
        {
            green = (byte)Mathf.Lerp(255, 0, Mathf.InverseLerp(180, 240, angle));
            blue = 255;
        }
    }
    private static void BlueToRed(float angle, ref byte red, ref byte blue)
    {
        if (angle >= 240 && angle < 300)
        {
            blue = 255;
            red = (byte)Mathf.Lerp(0, 255, Mathf.InverseLerp(240, 300, angle));
        }
        if (angle >= 300 && angle < 360)
        {
            blue = (byte)Mathf.Lerp(255, 0, Mathf.InverseLerp(300, 360, angle));
            red = 255;
        }
    }
}
