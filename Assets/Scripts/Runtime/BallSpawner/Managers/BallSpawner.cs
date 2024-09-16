using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;       // The ball prefab (sphere)
    public float fireForce = 1f;        // Force to apply when shooting
    public Color[] colors;              // Array of possible colors for the balls

    private GameObject previewBall;     // The next ball preview (sphere)
    private Color nextColor;            // Store the color of the next ball

    private void Start()
    {
        // Generate the first preview sphere at the start of the game
        GenerateNextBall();
    }

    private void OnEnable()
    {
        GameEvents.FireBall += FireBall;
    }

    private void OnDisable()
    {
        GameEvents.FireBall -= FireBall;
    }

    // Method to generate the next ball (sphere) for preview
    private void GenerateNextBall()
    {
        // Destroy the old preview ball if it exists
        if (previewBall != null)
        {
            Destroy(previewBall);
        }

        Vector3 previewPosition = transform.position + Vector3.up * 1f;
        previewBall = Instantiate(ballPrefab, previewPosition, Quaternion.identity);

        previewBall.GetComponent<Rigidbody>().isKinematic = true; 

        // Disable the collider of the preview ball
        Collider previewCollider = previewBall.GetComponent<Collider>();
        if (previewCollider != null)
        {
            previewCollider.enabled = false;
        }

        nextColor = colors[Random.Range(0, colors.Length)];
        Ball ballScript = previewBall.GetComponent<Ball>();

        if (ballScript != null)
        {
            ballScript.ballColor = nextColor;

            Renderer renderer = previewBall.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = nextColor;
            }
        }
    }

    public void FireBall()
    {
        // Instantiate the ball slightly above the current position to avoid collision with the spawner
        Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
        GameObject firedBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        Ball ballScript = firedBall.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.ballColor = nextColor;
        }

        // Get the Rigidbody component and ensure it's not set to kinematic
        Rigidbody rb = firedBall.GetComponent<Rigidbody>();
        rb.isKinematic = false;  // Ensure that gravity is enabled for this ball
    
        // Apply an immediate downward force to make the ball fall down
        rb.AddForce(Vector3.down * fireForce, ForceMode.Impulse);  // Impulse adds force instantly

        // Reactivate the collider of the preview ball when the current ball is fired
        Collider previewCollider = previewBall.GetComponent<Collider>();
        if (previewCollider != null)
        {
            previewCollider.enabled = true;
        }

        GenerateNextBall(); // Generate the next preview ball
    }

    public void UpdatePreviewPosition(Vector3 newPosition)
    {
        if (previewBall != null)
        {
            previewBall.transform.position = newPosition;
        }
    }
}
