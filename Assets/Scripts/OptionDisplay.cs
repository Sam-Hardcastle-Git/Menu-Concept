using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionDisplay : MonoBehaviour
{
    public Option option;
    public Image myIcon;
    public GameObject highlightSquare;   

    public void toggleHighlight(bool toggle)
    {        
        highlightSquare.SetActive(toggle);
    }
}
