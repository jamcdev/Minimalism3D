using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTaker : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public GameObject photoPlane;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        //GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        photoPlane.GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {

        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();


        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        ////Encode to a PNG
        //byte[] bytes = photo.EncodeToPNG();
        ////Write out the PNG. Of course you have to substitute your_path for something sensible
        //File.WriteAllBytes(your_path + "photo.png", bytes);
    }

    Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }
}
