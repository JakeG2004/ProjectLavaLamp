using UnityEngine;
using TMPro;

public class EfficiencyMonitor : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO lostGame;
    private int hopper = 0;
    private int transferRate = 1;
    private TMP_Text efficiencyDisplay;
    private bool lockOut;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        lockOut = false;
        //Get our child TMP object so we can edit its display live
        efficiencyDisplay = GetComponentInChildren<TextMeshProUGUI>();

        // Check for null manager
        if (LevelManager.Instance != null)
        {

        }
        else
        {
            Debug.LogError("Couldn't pull session data from levelmanager!");
        }

        efficiencyDisplay.text = LevelManager.Instance.currentSession.efficiency.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //Check to make sure efficiency is above 0
        if(LevelManager.Instance.currentSession.efficiency<=0)
        {
            lostGame.RaiseEvent();
        }

        //Make sure we don't touch anything if we are locked out by a level ended state
        if(lockOut)
        {
            return;
        }

        //Set transfer rate based on how large the hopper is, so that we can accelerate it when the difference is large.
        if(Mathf.Abs(hopper)>=100)
        {
            transferRate = 3;
        }
        else if(Mathf.Abs(hopper)>=50)
        {
            transferRate = 2;
        }
        else
        {
            transferRate = 1;
        }

        if(hopper<0)
        {
            //Debug.Log($"Tried to lower {LevelManager.Instance.currentSession.efficiency} by {transferRate}");
            LevelManager.Instance.currentSession.efficiency-=transferRate;
            hopper+=transferRate;
            //Debug.Log($"New efficiency is {LevelManager.Instance.currentSession.efficiency}");
        }
        else if(hopper>0)
        {
            //Debug.Log($"Tried to raise {LevelManager.Instance.currentSession.efficiency} by {transferRate}");
            LevelManager.Instance.currentSession.efficiency+=transferRate;
            hopper-=transferRate;
            //Debug.Log($"New efficiency is {LevelManager.Instance.currentSession.efficiency}");
        }


        efficiencyDisplay.text = LevelManager.Instance.currentSession.efficiency.ToString();

    }

    public void IncreaseEfficiencyScore(int additional)
    {
        //Make sure we don't go above 1000 efficiency
        if(!(LevelManager.Instance.currentSession.efficiency+hopper>=1000))
        {
            hopper+=additional;
        }
        else
        {
            hopper = 1000 - LevelManager.Instance.currentSession.efficiency;
        }
        //Debug.Log($"Received increment: {additional}");
    }

    public void DecreaseEfficiencyScore(int decrement)
    {
        //Debug.Log($"Received decrement: {decrement}");
        hopper-=decrement;
    }

    public void LockOut()
    {
        lockOut = true;
    }
}
