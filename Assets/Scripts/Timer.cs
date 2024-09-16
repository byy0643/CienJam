using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeMax;
    public float timeLimit;
    public float normalAngle;
    public int minuteAngle; // 1, 2, 3, 4, 5, 6, 7, 8, 9

    public Transform hour;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        timeLimit = timeMax = 90f;
        normalAngle = timeMax / 10f;
        hour.transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("Timer Start");
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;
        minuteAngle = (int)(timeLimit / normalAngle);
        hour.transform.rotation = Quaternion.Euler(0, 0, -(9 - minuteAngle) * 30f);
    }


}
