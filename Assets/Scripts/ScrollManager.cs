using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    [SerializeField] GameObject groundObject;
    [SerializeField] float scrollSpeed;
    Material groundMaterial;
    bool scroll;
    float offset = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        groundMaterial = groundObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(scroll)
        {
            offset += Time.deltaTime * scrollSpeed;
            groundMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0));
            if (offset >= 1.0f) offset -= 1.0f;
        }
    }
    public void StartScroll() => scroll = true;
    public void EndScroll() => scroll = false;
}
