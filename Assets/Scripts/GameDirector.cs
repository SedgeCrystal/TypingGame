using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{

    public bool isCorrect = false;
    InputFieldController inputFieldController;
    

    string s = "aaaa";

    void Start()
    {
        GameObject inputFieldObject = GameObject.FindGameObjectWithTag("InputField");
        this.inputFieldController = inputFieldObject.GetComponent<InputFieldController>();
        this.inputFieldController.exmStr = s;

        
    }

    // Update is called once per frame
    void Update()
    {
        


        if(this.isCorrect)
        {
            Debug.Log("Correct!");
            this.isCorrect = false;
        }


    }

}
