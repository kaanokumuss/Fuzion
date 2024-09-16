using UnityEngine;

public class BallSpawnerMover : MonoBehaviour
{
    public float minX = -5f;
    public float maxX = 5f;

    public void MoveSpawner(Vector3 targetPosition)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        transform.position = newPosition;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        MoveSpawner(worldPosition);
    }
}