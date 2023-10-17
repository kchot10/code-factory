using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class LightManager : MonoBehaviour
{

    Color32 color;
    public GameObject clearCylinder;
    public GameObject failCylinder;
    Material clearMaterial;
    Material failMaterial;
    bool isAlpha;

    private void Start()
    {
        clearMaterial = clearCylinder.GetComponent<Renderer>().material;
        failMaterial = failCylinder.GetComponent<Renderer>().material;
        isAlpha = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isAlpha)
            {
                clearMaterial.color = new UnityEngine.Color(clearMaterial.color.r, clearMaterial.color.g, clearMaterial.color.b, 120/255);
                failMaterial.color = new UnityEngine.Color(failMaterial.color.r, failMaterial.color.g, failMaterial.color.b, 120/255);

                print("Alpha enable");
            }
            else
            {
                clearMaterial.color = new UnityEngine.Color(clearMaterial.color.r, clearMaterial.color.g, clearMaterial.color.b, 255/255);
                failMaterial.color = new UnityEngine.Color(failMaterial.color.r, failMaterial.color.g, failMaterial.color.b, 255/255);

                print("Alpha disable");

            }
            isAlpha = !isAlpha;
        }
    }


}
