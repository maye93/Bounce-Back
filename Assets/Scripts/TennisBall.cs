using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    [SerializeField] private float ballSpeed = 10f; // Speed of the ball
    [SerializeField] private float ballBounce = 0.8f; // Bounciness of the ball

    public bool groundTouch = false;
    private Rigidbody ballRigidbody;
    private Mesh originalMesh;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();

        ballRigidbody.freezeRotation = true;
        ballRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        ballRigidbody.velocity = Vector3.forward * ballSpeed; // Starts moving forward

        // Increase physics solver iterations and decrease time step
        Physics.defaultSolverIterations = 10;
        Time.fixedDeltaTime = 0.002f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get collision impulse
        float collisionImpulse = collision.impulse.magnitude;

        // Bounce off other objects with correct force
        Vector3 bounceDirection = Vector3.Reflect(ballRigidbody.velocity, collision.contacts[0].normal);
        ballRigidbody.velocity = bounceDirection.normalized * collisionImpulse * ballBounce / ballRigidbody.mass;

        if (collision.gameObject.CompareTag("Ground"))
        {
            groundTouch = true;
        }
    }

    public void AddSpeed(Vector3 force, Vector3 point)
    {
        // Add force at the specified point
        ballRigidbody.AddForceAtPosition(force, point);
    }
}