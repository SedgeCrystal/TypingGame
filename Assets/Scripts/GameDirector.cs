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
    private bool isCorrect = false;

    //other GameObject Controllers
    InputFieldController inputFieldController;
    ExampleTextController exampleTextController;
    
    //Texts which this class controll
    Text timerText;
    Text lineText;
    Text titleText;
    Text countdownText;
    
    //GameObject to deactive typing game before the game start.
    GameObject gamePanel;

    //time until start
    float time;

    //line number in exampleText which should be being input
    int line;
    
    //the number of line in example text
    int MAX_LINE;

    //List of example text;
    public List<string> exmList;

    //name of chosen txt file
    private string file;

    //should start the game
    bool shouldStart;

    //countdown timer before game starts
    float countdownTimer;

    public bool IsCorrect { get => isCorrect; set => isCorrect = value; }
    public string File { get => file; set => file = value; }

    private void Awake()
    {   
        //initialize property.
        this.line = 0;
        this.time = 0;

        this.shouldStart = false;
        this.countdownTimer = 3;

       
    }


    void Start()
    {
        //set GameObject Components in this Scene.
        SetComponents();

        
        this.MAX_LINE = exmList.Count;
        
       
        //send Infomation about example text to ExamapleTextControlloer and InputFieldController
        this.SendExampleTextLineInfo();

        //set title
        this.titleText.text = this.File;

        //unitl game start, game is deactive.
        this.gamePanel.SetActive(false);
    }


    //set GameObject Components in this Scene.
    void SetComponents()
    {
        
        GameObject inputFieldObject = GameObject.FindGameObjectWithTag("InputField");
        this.inputFieldController = inputFieldObject.GetComponent<InputFieldController>();

        GameObject exampleTextObject = GameObject.FindGameObjectWithTag("ExampleText");
        this.exampleTextController = exampleTextObject.GetComponent<ExampleTextController>();

        GameObject timerTextObject = GameObject.FindGameObjectWithTag("TimerText");
        this.timerText = timerTextObject.GetComponent<Text>();

        GameObject lineTextObject = GameObject.FindGameObjectWithTag("LineText");
        this.lineText = lineTextObject.GetComponent<Text>();

        GameObject titleTextObject = GameObject.FindGameObjectWithTag("FileNameText");
        this.titleText = titleTextObject.GetComponent<Text>();

        this.gamePanel = GameObject.FindGameObjectWithTag("GamePanel");

        GameObject countdownTextObject = GameObject.FindGameObjectWithTag("CountdownText");
        this.countdownText = countdownTextObject.GetComponent<Text>();

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
        this.inputFieldController.exmStr = this.exmList[line];
        this.exampleTextController.SetText(exmList, line);
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
        this.lineText.text = string.Format("{0:000}/{1:000}", line, MAX_LINE);
    }

    //check isCorrect
    //isCorrect is true, example text and input text is same
    void CheckIsCorrect()
    {
        //do nothing if isCorrect is false
        if (!this.IsCorrect)
        {
            return;
        }

        //change next line
        this.line++;

    
        this.SendExampleTextLineInfo();

        this.IsCorrect = false;

    }


    //while countdown, the game doesn't start.
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
        //if game has started, do nothing.
        if (this.shouldStart)
        {
            return;
        }

        this.countdownTimer -= Time.deltaTime;


        //if countdown finish, game starts.
        if (this.countdownTimer < 0)
        {

            this.shouldStart = true;
            this.countdownText.text = "";
            return;
        }
        this.countdownText.text = Math.Ceiling(countdownTimer).ToString("F0");

       

    }
    //if game ends, load ResultScene.
    void CheckEnd()
    {
        if (this.line < this.MAX_LINE)
        {
            return;
        }
        SceneManager.sceneLoaded += ResultSceneLoaded;
        SceneManager.LoadScene("ResultScene");
    }


    //Before showing results, the information about game is passed.
    void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        GameObject resultDirectorObject = GameObject.FindGameObjectWithTag("ResultDirector");
        Debug.Log(resultDirectorObject);
        ResultDirector resultDirector = resultDirectorObject.GetComponent<ResultDirector>();
       
        int sum = 0;
        foreach (string exm in this.exmList)
        {
            sum += exm.Length;

        }


        //imfortaion is passed to ResultDirector.
        resultDirector.Title = this.File;
        resultDirector.Time = this.time;
        resultDirector.Wps = sum / this.time;
        resultDirector.ExmList = this.exmList;

        SceneManager.sceneLoaded -= ResultSceneLoaded;

    }
}
