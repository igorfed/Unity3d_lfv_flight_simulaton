using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_on_Off : MonoBehaviour
{
    public KeyCode pressM;
    public GameObject Panel;
    public Text text;

    private const string show_text = "to Show Info Panel Press [M]";
    private const string hide_text = "to Hide Info Panel Press [M]";


    [Header("Hot Keys")]
    public KeyCode pressI;
    public GameObject Panel_HotKeys;
    
    public Text text_Hot_Keys;
    private const string show_text_hot_keys = "to Show Hot Keys Panel Press [I]";
    private const string hide_text_hot_keys = "to Hide Hot Keys Panel Press [I]";

    void Start()
    {
        text.color = Color.blue;
        text_Hot_Keys.color = Color.blue;
        if (Panel != null)
        {
            Panel.SetActive(false);
            text.text = show_text;
            text.color = Color.red;
        }
        if (Panel_HotKeys != null)
        {
            Panel_HotKeys.SetActive(false);
            text_Hot_Keys.text = show_text_hot_keys;
            text_Hot_Keys.color = Color.red;
        }


    }

    // Update is called once per frame
    void Update()
    {
        InfoPanel();
        HotKeysPanel();
    }

    void InfoPanel()
    {
        bool isActive = Panel.activeSelf;
        if (Input.GetKeyDown(pressM))
        {
            Panel.SetActive(!isActive);
            //print("M pressed :");
            //print(Panel.activeSelf);
        }
        if (!isActive)
        {
            text.text = show_text;
            text.color = Color.red;
        }
        else
        {
            text.text = hide_text;
            text.color = Color.blue;
        }

    }
    void HotKeysPanel()
    {
        bool isActive = Panel_HotKeys.activeSelf;
        if (Input.GetKeyDown(pressI))
        {
           Panel_HotKeys.SetActive(!isActive);
            //print("M pressed :");
            //print(Panel.activeSelf);
        }
        if (!isActive)
        {
            text_Hot_Keys.text = show_text_hot_keys;
            text_Hot_Keys.color = Color.red;
        }
        else
        {
            text_Hot_Keys.text = hide_text_hot_keys;
            text_Hot_Keys.color = Color.blue;
        }

    }



}

