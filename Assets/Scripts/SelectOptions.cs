using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOptions : MonoBehaviour
{
    [SerializeField] protected int vMenu;
    [SerializeField] private List<RowGenerator> VerticalOptions = new List<RowGenerator>();
    [SerializeField] private Text primaryText, secondaryText, explanationText;
    [SerializeField] private GameObject quit;
    
    bool started = true;  

    void Update()
    {
        //Start with a row of icons
        if (started)
        {
            for (int i = 0; i < VerticalOptions.Count; i++)
            {
                if(i == 0)
                {
                    VerticalOptions[i].ToggleIcons(true);
                    started = false;
                }
            }
        }

        //Shift Up
        if (Input.GetKeyDown(KeyCode.W) && vMenu > 0)
        {
            HighlightQuit(false);
            Shift(-1);
            
        }

        //Shift Down
        if (Input.GetKeyDown(KeyCode.S) && vMenu < 4)
        {
            Shift(1);
            HighlightQuit(true);

        }

        //Exit menu
        if (Input.GetKeyDown(KeyCode.E) && vMenu == 4)
        {
            ExitMenu();
        }

        //Primary display text
        switch (vMenu)
        {
            case 0:
                changeText("Where is");                               

                break;

            case 1:
                changeText("Can I have");
                break;

            case 2:
                changeText("Take my");
                break;

            case 3:
                changeText("Take me to");
                break;

            case 4:
                changeText("Never Mind");
                break;
        }
    }       

    private void VerticalSelection(int currentlyHighlighted, bool toggle)
    {        
        for (int i = 0; i < VerticalOptions.Count; i++)
        {            
            if(i == currentlyHighlighted)
            {                         
                //show rows that are being used and hide rows that aren't
                VerticalOptions[i].ToggleIcons(toggle);
                VerticalOptions[i].highlighted = toggle;
                VerticalOptions[i].ResetMenu();
            }            
        }        
    }

    private void Shift(int amount)
    {        
        //Shift up and down
        VerticalSelection(vMenu, false);
        vMenu += amount;
        VerticalSelection(vMenu, true);        
    }

    private void changeText(string first)
    {
        primaryText.text = first;       
    }    

    private void HighlightQuit(bool toggle)
    {       
        if (vMenu == 4)
        {
            #region Change icon size
            if (toggle)
            {
                secondaryText.text = null;
                quit.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(115f, 115f);
            }
            else
            {
                quit.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100f, 100f);
            }
            #endregion

            quit.transform.GetChild(0).gameObject.SetActive(toggle);
        }
    }

    private void ExitMenu()
    {
        explanationText.text = "You left the menu";
    }
}
