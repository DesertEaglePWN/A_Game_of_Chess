using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    Vector3 CounterClockwise = new Vector3(0, -1, 0);
    Vector3 Clockwise = new Vector3(0, 1, 0);
    private float localZ;
    private float InputAxisValue;

    [SerializeField]
    private float minCameraZoom;

    [SerializeField]
    private float maxCameraZoom;
    
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(2) || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {
            InputAxisValue = Input.GetAxis("Mouse X");
            if (InputAxisValue > 0)
            {
                this.transform.parent.Rotate(CounterClockwise * Time.deltaTime * 50);

            }
            else if (InputAxisValue < 0)
            {
                this.transform.parent.Rotate(Clockwise * Time.deltaTime * 50);
            }
        
        }
        InputAxisValue = Input.GetAxis("Mouse ScrollWheel");
        if (InputAxisValue > 0) 
        {
            Debug.Log(InputAxisValue);
            localZ = this.transform.localPosition.z;
            if (Mathf.Abs(localZ) > minCameraZoom){
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y-1, this.transform.localPosition.z+1);
            }
        }
        else if (InputAxisValue < 0)
        {
            Debug.Log(InputAxisValue);
            localZ = this.transform.localPosition.z;
            if (Mathf.Abs(localZ) < maxCameraZoom)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y+1, this.transform.localPosition.z-1);
            }
        }
	}


}
