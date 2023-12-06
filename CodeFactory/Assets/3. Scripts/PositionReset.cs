using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReset : MonoBehaviour
{
    [SerializeField] private GameObject[] resetObjects;
    private Vector3[] StagePositions;
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private int _step2Count = 0;

    [SerializeField] private ParticleSystem resetButtonFX;


    void Start()
    {
        initialPositions = new Vector3[resetObjects.Length];
        initialRotations = new Quaternion[resetObjects.Length];
        for (int i = 0; i < resetObjects.Length; i++)
        {
            initialPositions[i] = resetObjects[i].transform.localPosition;
            initialRotations[i] = resetObjects[i].transform.localRotation;

        }
    }

    public void Process()
    {
        resetButtonFX.Play();
        for (int i = 0; i < resetObjects.Length; i++)
        {
            resetObjects[i].transform.localPosition = initialPositions[i];
            resetObjects[i].transform.localRotation = initialRotations[i];
        }
    }
}
