using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This Class is created to avoid select all inputText after ActivateInputField();
public class CustomInputField : InputField
{

    //wheather edit is end
    public bool isAfterSent = false;

    //variable to set caret position
    public int lastCaretPos = 0;


    protected override void LateUpdate()
    {

        base.LateUpdate();

        
        if (!isAfterSent)
        {
            return;
        }

        base.MoveTextEnd(false);

        //fix caret position 
        this.selectionAnchorPosition = this.lastCaretPos;
        this.selectionFocusPosition = this.lastCaretPos;

        
        this.isAfterSent = false;

    }
}