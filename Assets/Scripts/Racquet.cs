using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racquet : MonoBehaviour
{
    public HealthScore healthScore;
    public Animator anim;

    [SerializeField] private float hitSpeed = 3f;
    private bool strike = false;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        anim.Play("Hit", 0, 0f);
        strike = true; // Set strike to true when the racquet is swung
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (strike == true && collision.gameObject.CompareTag("Interactable"))
        {
            if (healthScore != null)
            {
                healthScore.Scoring();
            }
            Debug.Log("Hit");

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                // Calculate the hit direction based on the rotation of the racquet
                Vector3 hitDirection = transform.rotation * Vector3.forward;

                // Apply a force to the object in the opposite direction of the hit direction
                rb.AddForce(-hitDirection * hitSpeed, ForceMode.Impulse);
            }
        }
        strike = false;
    }
}