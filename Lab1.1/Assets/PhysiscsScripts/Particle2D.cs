using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public Vector2 position, velocity, acceleration;

    public float rotation, angularVelocity, angularAcceleration;

    void updatePositionEulerExplicit(float dt)
    {
        // x(t+dt) = x(t) + v(t)dt
        // Euler:
        // F(t+dt) = F(t) + f(t)dt
        //                  + (dF/dt) dt

        position += velocity * dt;

        // v(t+dt) = v(t) + a(t)dt

        velocity += acceleration * dt;
    }

    void updatePositionKinematic(float dt)
    {

    }

    void updateRotationEulerExplicit(float dt)
    {
        rotation += angularVelocity * dt;
        rotation = (rotation + 360f) % 360f;

        angularVelocity += angularAcceleration * dt;
    }

    void updateRotationKinematic(float dt)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updatePositionEulerExplicit(Time.fixedDeltaTime);
        transform.position = position;

        updateRotationEulerExplicit(Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        acceleration.x = -Mathf.Sin(Time.fixedTime);
    }
}
