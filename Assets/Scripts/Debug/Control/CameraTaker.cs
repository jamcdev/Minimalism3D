using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTaker : MonoBehaviour
{
    WebCamTexture webCamTexture;
    //public GameObject photoPlane;z
    public RawImage rawImage;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        //GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to

        // For object.
        //photoPlane.GetComponent<Renderer>().material.mainTexture = webCamTexture;

        // For UI RawImage
        rawImage.texture = webCamTexture;
        rawImage.material.mainTexture = webCamTexture;


        webCamTexture.Play();
    }

    public void startTakePhotoCoroutine()
    {
        StartCoroutine(TakePhoto());
    }

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {

        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();


        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        // Freeze photoFrame
        rawImage.texture = photo;

        // save static photoFrame data
        //FormData.itemPhoto = photo;

        ////Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        ////Write out the PNG. Of course you have to substitute your_path for something sensible
        //File.WriteAllBytes(your_path + "photo.png", bytes);

        //Convert byte[] to string base 64
        string photoStringb64 = System.Convert.ToBase64String(bytes);

        FormData.itemPhoto = photoStringb64;
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
