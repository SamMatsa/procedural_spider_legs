using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] GameObject waterParent;
    [SerializeField] float timeUntilDelete = 1;
    private float timeInWater = 0;
    private bool countTimer = false;
    private Vector3 maxScaleVector;

    // Start is called before the first frame update
    void Start()
    {
        maxScaleVector = waterParent.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(countTimer);
        Debug.Log(timeInWater);
        if (timeInWater >= timeUntilDelete)
        {
            Destroy(waterParent);
        }
        if (countTimer)
        {
            //TODO Scale anpassen
            float percentWater = 1 - (timeInWater / timeUntilDelete);

            waterParent.transform.localScale = Vector3.Lerp(new Vector3(0,0,0), maxScaleVector, percentWater);

            timeInWater += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        countTimer = true;
    }

    void OnTriggerExit(Collider other)
    {
        countTimer = false;
    }
}
