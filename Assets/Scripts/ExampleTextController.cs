using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ExampleTextController : MonoBehaviour
{

    //the number of line ExampleText can show at once.
    const int MAX_ROW = 4;
    
    //UI Component to show example text.
    Text exampleText;
    
  
    void Start()
    {
        this.exampleText = gameObject.GetComponent<Text>();
    }



    public void SetText(List<string> list,int line)
    {
        this.exampleText.text = ArrangeText(list,line);
        
    }


    //arrange example text from list to show
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

            //emplty line is added to see easily
            sb.AppendLine();


            line++;
        }
        return sb.ToString();
    }
}
