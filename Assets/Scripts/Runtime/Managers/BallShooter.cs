using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallShooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public float fireForce = 1f;
    public Color[] colors;  

    private void OnEnable()
    {
        GameEvents.FireBall += FireBall;
    }

    private void OnDisable()
    {
        GameEvents.FireBall -= FireBall;
    }

    public void FireBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Ball ballScript = ball.GetComponent<Ball>();

        if (ballScript != null)
        {
            Color randomColor = colors[Random.Range(0, colors.Length)];
            ballScript.ballColor = randomColor;
        }

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * fireForce);
    }
}