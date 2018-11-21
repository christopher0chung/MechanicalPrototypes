using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCG_RigidBodySerialized {

    public float mass;
    public float drag;
    public float angDrag;
    public bool useGravity;
    public bool isKinematic;
    public RigidbodyInterpolation interpolate;
    public CollisionDetectionMode collDetect;
    public RigidbodyConstraints constraints;

    public SCG_RigidBodySerialized(Rigidbody rigidbody)
    {
        mass = rigidbody.mass;
        drag = rigidbody.drag;
        angDrag = rigidbody.angularDrag;
        useGravity = rigidbody.useGravity;
        isKinematic = rigidbody.isKinematic;
        interpolate = rigidbody.interpolation;
        collDetect = rigidbody.collisionDetectionMode;
        constraints = rigidbody.constraints;
    }

    public SCG_RigidBodySerialized(float mass, 
        float drag, 
        float angDrag, 
        bool useGravity,
        bool isKinematic, 
        RigidbodyInterpolation interpolation, 
        CollisionDetectionMode collDetect, 
        RigidbodyConstraints constraints)
    {
        this.mass = mass;
        this.drag = drag;
        this.angDrag = angDrag;
        this.useGravity = useGravity;
        this.isKinematic = isKinematic;
        this.interpolate = interpolation;
        this.collDetect = collDetect;
        this.constraints = constraints;
    }

    public void RestoreRigidbody(Rigidbody rigidbody)
    {
        rigidbody.mass = mass;
        rigidbody.drag = drag;
        rigidbody.angularDrag = angDrag;
        rigidbody.useGravity = useGravity;
        rigidbody.isKinematic = isKinematic;
        rigidbody.interpolation = interpolate;
        rigidbody.collisionDetectionMode = collDetect;
        rigidbody.constraints = constraints;
    }
}
