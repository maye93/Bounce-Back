using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image crosshair = null;

    private bool isCrosshairActive;
    private bool doOnce;

    public TennisBall tennisBall;
    public Hand hand;
    public Racquet racquet;

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                TennisBall ball = hit.collider.gameObject.GetComponent<TennisBall>();
                if (ball != null && !doOnce)
                {
                    tennisBall = ball;
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
            }
            isCrosshairActive = false;
            doOnce = false; // Reset doOnce flag

            tennisBall = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            hand.PlayAnimation();

            if (hand.heldObject != null && hand.isThrown == false)
            {
                hand.ThrowObject();
            }
            else if (hand.heldObject == null && tennisBall != null)
            {
                hand.PickUpObject(tennisBall.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            racquet.PlayAnimation();
        }
    }

    void CrosshairChange(bool on)
    {
        if (crosshair != null) // Check if crosshair is not null
        {
            if (on && !doOnce)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
                isCrosshairActive = false;
            }
        }
    }
}
