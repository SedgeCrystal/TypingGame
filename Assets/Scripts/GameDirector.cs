using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{

    public bool isCorrect = false;
    InputFieldController inputFieldController;
    ExampleTextController exampleTextController;
    
    Text timerText;
    Text lineText;
    Text titleText;

    float time;
    int line;
    
    int MAX_LINE;

    public List<string> exampleTextList;
    string title;

   

    private void Awake()
    {
        this.line = 0;

        this.exampleTextList = new List<string>() { "aaa", "bbb","ccc","ddd","eee"};
        MAX_LINE = exampleTextList.Count;

        this.time = -3;
        this.title = "Sample";
    }
    void Start()
    {
        GameObject inputFieldObject = GameObject.FindGameObjectWithTag("InputField");
        this.inputFieldController = inputFieldObject.GetComponent<InputFieldController>();
        this.inputFieldController.exmStr = this.exampleTextList[line];
        


        GameObject exampleTextObject = GameObject.FindGameObjectWithTag("ExampleText");
        this.exampleTextController = exampleTextObject.GetComponent<ExampleTextController>();
        this.exampleTextController.SetText(exampleTextList,line);

        GameObject timerTextObject = GameObject.FindGameObjectWithTag("TimerText");
        this.timerText = timerTextObject.GetComponent<Text>();

        GameObject lineTextObject = GameObject.FindGameObjectWithTag("LineText");
        this.lineText = lineTextObject.GetComponent<Text>();

        GameObject titleTextObject = GameObject.FindGameObjectWithTag("TitleText");
        this.titleText = titleTextObject.GetComponent<Text>();
        this.titleText.text = this.title;
    }

    // Update is called once per frame
    void Update()
    {

        this.time += Time.deltaTime;
        
        if(time < 0)
        {

            return; 
        }
        this.timerText.text = time.ToString("F1");


        this.lineText.text = string.Format("{0:00}/{1:00}", line, MAX_LINE);

        if (this.isCorrect)
        {
            line++;
            Debug.Log(line);
            if (this.line >= MAX_LINE)
            {
                Debug.Log("END");

            }
            else
            {
                this.inputFieldController.exmStr = this.exampleTextList[line];
                this.exampleTextController.SetText(exampleTextList, line);
                
            }
            this.isCorrect = false;
        }


    }

}
