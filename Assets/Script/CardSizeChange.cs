using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSizeChange : MonoBehaviour {

    private int CardListSize; 
    private int WidthHandHolder;
    private int HeightHandHolder; 
    private float HorizontalLayoutGroupWidth;
    private float HorizontalLayoutGroupSpacing;
    private float CardWidth;
    private float totalsize; 

	// Use this for initialization
	void Start () {
		
	}

    void ControlSizeWidth()
    {   //Width: 82 * 7 = 574; Height: 82 * 6 = 492
        WidthHandHolder = 82 * 7;
        HeightHandHolder = 82 * 6;

    }
	
	// Update is called once per frame
	void Update () {
        totalsize = (HorizontalLayoutGroupSpacing + CardWidth) * CardListSize;

        if(totalsize > 500)
        {   //Create Method for ControlSizeWidth
            ControlSizeWidth(); 
        }
        else
        {   //Create Method for ControlSizeWidth
            ControlSizeWidth().off;
            reset WidthHandHolder; 
        }
	}
}
