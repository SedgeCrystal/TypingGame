using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ExampleTextController : MonoBehaviour
{
    const int MAX_ROW = 4;

    Text exampleText;
    

    GameDirector gameDirector;
    void Start()
    {
        this.exampleText = gameObject.GetComponent<Text>();
        
        GameObject gameDirectorObject = GameObject.FindGameObjectWithTag("GameDirector");
        this.gameDirector = gameDirectorObject.GetComponent<GameDirector>();
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(List<string> list,int line)
    {
        this.exampleText.text = ArrangeText(list,line);
        
    }

    string ArrangeText(List<string> list,int line)
    {
        StringBuilder sb = new StringBuilder();
        
        for (int i = 0; i < MAX_ROW; i++)
        {
            if(line >= list.Count)
            {
                break;
            }
            sb.AppendLine(list[line]);
            sb.AppendLine();
            line++;
        }
        return sb.ToString();
    }
}
