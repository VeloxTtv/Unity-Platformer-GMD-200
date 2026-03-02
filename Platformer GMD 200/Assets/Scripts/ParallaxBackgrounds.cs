using UnityEngine;

public class ParallaxBackgrounds : MonoBehaviour
{
    public float parallaxFactor;
    private float length;
    private Transform cam;
    private GameObject clone;

    void Start()
    {
        cam = Camera.main.transform;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        SpriteRenderer thisSR = GetComponent<SpriteRenderer>();

        clone = new GameObject(gameObject.name + "_clone");
        clone.transform.position = new Vector3(transform.position.x + length, transform.position.y, transform.position.z);
        clone.transform.localScale = transform.lossyScale;

        SpriteRenderer cloneSR = clone.AddComponent<SpriteRenderer>();
        cloneSR.sprite = thisSR.sprite;
        cloneSR.sortingLayerID = thisSR.sortingLayerID;
        cloneSR.sortingOrder = thisSR.sortingOrder;
        cloneSR.color = thisSR.color;
    }

    void Update()
    {
        float x = cam.position.x * (1f - parallaxFactor);
        float xMod = Mathf.Repeat(x, length);

        transform.position = new Vector3(cam.position.x - xMod, transform.position.y, transform.position.z);
        clone.transform.position = new Vector3(cam.position.x - xMod + length, transform.position.y, transform.position.z);
    }

    void OnDestroy()
    {
        if (clone != null) Destroy(clone);
    }
}