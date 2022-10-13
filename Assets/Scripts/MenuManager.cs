using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Transform grid;
    public GameObject baseScore;

    public List<User> usuarios;
    private string urlGetInfo = "https://cam-dev.000webhostapp.com/R-Type/GetInfo.php";

    void Start()
    {
        StartCoroutine(GetInfo());
    }

    IEnumerator GetInfo()
    {
        UnityWebRequest request = UnityWebRequest.Get(urlGetInfo);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("ERROR");
            yield return 0;
        }
        SplitData(request.downloadHandler.text);
    }

    void SplitData(string _data)
    {
        string[] users = _data.Split(new string[] { "<br>" }, System.StringSplitOptions.None);
        for (int i = 0; i < users.Length -1; i++)
        {
            string[] userData = users[i].Split(new string[] { "<-->" }, System.StringSplitOptions.None);
            User newUser = new User(userData[0], int.Parse(userData[1]));
            usuarios.Add(newUser);
        }
        PrintUsers();
    }

    void PrintUsers()
    {
        for (int i = 0; i < usuarios.Count; i++)
        {
            GameObject newUser = Instantiate(baseScore, grid);
            newUser.transform.Find("Name").GetComponent<Text>().text = usuarios[i].userName;
            newUser.transform.Find("Score").GetComponent<Text>().text = usuarios[i].score.ToString();
        }
    }

    public void LoadGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
[System.Serializable]
public class User
{
    public string userName;
    public int score;

    public User(string _userName, int _score)
    {
        userName = _userName;
        score = _score;
    }
}
