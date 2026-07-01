using UnityEngine;

public class FrequencyManipulator : MonoBehaviour
{
    [SerializeField] private float defaultWidth;
    [SerializeField] private float defaultHeight;
    [SerializeField] private float defaultPos;

    void Awake()
    {
        //Set dimensions to the defaults
        gameObject.transform.localScale = new Vector3(defaultWidth, defaultHeight, transform.localScale.z);
        

        //Actually set position (Relative to camera)
        transform.localPosition = new Vector3(defaultPos, 0f, 0.6f);
    }


    // Maps 0-1 input to a 0.5-1.5 multiplier of the default height.
    public void OnHeightSignalReceived(float signalValue)
    {

        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f; 
        float scaleY = defaultHeight * multiplier;

        //Calculate and apply new Y scale
        gameObject.transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    //Maps 0-1 input to a 0.5-1.5 multiplier of the default width.
    public void OnWidthSignalReceived(float signalValue)
    {

        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f;
        float scaleX = defaultWidth * multiplier;

        gameObject.transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    public void OnPosSignalReceived(float signalValue)
    {
        gameObject.transform.localPosition = new Vector3(signalValue-0.5f, 0f, 0.7f);
    }
}
