using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRandomBumps : MonoBehaviour
{
    public int numberOfBumps = 10;
    public int numberOfDips = 5;
    public float featureHeight = 0.5f;
    public float featureRadius = 2f;

    private Vector3[] originalVertices;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            originalVertices = meshFilter.mesh.vertices;
            ApplyRandomFeatures();
        }
        else
        {
            Debug.LogWarning("MeshFilter component not found.");
        }
    }

    void ApplyRandomFeatures()
    {
        if (originalVertices != null && originalVertices.Length > 0)
        {
            Vector3[] modifiedVertices = new Vector3[originalVertices.Length];

            for (int i = 0; i < originalVertices.Length; i++)
            {
                Vector3 vertex = originalVertices[i];
                Vector2 vertex2D = new Vector2(vertex.x, vertex.z);

                float featureAmount = 0f;

                // Создание выпуклостей
                for (int j = 0; j < numberOfBumps; j++)
                {
                    Vector2 featureCenter = Random.insideUnitCircle * featureRadius;
                    float distance = Vector2.Distance(vertex2D, featureCenter);
                    featureAmount += Mathf.Clamp01(1f - distance / featureRadius);
                }

                // Создание углублений
                for (int j = 0; j < numberOfDips; j++)
                {
                    Vector2 featureCenter = Random.insideUnitCircle * featureRadius;
                    float distance = Vector2.Distance(vertex2D, featureCenter);
                    featureAmount -= Mathf.Clamp01(1f - distance / featureRadius);
                }

                vertex.y += featureAmount * featureHeight;
                modifiedVertices[i] = vertex;
            }

            meshFilter.mesh.vertices = modifiedVertices;
            meshFilter.mesh.RecalculateNormals();
        }
    }
}


