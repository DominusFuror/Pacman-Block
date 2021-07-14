using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CodeInputSC : MonoBehaviour
{
    public Text text;
    public string Code;
    bool win = false;
    Color startColor;
    void Start()
    {

        Code = new StreamReader("CodeBlockConfig.txt").ReadToEnd();
        startColor = text.color;

    }




    // Update is called once per frame
    void Update()
    {
        if (!win)
        {
            if (text.text.Length <= 11)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0)) text.text += 0;
                if (Input.GetKeyDown(KeyCode.Alpha1)) text.text += 1;
                if (Input.GetKeyDown(KeyCode.Alpha2)) text.text += 2;
                if (Input.GetKeyDown(KeyCode.Alpha3)) text.text += 3;
                if (Input.GetKeyDown(KeyCode.Alpha4)) text.text += 4;
                if (Input.GetKeyDown(KeyCode.Alpha5)) text.text += 5;
                if (Input.GetKeyDown(KeyCode.Alpha6)) text.text += 6;
                if (Input.GetKeyDown(KeyCode.Alpha7)) text.text += 7;
                if (Input.GetKeyDown(KeyCode.Alpha8)) text.text += 8;
                if (Input.GetKeyDown(KeyCode.Alpha9)) text.text += 9;
            }
            if (Input.GetKeyDown(KeyCode.Backspace)) text.text = "";
            if (Input.GetKeyDown(KeyCode.Return)) OnEnter();


        
        }


    }



    public void OnEnter()
    {


        if (text.text == Code)
        {
            MainServerManager.SendMQTTMess("CodePhaseEnd");
            text.color = Color.green;
            win = true;
        }
        else
        {
            StartCoroutine(IncorrectEnter());
        }
    }

    IEnumerator IncorrectEnter()
    {
        text.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        text.color = startColor;
        yield return new WaitForSeconds(0.5f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        text.color = startColor;
        yield return new WaitForSeconds(0.5f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        text.color = startColor;

    }

}
