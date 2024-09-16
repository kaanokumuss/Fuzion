using UnityEngine;

public class BallSpawnerMover : MonoBehaviour
{
    public BallShooter ballShooter; // Reference to the BallShooter script
    public float minX = -5f;
    public float maxX = 5f;

    public void MoveSpawner(Vector3 targetPosition)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        transform.position = newPosition;

        // Update the preview ball's position along with the spawner
        ballShooter.UpdatePreviewPosition(newPosition);
    }

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        // Move the spawner based on the mouse position
        MoveSpawner(worldPosition);
    }
}