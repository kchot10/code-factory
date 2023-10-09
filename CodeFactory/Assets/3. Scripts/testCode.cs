using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class testCode : MonoBehaviour
{

    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;
    
    private Vector3[] wayPoints;


    // Start is called before the first frame update
    void Start()
    {
        wayPoints = new Vector3[4];
        wayPoints.SetValue(p1.position, 0);
        wayPoints.SetValue(p2.position, 1);
        wayPoints.SetValue(p3.position, 2);
        wayPoints.SetValue(p4.position, 3);

        transform.DOPath(wayPoints, 10.0f, PathType.CatmullRom).SetLookAt(0f).SetEase(Ease.Linear);
       //  transform.DOPath(wayPoints, 10.0f, PathType.CatmullRom).SetEase(Ease.Linear);
    }


}
