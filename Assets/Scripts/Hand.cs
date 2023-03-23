using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Animator anim;
    public bool isThrown = false;

    [SerializeField] private float throwForce = 10f;

    public Transform holdPlacement;
    public GameObject heldObject;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        anim.Play("Throw", 0, 0f);
    }

    public void ThrowObject()
    {
        Rigidbody heldRigidbody = heldObject.GetComponent<Rigidbody>();
        Vector3 direction = Camera.main.transform.forward;
        heldRigidbody.velocity = direction * throwForce;
        heldRigidbody.useGravity = true;
        heldRigidbody.mass = 0.1f;
        heldObject.transform.parent = null;
        heldObject = null;
        isThrown = true;
    }


    public void PickUpObject(GameObject objectToPickUp)
    {
        objectToPickUp.transform.position = holdPlacement.position; // Move object to hold placement
        objectToPickUp.transform.rotation = holdPlacement.rotation;
        heldObject = objectToPickUp;
        heldObject.transform.parent = holdPlacement.transform;
        heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        heldObject.GetComponent<Rigidbody>().useGravity = false;
        isThrown = false;
    }
}
