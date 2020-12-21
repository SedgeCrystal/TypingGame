using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    // Start is called before the first frame update
    CustomInputField customInputField;
   
    
    
    void Start()
    {
        this.customInputField = gameObject.GetComponent<CustomInputField>();
        this.customInputField.ActivateInputField();

    }

    public void InputText()
    {

        
        this.customInputField.ActivateInputField();

        this.customInputField.lastCaretPos = this.customInputField.caretPosition;
        this.customInputField.isAfterSent = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}


