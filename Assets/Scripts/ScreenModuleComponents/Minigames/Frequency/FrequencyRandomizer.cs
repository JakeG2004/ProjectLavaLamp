using UnityEngine;

public class FrequencyRandomizer : MonoBehaviour
{
    [Tooltip("THESE SHOULD ALWAYS MATCH THE ASSOCIATED ManipulatedFrequency")]
    [SerializeField] private float defaultWidth;
    [SerializeField] private float defaultHeight;
    [SerializeField] private float defaultPos;
    private float randWidthMult;
    private float randHeightMult;
    private float randPosOffset;
    private bool isWidthClose;
    private bool isHeightClose;
    private bool isPosClose;
    [SerializeField] private float tolerance;
    [SerializeField] VoidEventChannelSO frequencyMatched;
    [SerializeField] VoidEventChannelSO stopSiren;

    void Awake()
    {
        isWidthClose = false;
        isHeightClose = false;
        isPosClose = false;

        //Set dimensions to random
        randWidthMult = Random.Range(0.5f, 1.5f);
        //Checks to see if random value is within tolerance range of one
        //If it is, this means it would start as acceptable, and therefore cause issues, 
        //so we reroll it.
        while(Mathf.Abs(randWidthMult-1)<=tolerance)
        {
            randWidthMult = Random.Range(0.5f, 1.5f); 
        }

        //Do the Same for Height
        randHeightMult = Random.Range(0.5f, 1.5f);
        while(Mathf.Abs(randHeightMult-1)<=tolerance)
        {
            randHeightMult = Random.Range(0.5f, 1.5f); 
        }
        //And position
        randPosOffset = Random.Range(-0.5f, 0.5f); 
        while(Mathf.Abs(randPosOffset)<=tolerance)
        {
            randPosOffset = Random.Range(-0.5f, 0.5f); 
        }
        
        Debug.Log($"Width multiplier is: {randWidthMult}");
        Debug.Log($"Height multiplier is: {randHeightMult}");
        Debug.Log($"Pos offset is: {randPosOffset}");


        gameObject.transform.localScale = new Vector3(defaultWidth*randWidthMult, defaultHeight*randHeightMult, transform.localScale.z);

        transform.localPosition = new Vector3(randPosOffset, 0f, 0.7f);
        //Actually set position (Will need to be readded)
        //transform.position = new Vector3(defaultPos, transform.position.y, transform.position.z);
    }


    void Update()
    {
        if(isWidthClose&&isHeightClose&&isPosClose){
            Debug.Log("WIN WIN WIN");
            //.RaiseEvent();
        }
    }


    // Maps 0-1 input to a 0.5-1.5 multiplier of the default height.
    public void OnHeightSignalReceived(float signalValue)
    {
        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f;

        if(Mathf.Abs(randHeightMult-multiplier)<=tolerance)
        {
            isHeightClose = true;
            Debug.Log("Height is close!");
        }
        else
        {
            isHeightClose = false;
            //Debug.Log($"Is {Mathf.Abs(randHeightMult-multiplier)} lower than {tolerance}? No!");
        }
    }
    

    //Stores signal from sliders to compare against internal random modification
    public void OnWidthSignalReceived(float signalValue)
    {
        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f;

        if(Mathf.Abs(randWidthMult-multiplier)<=tolerance)
        {
            isWidthClose = true;
            Debug.Log("Width is close!");
        }
        else
        {
            isWidthClose = false;
        }
    }

    //Stores signal from sliders to compare against internal random modification
    public void OnPosSignalReceived(float signalValue)
    {

        if(Mathf.Abs(randPosOffset-(signalValue-0.5f))<=tolerance)
        {
            isPosClose = true;
            Debug.Log("Pos is close!");
        }
        else
        {
            isPosClose = false;
            Debug.Log($"Is {Mathf.Abs(randPosOffset-(signalValue-0.5f))} lower than {tolerance}? No!");
        }
    }




}
