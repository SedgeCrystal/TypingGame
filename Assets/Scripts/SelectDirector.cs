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

    //the state of this Scene
    //0:unable to start
    //1:able to start
    //2:error occur while read file
    State state;
    Text noticeText;
    string errMsg;

    const int MAX_CHAR = 50;

    OpenFileDialog openFileDialog;

    List<string> exmList;


    void Start()
    {
        path = "C:\\";
        GameObject inputFieldObject = GameObject.FindWithTag("InputField");
        this.inputField = inputFieldObject.GetComponent<InputField>();
        this.inputField.text = path;

        GameObject noticeTextObject = GameObject.FindWithTag("NoticeText");
        this.noticeText = noticeTextObject.GetComponent<Text>();

        this.openFileDialog = new OpenFileDialog();       
        this.openFileDialog.Filter = "txt files (*.txt)|*.txt";
        this.openFileDialog.FileName = path;
        
        this.openFileDialog.CheckFileExists = true;

        this.exmList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.state);
        CheckPath();
        UpdateNoticeText();
       
    }

    public void OnClickBrowseButton()
    {
        
        if (this.openFileDialog.ShowDialog() == DialogResult.OK)
        {
            this.path = this.openFileDialog.FileName;
        }
        
        this.inputField.text = path;

        
    } 

    public void OnClickStartButton()
    {
        this.ReadText();
        
        

        Debug.Log(this.exmList.Count);
        

        this.CheckExmList();

        if(this.state == State.OK)
        {
            Debug.Log("OK");
        }
    }

    void ReadText()
    {
        if (state == State.NotExist)
        {
            return;
        }
        try
        {
            
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    line = line.Trim();
                    if(line.Length == 0)
                    {
                        continue;
                    }
                    this.exmList.Add(line);
                }
            }
            
        }
        catch (Exception e)
        {
            state = State.Exception;
            this.errMsg = $"{e.GetType()} has occurred.";
        }

    }
    
    void CheckExmList()
    {
        if (state == State.NotExist)
        {
            return;
        }

        if (this.exmList.Count == 0)
        {
            this.state = State.Empty;
            return;
        }

        foreach(string exm in exmList)
        {
            if(exm.Length > SelectDirector.MAX_CHAR)
            {
                this.state = State.OverMaxChar;
                return;
            }
            {

            }
        }
    }

    public void OnEndPathEdit()
    {
        this.path = inputField.text;
        this.openFileDialog.FileName = this.path;
    }

    void CheckPath()
    {
        if(this.state != State.OK && this.state !=State.NotExist)
        {
            return;
        }

        if (!File.Exists(this.path))
        {
            this.state = State.NotExist;
            return;
        }

        string ext = Path.GetExtension(this.path);
        this.state = (ext == ".txt") ? State.OK : State.NotExist;
    }

    void UpdateNoticeText()
    {

        string message = "";
        switch (this.state)
        {
            case State.NotExist: message = "THIS PATH IS NOT POITING TEXT FILE!";
                break;
            case State.Exception: message = this.errMsg;
                break;
            case State.Empty: message = "THIS FILE IS EMPTY";
                break;
            case State.OverMaxChar:
                message = "THIS FILE CONTAINS TOO MANY CHARACTERS";
                break;
        }

        this.noticeText.text = message;
    }
   
}

enum State
{
    OK,
    NotExist,
    Exception,
    Empty,
    OverMaxChar
}
