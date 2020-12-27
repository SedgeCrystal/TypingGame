using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    //result of checking inputText
    public bool isCorrect = false;

    //other GameObject Controllers
    InputFieldController inputFieldController;
    ExampleTextController exampleTextController;
    
    //Text which this class controll
    Text timerText;
    Text lineText;
    Text titleText;
    Text countdownText;
    
    //GameObject to deactive typing game before the game start.
    GameObject gamePanel;

    //time until start
    float time;

    //line number in exampleText which should be being input
    public int line;
    
    //the number of line in example text
    int MAX_LINE;

    //List of example text;
    public List<string> exampleTextList;

    //name of chosen txt file
    public string title;

    //should start the game
    bool shouldStart;

    //countdown timer before game starts
    float countdownTimer;
   
    private void Awake()
    {
        this.line = 0;
        Debug.Log(this.line);
        this.time = 0;

        this.shouldStart = false;
        this.countdownTimer = 3;

       
    }


    void Start()
    {   
        //set GameObject Components in this Scene.
        GameObject inputFieldObject = GameObject.FindGameObjectWithTag("InputField");
        this.inputFieldController = inputFieldObject.GetComponent<InputFieldController>(); 
       
        GameObject exampleTextObject = GameObject.FindGameObjectWithTag("ExampleText");
        this.exampleTextController = exampleTextObject.GetComponent<ExampleTextController>();
 
        GameObject timerTextObject = GameObject.FindGameObjectWithTag("TimerText");
        this.timerText = timerTextObject.GetComponent<Text>();

        GameObject lineTextObject = GameObject.FindGameObjectWithTag("LineText");
        this.lineText = lineTextObject.GetComponent<Text>();

        GameObject titleTextObject = GameObject.FindGameObjectWithTag("TitleText");
        this.titleText = titleTextObject.GetComponent<Text>();

        this.gamePanel = GameObject.FindGameObjectWithTag("GamePanel");

        GameObject countdownTextObject = GameObject.FindGameObjectWithTag("CountdownText");
        this.countdownText = countdownTextObject.GetComponent<Text>();


        this.line = 0;
        this.MAX_LINE = exampleTextList.Count;
        //Debug.Log(this.MAX_LINE);
       
        //send Infomation about example text to ExamapleTextControlloer and InputFieldController
        this.SendExampleTextLineInfo();

        //set title
        this.titleText.text = this.title;

        //unitl game start, game is deactive.
        this.gamePanel.SetActive(false);
    }

    
    void Update()
    {

        this.UpdateTimer();
        this.UpdateLine();

        
        this.Countdown();
        this.ActivateGame();
        this.CheckIsCorrect();
        this.CheckEnd();

    }

    //send Infomation about example text to ExamapleTextControlloer and InputFieldController
    void SendExampleTextLineInfo()
    {
        if(line >= MAX_LINE)
        {
            return;
        }
        this.inputFieldController.exmStr = this.exampleTextList[line];
        this.exampleTextController.SetText(exampleTextList, line);
    }

    //update time and timerText
    void UpdateTimer()
    {
        this.time += Time.deltaTime;
        this.timerText.text = time.ToString("F1");
    }

    //update lineText
    void UpdateLine()
    {
        this.lineText.text = string.Format("{0:00}/{1:00}", line, MAX_LINE);
    }

    //check isCorrect
    //isCorrect is true, example text and input text is same
    void CheckIsCorrect()
    {
        //do nothing if isCorrect is false
        if (!this.isCorrect)
        {
            return;
        }

        //change next line
        this.line++;

        //check all line has typed
        if (this.line >= MAX_LINE)
        {
            //todo ResultScene
        }
        else
        {
            this.SendExampleTextLineInfo();

        }

        
        this.isCorrect = false;

    }

    //
    void ActivateGame()
    {

        this.gamePanel.SetActive(this.shouldStart);
        if (!this.shouldStart)
        {
            this.time = 0;
        }

    }


    void Countdown()
    {
        if (this.shouldStart)
        {
            return;
        }
        this.countdownTimer -= Time.deltaTime;



        if (this.countdownTimer < 0)
        {

            this.shouldStart = true;
            this.countdownText.text = "";
            return;
        }
        this.countdownText.text = Math.Ceiling(countdownTimer).ToString("F0");

       

    }

    void CheckEnd()
    {
        if(this.line != this.MAX_LINE)
        {
            return;
        }
        SceneManager.sceneLoaded += LoadResultScene;
        SceneManager.LoadScene("ResultScene");
    }

    void LoadResultScene(Scene next, LoadSceneMode mode)
    {
        GameObject resultDirectorObject = GameObject.FindGameObjectWithTag("ResultDirector");
        Debug.Log(resultDirectorObject);
        ResultDirector resultDirector = resultDirectorObject.GetComponent<ResultDirector>();
       
        int sum = 0;
        foreach (string exm in this.exampleTextList)
        {
            sum += exm.Length;

        }

        resultDirector.Title = this.title;
        resultDirector.Time = this.time;
        resultDirector.Wps = this.time / sum;
        resultDirector.ExmList = this.exampleTextList;

        SceneManager.sceneLoaded -= LoadResultScene;

    }
}
