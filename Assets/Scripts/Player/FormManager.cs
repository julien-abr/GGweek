using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormManager 
{
    public FormType formType;
    public FormManager()
    {
        Debug.Log("Createdmanager");
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            formType = FormType.Bird;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            formType = FormType.Boar;
        }
    }

    public enum FormType
    {
        Human,
        Boar,
        Bird,
        Camel
    }
}
