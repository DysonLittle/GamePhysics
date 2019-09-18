using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Particle2D : MonoBehaviour
{
     float GRAV = 9.8f;

    public GameObject plane;

    /* UI */
    public Dropdown positionDropdown;
    public Dropdown rotationDropdown;
    public Dropdown forceDemoDropdown;
    public Slider slider;

    int positionMode, rotationMode, forceDemoMode;

    public bool sinAcceleration = true;


    /* Physics */

    public Vector2 position, velocity, acceleration;

    public float rotation, angularVelocity, angularAcceleration;

    public float startingMass = 1.0f;

    float mass, massInv;

    public void setMass(float newMass)
    {
        //mass = newMass > 0.0f ? newMass : 0.0f;
        mass = Mathf.Max(0.0f, newMass);
        massInv = mass > 0.0f ? 1.0f / mass : 0.0f;
    }

    public float getMass()
    {
        return mass;
    }

    

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

    public void changeForceDemoMode()
    {
        forceDemoMode = forceDemoDropdown.value;

        if (forceDemoMode == 0)
        {
            plane.SetActive(true);
            plane.transform.rotation = Quaternion.identity;
            plane.transform.Rotate(new Vector3(0, 0, -22.5f));
        }
        else if (forceDemoMode == 1)
        {
            plane.SetActive(true);
            plane.transform.rotation = Quaternion.identity;
            plane.transform.Rotate(new Vector3(0, 0, 45));
        }
        else if (forceDemoMode == 2)
        {
            plane.SetActive(false);
        }
        else if (forceDemoMode == 3)
        {
            plane.SetActive(true);
            plane.transform.rotation = Quaternion.identity;
        }

        reset();
    }

    public void changeSlider()
    {
        //slider.value;
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

    void reset()
    {
        position = Vector2.zero;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        positionMode = 0;
        rotationMode = 0;
        forceDemoMode = 0;

        setMass(startingMass);
    }

    Vector2 force;

    public void addForce(Vector2 newForce)
    {
        //D'Alembert
        force += newForce;
    }

    void updateAcceleration()
    {
        acceleration = force * massInv;
        force = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (positionMode == 0)
            updatePositionKinematic(Time.fixedDeltaTime);
        else if (positionMode == 1)
            updatePositionEulerExplicit(Time.fixedDeltaTime);
        transform.position = position;

        /* Rotation
         * 
        if (rotationMode == 0)
            updateRotationKinematic(Time.fixedDeltaTime);
        else if (rotationMode == 1)
            updateRotationEulerExplicit(Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        */



        /*
        if (sinAcceleration)
            acceleration.x = -Mathf.Sin(Time.fixedTime);
        else
            acceleration.x = 0;
        */

        //lab 2 test: gravity

        if (forceDemoMode == 0 || forceDemoMode == 1)
        {
            Vector2 grav_Force = ForceGenerator.GenerateForce_Gravity(mass, GRAV, Vector2.up);

            Vector2 normal_Force = ForceGenerator.GenerateForce_normal(grav_Force, plane.transform.up);

            addForce(ForceGenerator.GenerateForce_sliding(grav_Force, normal_Force));
        }
        else if (forceDemoMode == 2)
        {
            Vector2 grav_Force = ForceGenerator.GenerateForce_Gravity(mass, GRAV, Vector2.up);

            Vector2 drag_Force = ForceGenerator.GenerateForce_drag(velocity, Vector2.zero, slider.value * 1.225f * 2f, 1f, 1.05f);

            addForce(grav_Force);
            addForce(drag_Force);
        }
        else if (forceDemoMode == 3)
        {
            Vector2 grav_Force = ForceGenerator.GenerateForce_Gravity(mass, GRAV, Vector2.up);

            Vector2 normal_Force = ForceGenerator.GenerateForce_normal(grav_Force, plane.transform.up);

            Vector2 applied_Force = new Vector2(slider.value * 10f, 0f);

            Vector2 drag_Force;

            if (velocity == Vector2.zero)
            {
                drag_Force = ForceGenerator.GenerateForce_friction_static(normal_Force, applied_Force, 0.7f);
            }
            else
            {
                drag_Force = ForceGenerator.GenerateForce_friction_kinetic(normal_Force, velocity, 0.42f);
            }

            addForce(grav_Force);
            addForce(normal_Force);
            addForce(applied_Force);
            addForce(drag_Force);
        }
        

        updateAcceleration();
    }
}
