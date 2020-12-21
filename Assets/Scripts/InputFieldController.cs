using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    // Start is called before the first frame update
    CustomInputField customInputField;
    GameDirector gameDirector;
    Image inputFieldImage;
    public string exmStr = "aaaa";
    
    void Start()
    {
        this.customInputField = gameObject.GetComponent<CustomInputField>();
        GameObject gameDirectorObject = GameObject.FindGameObjectWithTag("GameDirector");
        this.gameDirector = gameDirectorObject.GetComponent<GameDirector>();
        this.customInputField.ActivateInputField();

        this.inputFieldImage = this.GetComponent<Image>();
    }

    public void OnTextChanged()
    {
        if (this.isMatchText())
        {
            inputFieldImage.color = Color.green;
            return;
        }

        string str = this.customInputField.text;
        int l = str.Length;
        if(l > exmStr.Length)
        {
            inputFieldImage.color = Color.red;
            return;
        }

        string exmSubStr = exmStr.Substring(0, l);
        if (str.Equals(exmSubStr))
        {
            inputFieldImage.color = Color.black;
        }
        else
        {
            inputFieldImage.color = Color.red;
            
        }
    }

    public void OnEndEditText()
    {
        
        if (this.isMatchText())
        {
            gameDirector.isCorrect = true;
            this.customInputField.text = "";
        }
        else
        {
            gameDirector.isCorrect = false;
        }
        
        this.customInputField.ActivateInputField();

        this.customInputField.lastCaretPos = this.customInputField.caretPosition;
        this.customInputField.isAfterSent = true;
        
        
    }

    bool isMatchText()
    {
        return string.Equals(this.customInputField.text,this.exmStr);
    }

}


