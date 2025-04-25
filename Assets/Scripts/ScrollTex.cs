using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public float scrollSpeedX = 0.1f; // Speed of scrolling on X-axis
    public float scrollSpeedY = 0.1f; // Speed of scrolling on Y-axis

    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        offset = rend.sharedMaterial.mainTextureOffset;
    }

    void Update()
    {
        offset.x += scrollSpeedX * Time.deltaTime;
        offset.y += scrollSpeedY * Time.deltaTime;

        // Apply the offset to all materials
        foreach (Material mat in rend.materials)
        {
            mat.mainTextureOffset = offset;
        }
    }
}
