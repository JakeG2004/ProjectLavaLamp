using UnityEngine;

public class FrequencyRandomizer : MonoBehaviour
{
    [SerializeField] private float defaultWidth;
    [SerializeField] private float defaultHeight;
    [SerializeField] private float defaultPos;

    void Awake()
    {
        //Set dimensions to the defaults
        gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        //Actually set position (Will need to be readded)
        //transform.position = new Vector3(defaultPos, transform.position.y, transform.position.z);
    }


    // Maps 0-1 input to a 0.5-1.5 multiplier of the default height.
    public virtual void OnHeightSignalReceived(float signalValue)
    {

        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f; 
        float scaleY = defaultHeight * multiplier;

        //Calculate and apply new Y scale
        gameObject.transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    //Maps 0-1 input to a 0.5-1.5 multiplier of the default width.
    public virtual void OnWidthSignalReceived(float signalValue)
    {

        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f;
        float scaleX = defaultWidth * multiplier;

        gameObject.transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}
