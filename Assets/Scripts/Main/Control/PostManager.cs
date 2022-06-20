using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    public GameObject CanvasPost;

    // Start is called before the first frame update
    void Start()
    {
        CanvasPost.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideCanvasPost()
    {
        CanvasPost.SetActive(false);
    }

    public void showCanvasPost()
    {
        CanvasPost.SetActive(true);
    }
}
