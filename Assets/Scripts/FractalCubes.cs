using UnityEngine;

public class FractalCubes : MonoBehaviour
{
    public GameObject cubePrefab; // Assign a cube prefab in the Inspector
    public int maxDepth = 3; // Number of recursion levels
    public float scaleFactor = 0.5f; // Shrinking factor for child cubes
    private GameObject rootObject;

    private static readonly Vector3[] positions = {
        new Vector3(-1, -1, -1), new Vector3(-1, -1, 0), new Vector3(-1, -1, 1),
        new Vector3(-1,  0, -1),                      new Vector3(-1,  0, 1),
        new Vector3(-1,  1, -1), new Vector3(-1,  1, 0), new Vector3(-1,  1, 1),

        new Vector3(0, -1, -1),                      new Vector3(0, -1, 1),

        new Vector3(0,  1, -1),                      new Vector3(0,  1, 1),

        new Vector3(1, -1, -1), new Vector3(1, -1, 0), new Vector3(1, -1, 1),
        new Vector3(1,  0, -1),                      new Vector3(1,  0, 1),
        new Vector3(1,  1, -1), new Vector3(1,  1, 0), new Vector3(1,  1, 1)
    };

    void Start()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube prefab is not assigned in the Inspector!");
            return;
        }

        rootObject = new GameObject("FractalRoot");
        GenerateFractal(Vector3.zero, Quaternion.identity, 1, 0, rootObject.transform);
    }

    void GenerateFractal(Vector3 position, Quaternion rotation, float scale, int currentDepth, Transform parent)
    {
        if (currentDepth >= maxDepth) return;

        GameObject cube = Instantiate(cubePrefab, position, rotation);
        cube.transform.localScale = Vector3.one * scale;
        cube.transform.parent = parent;

        // Change color to better visualize different levels
        cube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

        for (int i = 0; i < positions.Length; i++)
        {
            GenerateFractal(
                position + positions[i] * (scale * 0.6f),
                rotation,
                scale * scaleFactor,
                currentDepth + 1,
                cube.transform // Each child is parented to its own cube
            );
        }
    }
}
