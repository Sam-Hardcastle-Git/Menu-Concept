using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class RowGenerator : MonoBehaviour
    {        
        [SerializeField] private GameObject unit;
        [SerializeField] private int hMenu;
        [SerializeField] private Text secondaryText, selectingText;
        [SerializeField] private string rowExplanation;
        [SerializeField] private List<Option> unitEntries = new List<Option>();
        [SerializeField] private List<GameObject> unitObjects = new List<GameObject>();
        private Vector3 previousCubePosition;
        public bool highlighted, started;


    private void Start()
    {
        previousCubePosition = transform.position;
        makeUnits();        
    }

    private void Update()
    {        
        //Set text and highlight box when starting
        if(started && highlighted)
        {
            HorizontalSelection(hMenu, true);
            changeText(hMenu);
            started = false;
        }               

        //Shift right
        if (Input.GetKeyDown(KeyCode.D) && highlighted && hMenu < unitEntries.Count - 1)
        {
            Shift(1);            
        }

        //Shift left
        if (Input.GetKeyDown(KeyCode.A) && highlighted && hMenu > 0)
        {
            Shift(-1);            
        }

        //Select
        if (Input.GetKeyDown(KeyCode.E) && highlighted)
        {            
            displayExplanation(rowExplanation, unitEntries[hMenu].name);
        }
    }

    private void makeUnits()
    {
        for (int i = 0; i < unitEntries.Count; i++)
        {
            //create a unit
            previousCubePosition = previousCubePosition + new Vector3(110, 0, 0);
            GameObject unitInstance = Instantiate(unit, previousCubePosition, Quaternion.identity);

            //child the unit to the canvas so the image display
            unitInstance.transform.SetParent(this.transform);

            //display units icon
            unitInstance.GetComponent<OptionDisplay>().myIcon.sprite = unitEntries[i].icon;

            //add to list of unit objects
            unitObjects.Add(unitInstance);

            //hide units until they are needed
            unitInstance.SetActive(false);            
        }                
    }

    public void ToggleIcons(bool toggle)
    {        
        foreach (GameObject g in unitObjects)
        {
            g.SetActive(toggle);
            highlighted = toggle;
        }
    }

    public void HorizontalSelection(int currentlyHighlighted, bool toggle)
    {       
        for (int i = 0; i < unitObjects.Count; i++)
        {            
            if (i == currentlyHighlighted)
            {              
                //Show that the option is highlighted
                OptionDisplay op = unitObjects[i].GetComponent<OptionDisplay>();
                op.toggleHighlight(toggle);

                #region expand/shrink icon
                if (!toggle)
                {
                    unitObjects[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100f, 100f);
                }
                else
                {
                    unitObjects[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(115f, 115f);
                }
                #endregion
            }
        }
    }      
    
    public void ResetMenu()
    {
        //Set the row selection back to the first entry when switching rows
        HorizontalSelection(hMenu, false);
        hMenu = 0;
        HorizontalSelection(hMenu, true);
        changeText(hMenu);
        
    }


    private void changeText(int entry)
    {
        secondaryText.text = unitEntries[entry].name;        
    }

    public void displayExplanation(string verticalOption, string horizontalOption)
    {        
        selectingText.text = ("You " +  verticalOption + " " + horizontalOption);
    }

    private void Shift(int amount)
    {
        //Shift left or right
        HorizontalSelection(hMenu, false);
        hMenu += amount;
        HorizontalSelection(hMenu, true);
        changeText(hMenu);
    }

   
}


