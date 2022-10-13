using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class phpManager : MonoBehaviour
{
    private string postUrl = "https://cam-dev.000webhostapp.com/R-Type/SetInfo.php";
    private bool sendActive;

    void Start()
    {
        sendActive = true;
    }
    public void SendScore(InputField _name)
    {
        if (_name.text.Length > 0 && sendActive == true)
        {
            sendActive = false;
            StartCoroutine(SendInfo(_name.text));
        }
    }
    IEnumerator SendInfo(string _userName)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", _userName);
        form.AddField("score", PlayerValues.score);

        UnityWebRequest request = UnityWebRequest.Post(postUrl, form);
        yield return request.SendWebRequest();
    }
}
