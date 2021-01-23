using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGizmo : MonoBehaviour
{
    private enum Shape
    {
        Cube = 1,
        Sphere = 2,
    }

    [SerializeField] private Shape _shape = Shape.Cube;
    [SerializeField] private Color _color = Color.cyan;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        switch (_shape)
        {
            case Shape.Cube:
                Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
                break;

            case Shape.Sphere:
                Gizmos.DrawSphere(transform.position, 0.2f);
                break;
        }
    }
}
