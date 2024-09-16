using UnityEngine;

public class Ball : MonoBehaviour
{
    public Color ballColor;  // Topun rengi

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = ballColor;
        }

        AdjustScaleBasedOnColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ball otherBall = collision.gameObject.GetComponent<Ball>();

        if (otherBall != null)
        {
            // Eğer renkler aynıysa topları birleştir
            if (IsColorCloseTo(otherBall.ballColor, this.ballColor))
            {
                MergeBalls(this, otherBall);
            }
        }
    }

    void AdjustScaleBasedOnColor()
    {
        Vector3 newScale = Vector3.one;

        if (IsColorCloseTo(ballColor, Color.blue))
        {
            newScale = new Vector3(1f, 1f, 1f);
        }
        else if (IsColorCloseTo(ballColor, new Color(0, 1, 1))) // Mavi-Yeşil
        {
            newScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else if (IsColorCloseTo(ballColor, Color.green))
        {
            newScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
        else if (IsColorCloseTo(ballColor, new Color(0.9f, 0.8f, 0))) // Sarı Yeşil
        {
            newScale = new Vector3(1.6f, 1.6f, 1.6f);
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
        else if (IsColorCloseTo(ballColor, Color.gray))
        {
            newScale = new Vector3(2.8f, 2.8f, 2.8f);
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
        // Küçük ve büyük topu belirle
        Ball largerBall = ball1.transform.localScale.magnitude >= ball2.transform.localScale.magnitude ? ball1 : ball2;
        Ball smallerBall = ball1.transform.localScale.magnitude < ball2.transform.localScale.magnitude ? ball1 : ball2;

        // En büyük ölçeği belirle
        Vector3 largestScale = new Vector3(2.8f, 2.8f, 2.8f);  // Bu, gri topun ölçeği

        // Eğer her iki top da en büyük ölçeğe sahipse, her ikisini de yok et
        if (largerBall.transform.localScale == largestScale && smallerBall.transform.localScale == largestScale)
        {
            Destroy(largerBall.gameObject);
            Destroy(smallerBall.gameObject);
        }
        else
        {
            // Küçük topu yok et
            Destroy(smallerBall.gameObject);

            // Büyük topun ölçeğini bir üst seviyeye çıkar
            Vector3 newScale = largerBall.transform.localScale + Vector3.one * 0.2f;
            largerBall.transform.localScale = newScale;

            // Yeni rengi hesapla
            Color newColor = GetNextColor(largerBall.ballColor);
            largerBall.ballColor = newColor;

            // Renderer rengini güncelle
            Renderer renderer = largerBall.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }
        }
    }

    private Color GetNextColor(Color currentColor)
    {
        // Renk geçişleri
        if (IsColorCloseTo(currentColor, Color.blue))
        {
            return new Color(0, 1, 1); // Mavi-Yeşil
        }
        else if (IsColorCloseTo(currentColor, new Color(0, 1, 1))) // Mavi-Yeşil
        {
            return Color.green; // Yeşil
        }
        else if (IsColorCloseTo(currentColor, Color.green))
        {
            return new Color(0.9f, 0.8f, 0); // Sarı Yeşil
        }
        else if (IsColorCloseTo(currentColor, new Color(0.9f, 0.8f, 0))) // Sarı Yeşil
        {
            return Color.yellow; // Sarı
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
            return Color.red; // Kırmızı
        }
        else if (IsColorCloseTo(currentColor, Color.red))
        {
            return new Color(0.55f, 0, 0); // Koyu Kırmızı
        }
        else if (IsColorCloseTo(currentColor, new Color(0.55f, 0, 0))) // Koyu Kırmızı
        {
            return Color.gray; // Gri
        }

        // Varsayılan olarak aynı rengi döndür
        return currentColor;
    }
}
