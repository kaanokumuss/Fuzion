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
        else if (IsColorCloseTo(ballColor, Color.yellow))
        {
            newScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else if (IsColorCloseTo(ballColor, Color.red))
        {
            newScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
        else if (IsColorCloseTo(ballColor, Color.green))
        {
            newScale = new Vector3(1.6f, 1.6f, 1.6f);
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

        // Eğer her iki top da yeşilse, her ikisini de yok et
        if (IsColorCloseTo(largerBall.ballColor, Color.green) && IsColorCloseTo(smallerBall.ballColor, Color.green))
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
        // Renk yükseltme kuralları
        if (IsColorCloseTo(currentColor, Color.blue))
        {
            return Color.yellow; // Mavi -> Sarı
        }
        else if (IsColorCloseTo(currentColor, Color.yellow))
        {
            return Color.red; // Sarı -> Kırmızı
        }
        else if (IsColorCloseTo(currentColor, Color.red))
        {
            return Color.green; // Kırmızı -> Yeşil
        }
        else if (IsColorCloseTo(currentColor, Color.green))
        {
            // Yeşil zaten en yüksek renk, isteğe bağlı olarak başka bir işlem yapılabilir
            return currentColor; // Değişmez
        }

        // Varsayılan olarak aynı rengi döndür
        return currentColor;
    }
}
