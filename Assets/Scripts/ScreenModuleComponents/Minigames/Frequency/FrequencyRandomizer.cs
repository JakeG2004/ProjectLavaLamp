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
        /*while(Mathf.Abs(randWidthMult))
        {
            //OIUGSIOSUDGIOUHGIOUSDHGIUSDHGISUODHGIOUSDGHIDSUGHDIUOGHDSIOGUSDGHIGUODS
        }*/
        randHeightMult = Random.Range(0.5f, 1.5f);
        //randPosOffset = Random.Range(0.5f, 1.5f);  TBD
        
        Debug.Log($"Width multiplier is: {randWidthMult}");
        Debug.Log($"Height multiplier is: {randHeightMult}");


        gameObject.transform.localScale = new Vector3(defaultWidth*randWidthMult, defaultHeight*randHeightMult, transform.localScale.z);

        //Actually set position (Will need to be readded)
        //transform.position = new Vector3(defaultPos, transform.position.y, transform.position.z);
    }


    void Update()
    {
        if(isWidthClose&&isHeightClose&&isPosClose){
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
        }
        else
        {
            isHeightClose = false;
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
        }
        else
        {
            isWidthClose = false;
        }
    }




}
