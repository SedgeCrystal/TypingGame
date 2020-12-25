using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;

public class SelectDirector : MonoBehaviour
{

    InputField inputField;
    
    // Start is called before the first frame update
    string path;


    OpenFileDialog openFileDialog;
    void Start()
    {
        path = "C:\\";
        GameObject inputFieldObject = GameObject.FindWithTag("InputField");
        this.inputField = inputFieldObject.GetComponent<InputField>();
        this.inputField.text = path;
    
        this.openFileDialog = new OpenFileDialog();       
        this.openFileDialog.Filter = "txt files (*.txt)|*.txt";
        this.openFileDialog.FileName = path;
        
        this.openFileDialog.CheckFileExists = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        
       

   
    }

    public void OnClickBrowseButton()
    {
        
        if (this.openFileDialog.ShowDialog() == DialogResult.OK)
        {
            this.path = this.openFileDialog.FileName;
        }
        
        this.inputField.text = path;

        
    } 

    public void OnEndPathEdit()
    {
        this.path = inputField.text;
        this.openFileDialog.FileName = this.path;
    }

   
}
