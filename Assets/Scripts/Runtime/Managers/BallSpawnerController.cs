using UnityEngine;

public class BallSpawnerController : MonoBehaviour
{
    public TouchManager touchManager;
    public BallSpawnerMover spawnerMover;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;  
    }

    void Update()
    {
        Vector3 touchPosition = touchManager.GetTouchPosition(cam);
        spawnerMover.MoveSpawner(touchPosition);

        if (touchManager.IsTouching())
        {
            GameEvents.FireBall?.Invoke();
        }
    }
}