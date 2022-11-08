using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class downloadListScript : MonoBehaviour
{
    string path = Application.persistentDataPath + "/downlog.txt";
    public GameObject btnTemplate;
    GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<5;i++){
            g = Instantiate(btnTemplate, transform);
            //g.transform.GetChild(1).GetComponent<TMPro>
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
