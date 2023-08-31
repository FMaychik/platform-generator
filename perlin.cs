using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlin : MonoBehaviour
{
    public float reliefHeight = 1f;
    public float noiseScale = 0.1f;
    public float randomOffset = 0f;

    private Vector3[] originalVertices;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            originalVertices = meshFilter.mesh.vertices;
            ApplyPerlinRelief();
        }
        else
        {
            Debug.LogWarning("MeshFilter component not found.");
        }
    }

    void ApplyPerlinRelief()
    {
        if (originalVertices != null && originalVertices.Length > 0)
        {
            Vector3[] modifiedVertices = new Vector3[originalVertices.Length];

            for (int i = 0; i < originalVertices.Length; i++)
            {
                Vector3 vertex = originalVertices[i];
                float noiseValue = Mathf.PerlinNoise(
                    (vertex.x + randomOffset) * noiseScale,
                    (vertex.z + randomOffset) * noiseScale
                );
                vertex.y += noiseValue * reliefHeight;
                modifiedVertices[i] = vertex;
            }

            meshFilter.mesh.vertices = modifiedVertices;
            meshFilter.mesh.RecalculateNormals();
        }
    }
}
