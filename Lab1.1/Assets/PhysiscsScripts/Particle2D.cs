﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Particle2D : MonoBehaviour
{
    public Dropdown positionDropdown;
    public Dropdown rotationDropdown;

    int positionMode, rotationMode;

    public Vector2 position, velocity, acceleration;

    public float rotation, angularVelocity, angularAcceleration;

    public bool sinAcceleration = true;

    public void changePositionMode()
    {
        positionMode = positionDropdown.value;
        Debug.Log(positionMode);
    }

    public void changeRotationMode()
    {
        rotationMode = rotationDropdown.value;
        Debug.Log(rotationMode);
    }

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
        // x(t+dt) = x(t) + v(t)dt + 0.5 * a(t) * dt^2

        position = position + velocity * dt + 0.5f * acceleration * dt * dt;

        // v(t+dt) = v(t) + a(t) * dt

        velocity = velocity + acceleration * dt;
    }

    void updateRotationEulerExplicit(float dt)
    {
        rotation += angularVelocity * dt;
        rotation = (rotation + 360f) % 360f;

        angularVelocity += angularAcceleration * dt;
    }

    void updateRotationKinematic(float dt)
    {
        rotation = rotation + angularVelocity * dt + 0.5f * angularAcceleration * dt * dt;
        rotation = (rotation + 360f) % 360f;

        angularVelocity = angularVelocity + angularAcceleration * dt;
    }

    // Start is called before the first frame update
    void Start()
    {
        positionMode = 0;
        rotationMode = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (positionMode == 0)
            updatePositionKinematic(Time.fixedDeltaTime);
        else if (positionMode == 1)
            updatePositionEulerExplicit(Time.fixedDeltaTime);
        transform.position = position;

        if (rotationMode == 0)
            updateRotationKinematic(Time.fixedDeltaTime);
        else if (rotationMode == 1)
            updateRotationEulerExplicit(Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        if (sinAcceleration)
            acceleration.x = -Mathf.Sin(Time.fixedTime);
        else
            acceleration.x = 0;
    }
}
