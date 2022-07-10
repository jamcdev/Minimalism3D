using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    public GameObject CanvasPost;
    public GameObject CanvasPostAvaliableItems;

    public PutPlaceholder putPlaceholder;

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
        CanvasPostAvaliableItems.SetActive(false);

    }

    public void cancelAndHideCanvasPost()
    {
        CanvasPost.SetActive(false);
        CanvasPostAvaliableItems.SetActive(false);

        // hide red box since its not being posted.
        PlayerInfo.tokens++; // return token
        Destroy(putPlaceholder.redPlaceholder);
    }

    public void showCanvasPost()
    {
        CanvasPost.SetActive(true);
    }


}
