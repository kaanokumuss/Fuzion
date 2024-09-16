using UnityEngine;

public class BallColorManager
{
    public static Vector3 GetScaleBasedOnColor(Color color)
    {
        if (IsColorCloseTo(color, Color.blue))
        {
            return new Vector3(1f, 1f, 1f);
        }
        else if (IsColorCloseTo(color, Color.green))
        {
            return new Vector3(1.4f, 1.4f, 1.4f);
        }
        else if (IsColorCloseTo(color, Color.yellow))
        {
            return new Vector3(1.8f, 1.8f, 1.8f);
        }
        else if (IsColorCloseTo(color, new Color(1, 0.65f, 0))) // Turuncu
        {
            return new Vector3(2f, 2f, 2f);
        }
        else if (IsColorCloseTo(color, new Color(0.54f, 0.27f, 0))) // Kahverengi
        {
            return new Vector3(2.2f, 2.2f, 2.2f);
        }
        else if (IsColorCloseTo(color, Color.red))
        {
            return new Vector3(2.4f, 2.4f, 2.4f);
        }
        else if (IsColorCloseTo(color, new Color(0.55f, 0, 0))) // Koyu Kırmızı
        {
            return new Vector3(2.6f, 2.6f, 2.6f);
        }
        else if (IsColorCloseTo(color, Color.gray)) // Gri
        {
            return new Vector3(2.8f, 2.8f, 2.8f);
        }
        else if (IsColorCloseTo(color, new Color(0.3f, 0.3f, 0.3f))) // Koyu Gri
        {
            return new Vector3(3.0f, 3.0f, 3.0f);
        }
        else if (IsColorCloseTo(color, new Color(0.5f, 0, 0.5f))) // Mor
        {
            return new Vector3(3.2f, 3.2f, 3.2f);
        }
        else if (IsColorCloseTo(color, new Color(0.68f, 0.85f, 0.9f))) // Açık Mavi
        {
            return new Vector3(3.4f, 3.4f, 3.4f);
        }
        else if (IsColorCloseTo(color, new Color(1f, 0.84f, 0))) // Altın
        {
            return new Vector3(3.6f, 3.6f, 3.6f);
        }
        else if (IsColorCloseTo(color, new Color(1f, 0.75f, 0.8f))) // Pembe
        {
            return new Vector3(3.8f, 3.8f, 3.8f);
        }
        else if (IsColorCloseTo(color, new Color(0.56f, 0.93f, 0.56f))) // Açık Yeşil
        {
            return new Vector3(4.0f, 4.0f, 4.0f);
        }

        return Vector3.one;
    }

    public static Color GetNextColor(Color currentColor)
    {
        if (IsColorCloseTo(currentColor, Color.blue))
        {
            return Color.green;
        }
        else if (IsColorCloseTo(currentColor, Color.green))
        {
            return Color.yellow;
        }
        else if (IsColorCloseTo(currentColor, Color.yellow))
        {
            return new Color(1, 0.65f, 0); // Turuncu
        }
        else if (IsColorCloseTo(currentColor, new Color(1, 0.65f, 0))) // Turuncu
        {
            return new Color(0.54f, 0.27f, 0); // Kahverengi
        }
        else if (IsColorCloseTo(currentColor, new Color(0.54f, 0.27f, 0))) // Kahverengi
        {
            return Color.red;
        }
        else if (IsColorCloseTo(currentColor, Color.red))
        {
            return new Color(0.55f, 0, 0); // Koyu Kırmızı
        }
        else if (IsColorCloseTo(currentColor, new Color(0.55f, 0, 0))) // Koyu Kırmızı
        {
            return Color.gray;
        }
        else if (IsColorCloseTo(currentColor, Color.gray))
        {
            return new Color(0.3f, 0.3f, 0.3f); // Koyu Gri
        }
        else if (IsColorCloseTo(currentColor, new Color(0.3f, 0.3f, 0.3f))) // Koyu Gri
        {
            return new Color(0.5f, 0, 0.5f); // Mor
        }
        else if (IsColorCloseTo(currentColor, new Color(0.5f, 0, 0.5f))) // Mor
        {
            return new Color(0.68f, 0.85f, 0.9f); // Açık Mavi
        }
        else if (IsColorCloseTo(currentColor, new Color(0.68f, 0.85f, 0.9f))) // Açık Mavi
        {
            return new Color(1f, 0.84f, 0); // Altın
        }
        else if (IsColorCloseTo(currentColor, new Color(1f, 0.84f, 0))) // Altın
        {
            return new Color(1f, 0.75f, 0.8f); // Pembe
        }
        else if (IsColorCloseTo(currentColor, new Color(1f, 0.75f, 0.8f))) // Pembe
        {
            return new Color(0.56f, 0.93f, 0.56f); // Açık Yeşil
        }

        return currentColor;
    }

    public static bool IsColorCloseTo(Color color1, Color color2, float tolerance = 0.1f)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance;
    }
}
