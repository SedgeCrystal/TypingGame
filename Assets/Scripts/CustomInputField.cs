using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomInputField : InputField
{
    public bool isAfterSent = false;
    public int lastCaretPos = 0;
    protected override void LateUpdate()
    {
       
        base.LateUpdate();

        if (isAfterSent)
        {
            
            MoveTextEnd(false);
            this.selectionAnchorPosition = this.lastCaretPos;
            this.selectionFocusPosition = this.lastCaretPos;
            isAfterSent = false;
        }
    }
}