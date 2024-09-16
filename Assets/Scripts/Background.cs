using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public enum dayTime {  AM, PM, NIGHT };
    public dayTime now;

    public Material amImage;
    public Material pmImage;
    public Material nightImage;
    MeshRenderer _mesh;
    // Start is called before the first frame update
    private void Awake()
    {
        now = dayTime.AM;
    }

    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBackgroundImage();
    }

    void ChangeBackgroundImage()
    {
        switch (now)
        {
            case dayTime.AM:
                _mesh.material = amImage;
                break;
            case dayTime.PM:
                _mesh.material = pmImage;
                break;
            case dayTime.NIGHT:
                _mesh.material = nightImage;
                break;
        }

    }
}
