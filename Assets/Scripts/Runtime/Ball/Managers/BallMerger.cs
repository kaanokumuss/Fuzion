using UnityEngine;

public class BallMerger
{
    public static void MergeBalls(Ball ball1, Ball ball2, float destroyDelay)
    {
        // Daha büyük ve daha küçük topu belirleyin
        Ball largerBall = ball1.transform.localScale.magnitude >= ball2.transform.localScale.magnitude ? ball1 : ball2;
        Ball smallerBall = ball1.transform.localScale.magnitude < ball2.transform.localScale.magnitude ? ball1 : ball2;

        // Maksimum ölçeği tanımlayın
        Vector3 maxScale = new Vector3(4.0f, 4.0f, 4.0f); 

        // Eğer her iki top da maksimum ölçeğe ulaştıysa, yok edin
        if (largerBall.transform.localScale == maxScale && smallerBall.transform.localScale == maxScale)
        {
            Object.Destroy(largerBall.gameObject, destroyDelay);
            Object.Destroy(smallerBall.gameObject, destroyDelay);
        }
        else
        {
            // Küçük topu yok edin
            Object.Destroy(smallerBall.gameObject);

            // Daha büyük topu büyütün
            Vector3 newScale = largerBall.transform.localScale + Vector3.one * 0.2f;
            largerBall.transform.localScale = newScale;

            // Yeni rengi hesaplayın ve uygulayın
            Color newColor = BallColorManager.GetNextColor(largerBall.ballColor);
            largerBall.ballColor = newColor;

            Renderer renderer = largerBall.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }

            // Eğer daha büyük top maksimum ölçeğe ulaştıysa, yok edin
            if (newScale == maxScale)
            {
                Object.Destroy(largerBall.gameObject, destroyDelay);
            }
        }
    }
}