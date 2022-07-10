using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenManager : MonoBehaviour
{
    public Text tokenButtonText;

    // Start is called before the first frame update
    void Start()
    {
        tokenButtonText.text = "+ Token: " + PlayerInfo.tokens;
    }

    // Update is called once per frame
    void Update()
    {
        updateTokenText();
    }

    public void updateTokenText()
    {
        tokenButtonText.text = "+ Token: " + PlayerInfo.tokens;
    }
}
