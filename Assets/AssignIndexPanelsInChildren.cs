using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignIndexPanelsInChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        int i = 1;
        foreach (var panel in GetComponentsInChildren<IndexPanel>())
        {
            panel.GetComponentInChildren<Text>().text = i.ToString();
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
