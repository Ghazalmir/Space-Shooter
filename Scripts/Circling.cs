using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circling : MonoBehaviour
{
    float timeCounter = 0f;
    [SerializeField] float speedOfSpin = 1f;
    [SerializeField] float radios = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter +=  Time.deltaTime * speedOfSpin;

        transform.position = Quaternion.AngleAxis(timeCounter, Vector3.forward) * new Vector3(radios, 0f);

        
    }
}
