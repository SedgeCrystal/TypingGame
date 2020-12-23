using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //time until start
    float time;

    //line number in exampleText which should be being input
    int line;
    
    //the number of line in example text
    int MAX_LINE;

    //List of example text;
    public List<string> exampleTextList;

    //name of chosen txt file
    string title;

   
    private void Awake()
    {
        this.line = 0;
        // get information from SelectScene

        this.exampleTextList = new List<string>() { "aaa", "bbb","ccc","ddd","eee"};
        MAX_LINE = exampleTextList.Count;

        this.time = -3;
        this.title = "Sample";
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


        //send Infomation about example text to ExamapleTextControlloer and InputFieldController
        SendExampleTextLineInfo();

        //set title
        this.titleText.text = this.title;
    }

    
    void Update()
    {

        this.UpdateTimer();
        this.UpdateLine();


        //todo CountDown before start the game
        if (time < 0)
        {

            return; 
        }

        this.CheckIsCorrect();

       
    }

    //send Infomation about example text to ExamapleTextControlloer and InputFieldController
    void SendExampleTextLineInfo()
    {
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
        line++;

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
}
