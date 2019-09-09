using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator
{
    public static Vector2 GenerateForce_Gravity(float particleMass, float gravitationalConstant, Vector2 worldUp)
    {
        Vector2 f_gravity = particleMass * gravitationalConstant * (-1 * worldUp);
        return f_gravity;
    }

    public static Vector2 GenerateForce_normal(Vector2 f_gravity, Vector2 surfaceNormal_unit)
    {
        
    }

    public static Vector2 GenerateForce_sliding(Vector2 f_gravity, Vector2 f_normal)
    {

    }

    public static Vector2 GenerateForce_friction_static(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_static)
    {

    }

    public static Vector2 GenerateForce_friction_kinetic(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_kinetic)
    {

    }

    public static Vector2 GenerateForce_drag(Vector2 particleVelocity, Vector2 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
    {

    }

    public static Vector2 GenerateForce_spring(Vector2 particlePosition, Vector2 anchorPosiiotn, float springRestingLength, float springStiffnessCoeffiecient)
    {

    }
}
