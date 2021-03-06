﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using UnityEngine.SceneManagement;

public class SelectDirector : MonoBehaviour
{
    //input path
    InputField inputField;

    //path to type
    string path;

    //the state of this Scene
    //State is defined at bottom of this file.
    State state;

    Text noticeText;

    //error message
    string errMsg;

    //max character in one line
    const int MAX_CHAR = 50;

    OpenFileDialog openFileDialog;

    //List of example text to type.
    List<string> exmList;


    void Start()
    {
        

        GameObject inputFieldObject = GameObject.FindWithTag("InputField");
        this.inputField = inputFieldObject.GetComponent<InputField>();
        

        GameObject noticeTextObject = GameObject.FindWithTag("NoticeText");
        this.noticeText = noticeTextObject.GetComponent<Text>();

        this.openFileDialog = new OpenFileDialog();
        //only txt file can be chosen
        this.openFileDialog.Filter = "txt files (*.txt)|*.txt";

        this.LoadPathData();
        this.ChangePath(this.path);

        this.openFileDialog.CheckFileExists = true;


        this.exmList = new List<string>();

       
    }


    void Update()
    {

        CheckPath();
        UpdateNoticeText();

    }
    void LoadPathData()
    {
        if (PlayerPrefs.HasKey("Path"))
        {
            this.path = PlayerPrefs.GetString("Path");
        }
        else
        {
            this.path = "C:\\";
        }
    }

    public void OnClickStartButton()
    {
        this.ReadText();


        this.CheckExmList();

        if (this.state == State.OK)
        {
            PlayerPrefs.SetString("Path", this.path);
            PlayerPrefs.Save();

            SceneManager.sceneLoaded += GameSceneLoaded;
            SceneManager.LoadScene("GameScene");
        }
    }

    void ReadText()
    {
        //if state is State.InvalidPath, do nothing.
        if (state == State.InvalidPath)
        {
            return;
        }

        //If exmList is not empty, exmList is assigned new List<string>
        if(this.exmList.Count > 0)
        {
            this.exmList = new List<string>();
        }

        try
        {

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();

                    //trim space at start and end of line.
                    line = line.Trim();

                    //if line is empty, line is not added in exmList.
                    if (line.Length == 0)
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
            //get type of exception.
            this.errMsg = $"{e.GetType()} has occurred!";
        }

    }

    void CheckExmList()
    {
        //if state is State.InvalidPath, do nothing.
        if (state == State.InvalidPath)
        {
            return;
        }

        

        //Check exmList is empty
        if (this.exmList.Count == 0)
        {
            this.state = State.Empty;
            return;
        }

        //Check elements of exmList contains many characters.
        foreach (string exm in exmList)
        {
            if (exm.Length > SelectDirector.MAX_CHAR)
            {
                this.state = State.OverMaxChar;
                return;
            }

        }
    }



    void CheckPath()
    {
        //if state isn't State.OK abd State.InvalidPath, do nothing.
        if (this.state != State.OK && this.state != State.InvalidPath)
        {
            return;
        }


        if (!File.Exists(this.path))
        {
            this.state = State.InvalidPath;
            return;
        }

        string ext = Path.GetExtension(this.path);
        this.state = (ext == ".txt") ? State.OK : State.InvalidPath;
    }

    void UpdateNoticeText()
    {

        string message = "";

        switch (this.state)
        {
            case State.OK:
                message = "";
                break;
            case State.InvalidPath:
                message = "This path is not pointing text file!";
                break;
            case State.Exception:
                message = this.errMsg;
                break;
            case State.Empty:
                message = "This file is empty!";
                break;
            case State.OverMaxChar:
                message = "This file contains too many character in one line!";
                break;
        }
        
        this.noticeText.text = message;
    }

    public void OnClickBrowseButton()
    {
        var dr = this.openFileDialog.ShowDialog();

        
        if (dr == DialogResult.OK)
        {
            this.ChangePath(this.openFileDialog.FileName);
        }
    }
    public void InputText()
    {
        this.ChangePath(this.inputField.text);

    }

    void ChangePath(string newPath)
    {
        this.path = newPath;

        this.openFileDialog.FileName = this.path;
        this.inputField.text = this.path;

        //If path is changed, state isn't Exception, Empty or OverMaxChar;
        this.state = State.InvalidPath;
    }


    //Before game starts, the information about file is passed.
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        GameObject gameDirectorObject = GameObject.FindGameObjectWithTag("GameDirector");
        GameDirector gameDirector = gameDirectorObject.GetComponent<GameDirector>();

        gameDirector.File = Path.GetFileName(this.path);
        gameDirector.exmList = this.exmList;

        //remove this function
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }


}

//Enum of state used in SelectDirector
enum State
{
    OK,
    InvalidPath,
    Exception,
    Empty,
    OverMaxChar
}
