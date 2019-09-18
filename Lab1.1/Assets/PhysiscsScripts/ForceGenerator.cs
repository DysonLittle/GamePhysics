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
        Vector2 f_normal = (Vector2.Dot(f_gravity, surfaceNormal_unit) / Vector2.Dot(surfaceNormal_unit, surfaceNormal_unit)) * -1 * surfaceNormal_unit;
        return f_normal;
    }

    public static Vector2 GenerateForce_sliding(Vector2 f_gravity, Vector2 f_normal)
    {
        Vector2 f_sliding = f_gravity + f_normal;
        return f_sliding;
    }

    public static Vector2 GenerateForce_friction_static(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_static)
    {
        Vector2 f_friction_s = f_opposing.magnitude < frictionCoefficient_static * f_normal.magnitude ? -1 * f_opposing : -1 * frictionCoefficient_static * f_normal.magnitude * f_opposing.normalized;
        return f_friction_s;
    }

    public static Vector2 GenerateForce_friction_kinetic(Vector2 f_normal, Vector2 particleVelocity, float frictionCoefficient_kinetic)
    {
        Vector2 f_friction_k = -1 * frictionCoefficient_kinetic * f_normal.magnitude * particleVelocity.normalized;
        return f_friction_k;
    }

    public static Vector2 GenerateForce_drag(Vector2 particleVelocity, Vector2 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
    {
        Vector2 relativeVelocity = fluidVelocity - particleVelocity;
        Vector2 f_drag = 0.5f * fluidDensity * Vector2.Dot(relativeVelocity, relativeVelocity) * objectDragCoefficient * objectArea_crossSection * (-1.0f * particleVelocity.normalized);
        return f_drag;
    }

    public static Vector2 GenerateForce_spring(Vector2 particlePosition, Vector2 anchorPosition, float springRestingLength, float springStiffnessCoefficient)
    {
        Vector2 f_spring = -1 * springStiffnessCoefficient * ((particlePosition - anchorPosition) - (springRestingLength * (particlePosition - anchorPosition).normalized));
        return f_spring;
    }
}
