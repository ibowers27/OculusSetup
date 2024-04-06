using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Floating mechanism: https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html & https://discussions.unity.com/t/how-to-make-an-object-float-in-space/79357
Stop when grabbed: https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html & https://forum.unity.com/threads/apply-force-to-rigidbody-using-float.787160/
Inheritence: https://learn.unity.com/tutorial/inheritance#5c8924f2edbc2a0d28f48439, https://www.youtube.com/watch?v=iw1JWGv1gLY&ab_channel=Comp-3Interactive, 
& https://discussions.unity.com/t/does-my-base-class-that-inherits-from-monobehaviour-needs-to-be-attached-to-an-gameobject-within-a-scene/168504 
**/


// BalloonBehavior extends or is inheritted from OVRGrabbable
public class BalloonBehavior : OVRGrabbable
{
    // initialize variable containing the force of the float
    private float floatForce = 5f;
    // initialize rigid body variable
    private Rigidbody rb;
    // create a boolean that verifies if the baloon has been grabbed before being released
    private bool hasBeenGrabbed = false;

    // Start method in OVRGrabbable is protected, so this has to be protected to prevent error; inherits and overrides method from OVRGrabbable
    protected override void Start()
    {
        // start method from OVRGrabbable
        base.Start();
        // Get the rigid body componenet from the object attached
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // if hasBeenGrabbed is true
        if (hasBeenGrabbed) 
        {
            // coninuously add the upward (y) force
            rb.AddForce(Vector3.up * floatForce * Time.deltaTime);
        }
    }

    // inherits and overrides method from OVRGrabbable
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            // GrabBegin method from OVRGrabbable
            base.GrabBegin(hand, grabPoint);

            // if hasBeenGrabbed is false
            if (!hasBeenGrabbed)
            {
                // set it to true at the beginning of the grab
                hasBeenGrabbed = true;
            }
    }

    // inherits and overrides method from OVRGrabbable
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        // GrabEnd method from OVRGrabbable
        base.GrabEnd(linearVelocity, angularVelocity);

        // if hasBeenGrabbed is true
        if (hasBeenGrabbed) 
        {
            // stop the object from floating if grabbed again
            rb.velocity = Vector3.zero;
        }
    }
}