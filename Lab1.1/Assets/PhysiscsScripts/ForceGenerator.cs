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
        Vector2 f_normal = (Vector2.Dot(f_gravity, surfaceNormal_unit) / Vector2.Dot(surfaceNormal_unit, surfaceNormal_unit)) * surfaceNormal_unit;
        return f_normal;
    }

    public static Vector2 GenerateForce_sliding(Vector2 f_gravity, Vector2 f_normal)
    {
        Vector2 f_sliding = f_gravity + f_normal;
        return f_sliding;
    }

    public static Vector2 GenerateForce_friction_static(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_static)
    {
        //make better
        Vector2 f_friction_s = f_opposing.magnitude < (frictionCoefficient_static * f_normal).magnitude ? -1 * f_opposing : -1 * frictionCoefficient_static * f_normal;
        return f_friction_s;
    }

    public static Vector2 GenerateForce_friction_kinetic(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_kinetic)
    {
        Vector2 f_friction_k =
    }

    public static Vector2 GenerateForce_drag(Vector2 particleVelocity, Vector2 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
    {

    }

    public static Vector2 GenerateForce_spring(Vector2 particlePosition, Vector2 anchorPosiiotn, float springRestingLength, float springStiffnessCoeffiecient)
    {

    }
}
