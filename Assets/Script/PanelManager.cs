using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance {get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePanel(string panelTag)
    {
        GameObject.FindGameObjectWithTag(panelTag).GetComponent<CanvasGroup>().alpha = 1f;
        GameObject.FindGameObjectWithTag(panelTag).GetComponent<CanvasGroup>().interactable = true;
    }

    public void DisablePanel(string panelTag)
    {
        GameObject.FindGameObjectWithTag(panelTag).GetComponent<CanvasGroup>().alpha = 0f;
        GameObject.FindGameObjectWithTag(panelTag).GetComponent<CanvasGroup>().interactable = false;
    }
}
