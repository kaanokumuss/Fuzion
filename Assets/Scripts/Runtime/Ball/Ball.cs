using UnityEngine;

public class Ball : MonoBehaviour
{
    public Color ballColor;
    private bool canMerge = false;
    public float mergeCooldown = 1f;
    public float destroyDelay = 2f;  // Topu yok etmeden önce gecikme süresi

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = ballColor;
        }

        AdjustScaleBasedOnColor();

        Invoke(nameof(EnableMerging), mergeCooldown);
    }

    private void EnableMerging()
    {
        canMerge = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canMerge) return;

        Ball otherBall = collision.gameObject.GetComponent<Ball>();

        if (otherBall != null && IsColorCloseTo(otherBall.ballColor, this.ballColor))
        {
            MergeBalls(this, otherBall);
        }
    }

    void AdjustScaleBasedOnColor()
    {
        Vector3 newScale = Vector3.one;

        if (IsColorCloseTo(ballColor, Color.blue))
        {
            newScale = new Vector3(1f, 1f, 1f);
        }
        else if (IsColorCloseTo(ballColor, Color.green))
        {
            newScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
        else if (IsColorCloseTo(ballColor, Color.yellow))
        {
            newScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        else if (IsColorCloseTo(ballColor, new Color(1, 0.65f, 0))) // Turuncu
        {
            newScale = new Vector3(2f, 2f, 2f);
        }
        else if (IsColorCloseTo(ballColor, new Color(0.54f, 0.27f, 0))) // Kahverengi
        {
            newScale = new Vector3(2.2f, 2.2f, 2.2f);
        }
        else if (IsColorCloseTo(ballColor, Color.red))
        {
            newScale = new Vector3(2.4f, 2.4f, 2.4f);
        }
        else if (IsColorCloseTo(ballColor, new Color(0.55f, 0, 0))) // Koyu Kırmızı
        {
            newScale = new Vector3(2.6f, 2.6f, 2.6f);
        }
        else if (IsColorCloseTo(ballColor, Color.gray)) // Gri
        {
            newScale = new Vector3(2.8f, 2.8f, 2.8f);
        }
        else if (IsColorCloseTo(ballColor, new Color(0.3f, 0.3f, 0.3f))) // Koyu Gri (En büyük top)
        {
            newScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
        // Yeni renkler ve ölçekler
        else if (IsColorCloseTo(ballColor, new Color(0.3f, 0.3f, 0.3f))) // Koyu Gri
        {
            newScale = new Vector3(3.2f, 3.2f, 3.2f); // Mor (#800080)
        }
        else if (IsColorCloseTo(ballColor, new Color(0.5f, 0, 0.5f))) // Mor
        {
            newScale = new Vector3(3.4f, 3.4f, 3.4f); // Açık Mavi (#ADD8E6)
        }
        else if (IsColorCloseTo(ballColor, new Color(0.68f, 0.85f, 0.9f))) // Açık Mavi
        {
            newScale = new Vector3(3.6f, 3.6f, 3.6f); // Altın (#FFD700)
        }
        else if (IsColorCloseTo(ballColor, new Color(1f, 0.84f, 0))) // Altın
        {
            newScale = new Vector3(3.8f, 3.8f, 3.8f); // Pembe (#FFC0CB)
        }
        else if (IsColorCloseTo(ballColor, new Color(1f, 0.75f, 0.8f))) // Pembe
        {
            newScale = new Vector3(4.0f, 4.0f, 4.0f); // Açık Yeşil (#90EE90)
        }

        transform.localScale = newScale;
    }

    private bool IsColorCloseTo(Color color1, Color color2, float tolerance = 0.1f)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance;
    }

    private void MergeBalls(Ball ball1, Ball ball2)
    {
        Ball largerBall = ball1.transform.localScale.magnitude >= ball2.transform.localScale.magnitude ? ball1 : ball2;
        Ball smallerBall = ball1.transform.localScale.magnitude < ball2.transform.localScale.magnitude ? ball1 : ball2;

        Vector3 maxScale = new Vector3(4.0f, 4.0f, 4.0f); // En büyük ölçek

        if (largerBall.transform.localScale == maxScale && smallerBall.transform.localScale == maxScale)
        {
            Destroy(largerBall.gameObject, destroyDelay);
            Destroy(smallerBall.gameObject, destroyDelay);
        }
        else
        {
            Destroy(smallerBall.gameObject);

            Vector3 newScale = largerBall.transform.localScale + Vector3.one * 0.2f;
            largerBall.transform.localScale = newScale;

            Color newColor = GetNextColor(largerBall.ballColor);
            largerBall.ballColor = newColor;

            Renderer renderer = largerBall.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }

            if (newScale == maxScale)
            {
                Destroy(largerBall.gameObject, destroyDelay);
            }
        }
    }

    private Color GetNextColor(Color currentColor)
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
            return new Color(0.3f, 0.3f, 0.3f); // Koyu Gri (En büyük renk)
        }
        // Yeni renkler
        else if (IsColorCloseTo(currentColor, new Color(0.3f, 0.3f, 0.3f))) // Koyu Gri
        {
            return new Color(0.5f, 0, 0.5f); // Mor (#800080)
        }
        else if (IsColorCloseTo(currentColor, new Color(0.5f, 0, 0.5f))) // Mor
        {
            return new Color(0.68f, 0.85f, 0.9f); // Açık Mavi (#ADD8E6)
        }
        else if (IsColorCloseTo(currentColor, new Color(0.68f, 0.85f, 0.9f))) // Açık Mavi
        {
            return new Color(1f, 0.84f, 0); // Altın (#FFD700)
        }
        else if (IsColorCloseTo(currentColor, new Color(1f, 0.84f, 0))) // Altın
        {
            return new Color(1f, 0.75f, 0.8f); // Pembe (#FFC0CB)
        }
        else if (IsColorCloseTo(currentColor, new Color(1f, 0.75f, 0.8f))) // Pembe
        {
            return new Color(0.56f, 0.93f, 0.56f); // Açık Yeşil (#90EE90)
        }

        return currentColor;
    }
}
