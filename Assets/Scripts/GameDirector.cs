using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{

    public bool isCorrect = false;
    InputFieldController inputFieldController;
    ExampleTextController exampleTextController;

    int line;
    int MAX_LINE;

    public List<string> exampleTextList;
   

   

    private void Awake()
    {
        this.line = 0;

        this.exampleTextList = new List<string>() { "aaa", "bbb","ccc","ddd","eee"};
        MAX_LINE = exampleTextList.Count;


    }
    void Start()
    {
        GameObject inputFieldObject = GameObject.FindGameObjectWithTag("InputField");
        this.inputFieldController = inputFieldObject.GetComponent<InputFieldController>();
        this.inputFieldController.exmStr = this.exampleTextList[line];



        GameObject exampleTextObject = GameObject.FindGameObjectWithTag("ExampleText");
        this.exampleTextController = exampleTextObject.GetComponent<ExampleTextController>();
        this.exampleTextController.SetText(exampleTextList,line);
        
    }

    // Update is called once per frame
    void Update()
    {
        


        if(this.isCorrect)
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
