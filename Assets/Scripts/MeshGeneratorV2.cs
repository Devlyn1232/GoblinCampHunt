
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshFilter))]
public class MeshGeneratorV2 : MonoBehaviour
{
    Mesh mesh;
    private int MESH_SCALE = 5;
    public GameObject[] objects;
    public GameObject GoblinCamp;
    public bool GoblinCampSpawned = false;
    [SerializeField] private AnimationCurve heightCurve;
    private Vector3[] vertices;
    private int[] triangles;
    
    private Color[] colors;
    [SerializeField] private Gradient gradient;
    
    private float minTerrainheight;
    private float maxTerrainheight;

    public int xSize;
    public int zSize;

    public float scale; 
    public int octaves;
    public float lacunarity;

    public int seed;

    private float lastNoiseHeight;
    
    public NavMeshSurface navSurface;

    void Start()
    {
        // Use this method if you havn't filled out the properties in the inspector
        // SetNullProperties(); 
        
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateNewMap();
        Invoke("generateNavMesh", .1f);
    }
    void generateNavMesh()
    {
        navSurface.BuildNavMesh();
    }

    private void SetNullProperties() 
    {
        if (xSize <= 0) xSize = 50;
        if (zSize <= 0) zSize = 50;
        if (octaves <= 0) octaves = 5;
        if (lacunarity <= 0) lacunarity = 2;
        if (scale <= 0) scale = 50;
    } 

    public void CreateNewMap()
    {
        CreateMeshShape();
        CreateTriangles();
        ColorMap();
        UpdateMesh();
    }
    /*
    private void CreateMeshShape ()
    {
        // Creates seed
        Vector2[] octaveOffsets = GetOffsetSeed();

        if (scale <= 0) scale = 0.0001f;
            
        // Create vertices
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                // Set height of vertices
                float noiseHeight = GenerateNoiseHeight(z, x, octaveOffsets);
                SetMinMaxHeights(noiseHeight);
                vertices[i] = new Vector3(x, noiseHeight, z);
                i++;
            }
        }
    }
    */
    //*
    private void CreateMeshShape()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        // Calculate the center position for reference
        Vector2 center = new Vector2(xSize / 2f, zSize / 2f);
        float islandRadius = Mathf.Min(xSize, zSize) / 2f; // Define the effective radius of the island

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                Vector2 position = new Vector2(x, z);
                float distanceFromCenter = Vector2.Distance(position, center);
                float normalizedDistance = distanceFromCenter / islandRadius;

                // Determine if the current position is at the edge
                bool isEdge = x == 0 || z == 0 || x == xSize || z == zSize;

                // Generate noise height
                Vector2[] octaveOffsets = GetOffsetSeed();
                float noiseHeight = GenerateNoiseHeight(z, x, octaveOffsets);
                
                // Adjust heights
                float height = 0f;
                if (!isEdge)
                {
                    // Apply the height curve and adjust based on the distance from the center
                    float heightFactor = heightCurve.Evaluate(1 - normalizedDistance);
                    height = noiseHeight * heightFactor;
                    
                    // Update min/max terrain heights for coloring
                    if (height > maxTerrainheight) maxTerrainheight = height;
                    if (height < minTerrainheight) minTerrainheight = height;
                }
                
                vertices[i++] = new Vector3(x, height, z);
            }
        }

        UpdateMesh(); // Ensure this method is called after vertices are set
    }
    //*/

    private Vector2[] GetOffsetSeed()
    {
        if (seed == 0f)
        {
            seed = Random.Range(0, 1000);
        }
        
        
        // changes area of map
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
                    
        for (int o = 0; o < octaves; o++) {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[o] = new Vector2(offsetX, offsetY);
        }
        return octaveOffsets;
    }

    private float GenerateNoiseHeight(int z, int x, Vector2[] octaveOffsets)
    {
        float amplitude = 20;
        float frequency = 1;
        float persistence = 0.5f;
        float noiseHeight = 0;

        // loop over octaves
        for (int y = 0; y < octaves; y++)
        {
            float mapZ = z / scale * frequency + octaveOffsets[y].y;
            float mapX = x / scale * frequency + octaveOffsets[y].x;

            //The *2-1 is to create a flat floor level
            float perlinValue = (Mathf.PerlinNoise(mapZ, mapX)) * 2 - 1;
            noiseHeight += heightCurve.Evaluate(perlinValue) * amplitude;
            frequency *= lacunarity;
            amplitude *= persistence;
        }
        return noiseHeight;
    }

    private void SetMinMaxHeights(float noiseHeight)
    {
        // Set min and max height of map for color gradient
        if (noiseHeight > maxTerrainheight)
            maxTerrainheight = noiseHeight;
        if (noiseHeight < minTerrainheight)
            minTerrainheight = noiseHeight;
    }


    private void CreateTriangles() 
    {
        // Need 6 vertices to create a square (2 triangles)
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        // Go to next row
        for (int z = 0; z < xSize; z++)
        {
            // fill row
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void ColorMap()
    {
        colors = new Color[vertices.Length];

        // Loop over vertices and apply a color from the depending on height (y axis value)
        for (int i = 0, z = 0; z < vertices.Length; z++)
        {
            float height = Mathf.InverseLerp(minTerrainheight, maxTerrainheight, vertices[i].y);
            colors[i] = gradient.Evaluate(height);
            i++;
        }
    }

    private void MapEmbellishments() 
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            // find actual position of vertices in the game
            Vector3 worldPt = transform.TransformPoint(mesh.vertices[i]);
            var noiseHeight = worldPt.y;
            // Stop generation if height difference between 2 vertices is too steep
            if(System.Math.Abs(lastNoiseHeight - worldPt.y) < 25)
            {
                // min height for object generation
                if (noiseHeight > 3)
                {
                    // Chance to generate
                    if (Random.Range(1, 5) == 1)
                    {
                        GameObject objectToSpawn = objects[Random.Range(0, objects.Length)];
                        var spawnAboveTerrainBy = noiseHeight * 2;
                        Instantiate(objectToSpawn, new Vector3(mesh.vertices[i].x * MESH_SCALE, spawnAboveTerrainBy, mesh.vertices[i].z * MESH_SCALE), Quaternion.identity);
                    }
                }
            }
            lastNoiseHeight = noiseHeight;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.transform.localScale = new Vector3(MESH_SCALE, MESH_SCALE, MESH_SCALE);
        SpawnObjectOnHighestPoint();
        MapEmbellishments();
        // Call after updating the mesh to ensure we have the latest terrain info
        
    }

    private void SpawnObjectOnHighestPoint()
    {
        if (!GoblinCampSpawned && GoblinCamp != null)
        {
            // Find the highest point on the terrain
            Vector3 highestPoint = Vector3.zero;
            foreach (var vertex in vertices)
            {
                if (vertex.y > highestPoint.y)
                {
                    highestPoint = vertex;
                }
            }

            // Adjust the highest point based on the Mesh scale and position
            Vector3 worldPoint = transform.TransformPoint(highestPoint);

            // Instantiate the object at the highest point
            Instantiate(GoblinCamp, worldPoint, Quaternion.identity);

            GoblinCampSpawned = true; // Prevent further spawns
        }
    }
}