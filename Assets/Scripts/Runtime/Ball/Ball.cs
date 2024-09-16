using UnityEngine;

public class Ball : MonoBehaviour
{
    public Color ballColor;
    private bool canMerge = false;
    public float mergeCooldown = 1f;
    public float destroyDelay = 2f;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = ballColor;
        }

        transform.localScale = BallColorManager.GetScaleBasedOnColor(ballColor);

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

        if (otherBall != null && BallColorManager.IsColorCloseTo(otherBall.ballColor, this.ballColor))
        {
            BallMerger.MergeBalls(this, otherBall, destroyDelay);
        }
    }
}