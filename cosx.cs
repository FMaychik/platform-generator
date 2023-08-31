using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cosx : MonoBehaviour
{
    public float reliefHeight = 1f;
    public float reliefFrequency = 1f;

    private Vector3[] originalVertices;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            originalVertices = meshFilter.mesh.vertices;
            ApplyCosineRelief();
        }
        else
        {
            Debug.LogWarning("MeshFilter component not found.");
        }
    }

    void ApplyCosineRelief()
    {
        if (originalVertices != null && originalVertices.Length > 0)
        {
            Vector3[] modifiedVertices = new Vector3[originalVertices.Length];

            for (int i = 0; i < originalVertices.Length; i++)
            {
                Vector3 vertex = originalVertices[i];
                vertex.y += Mathf.Cos(vertex.x * reliefFrequency) * reliefHeight;
                modifiedVertices[i] = vertex;
            }

            meshFilter.mesh.vertices = modifiedVertices;
            meshFilter.mesh.RecalculateNormals();
        }
    }
}
