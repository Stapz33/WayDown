using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evenement_Interaction : MonoBehaviour
{

    public GameObject infos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        infos.SetActive(true);
    }

    void OnMouseExit()
    {
        infos.SetActive(false);
    }
}
