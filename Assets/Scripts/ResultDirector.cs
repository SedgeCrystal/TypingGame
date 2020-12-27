using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultDirector : MonoBehaviour
{
    Text resultText;

    float wps;

    float time;

    List<string> exmList;

    string title;

    public float Wps { get => wps; set => wps = value; }
    public float Time { get => time; set => time = value; }
    public List<string> ExmList { get => exmList; set => exmList = value; }
    public string Title { get => title; set => title = value; }


    // Start is called before the first frame update
    void Start()
    {
        GameObject resultTextObject = GameObject.FindGameObjectWithTag("ResultText");
        this.resultText = resultTextObject.GetComponent<Text>();

        SetResultText();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void SetResultText()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(this.title);
        sb.AppendLine($"time:{time,3:F3}");
        sb.AppendLine($"wps:{wps,3:F3}");


        this.resultText.text = sb.ToString(); ;
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            SceneManager.LoadScene("SelectScene");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.sceneLoaded += LoadGameScene;
            SceneManager.LoadScene("GameScene");
        }
    }

    private void LoadGameScene(Scene next, LoadSceneMode mode)
    {
        GameObject gameDirectorObject = GameObject.FindGameObjectWithTag("GameDirector");
        
        GameDirector gameDirector = gameDirectorObject.GetComponent<GameDirector>();

        

        gameDirector.title = this.title;
        gameDirector.exampleTextList = this.exmList;
        gameDirector.line = 0;

        SceneManager.sceneLoaded -= LoadGameScene;
    }
}
