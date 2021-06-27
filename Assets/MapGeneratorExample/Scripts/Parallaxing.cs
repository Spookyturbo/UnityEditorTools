using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;  //The backgrounds to move with the camera
    private float[] parallaxScales;  //The speed to move relative to the camera per background (this is set by the backgrounds z)
    public float smoothing = 1f;  //Smooth the parralax effect. Must be >0

    private Transform camTransform;  //Caching
    private Vector3 previousCamPos;

    void Awake()
    {
        camTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initalize previousCamPos
        previousCamPos = camTransform.position;

        //Setup the parallaxScales
        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //The amount that the background will move
            float parallax = (previousCamPos.x - camTransform.position.x) * parallaxScales[i];

            //The x position the background will be moved to
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //The actual targeted position, only the x is changed
            Vector3 backgroundtargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //Smoothly move to new position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundtargetPos, smoothing * Time.deltaTime);
        }

        //Set previous camera position
        previousCamPos = camTransform.position;
    }
}
