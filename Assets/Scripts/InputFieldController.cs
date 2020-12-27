using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
 
    
    CustomInputField customInputField;
    GameDirector gameDirector;

    //Image determine background color
    Image inputFieldImage;

    //example text to compare
    public string exmStr;
    
    void Start()
    {
        this.customInputField = gameObject.GetComponent<CustomInputField>();
        GameObject gameDirectorObject = GameObject.FindGameObjectWithTag("GameDirector");
        this.gameDirector = gameDirectorObject.GetComponent<GameDirector>();
        this.customInputField.ActivateInputField();

        this.inputFieldImage = this.GetComponent<Image>();
    }

    //This is called when inputText is changed.
    //if input text and exmStr are same, color is green.
    //if input text is wrong, color is red.
    //if input text can be correct, color is black.
    public void OnTextChanged()
    {
        if (this.IsMatchText())
        {
            this.inputFieldImage.color = Color.green;
            return;
        }

        string str = this.customInputField.text;
        int l = str.Length;
        if(l > exmStr.Length)
        {
            this.inputFieldImage.color = Color.red;
            return;
        }

        string exmSubStr = exmStr.Substring(0, l);
        if (str.Equals(exmSubStr))
        {
            this.inputFieldImage.color = Color.black;
        }
        else
        {
            this.inputFieldImage.color = Color.red;
            
        }
    }


    //This is called when Enter is pressed.
    public void OnEndEditText()
    {
        
        if (this.IsMatchText())
        {
            this.gameDirector.IsCorrect = true;
            this.customInputField.text = "";
        }

        //After Enter is pressed, inputField become deactive.
        //To improve this, inputField have to be activated.
        this.customInputField.ActivateInputField();

        this.customInputField.lastCaretPos = this.customInputField.caretPosition;
        this.customInputField.isAfterSent = true;
        
        
    }

    bool IsMatchText()
    {
        return string.Equals(this.customInputField.text,this.exmStr);
    }

}


