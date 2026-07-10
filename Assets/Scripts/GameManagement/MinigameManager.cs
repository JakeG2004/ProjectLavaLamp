using UnityEngine;
using System.Collections;

public class MinigameManager : MonoBehaviour
{
    [Tooltip("Used for quick balancing.")]
    [SerializeField] int effLossPerSirenPerSecond;
    [SerializeField] int effLossMegaFail;
    [SerializeField] int effGainOnComplete;

    [Tooltip("The delay between sirens (excluding modifiers) depending on the day it is. 0 for none.")]
    [SerializeField] int[] DayExpectedTriggerWait;

    [SerializeField] int dayEnableMinigame1;
    [SerializeField] int dayEnableMinigame2;
    [SerializeField] int dayEnableMinigame3;
    [SerializeField] int dayEnableMinigame4;

    [Tooltip("An event channel for each siren.")]
	[SerializeField] private VoidEventChannelSO[] sirenChannels;

    [Tooltip("Signal for decreasing efficiency score:")]
	[SerializeField] private IntEventChannelSO efficiencySubtract;

    [Tooltip("Signal for increacing efficiency score:")]
	[SerializeField] private IntEventChannelSO efficiencyAdd;

    public static MinigameManager Instance = null;
    EmployeeData currentSession = null;

    // terminalSirensAreActive[0] is terminal 2, terminalSirensAreActive[1] is terminal 3...
    bool[] terminalSirensAreActive = {false,false,false,false};



    // Awake is called when object is made active
    void Awake()
    {
        // Singleton functionality.
        if (Instance == null)
        {
            Instance = GetComponent<MinigameManager>();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Make absolutely sure we aren't creating duplicate coroutines
        StopAllCoroutines();

        // Pull the data out of the Level Manager
        if (LevelManager.Instance != null && LevelManager.Instance.currentSession != null)
        {
            currentSession = LevelManager.Instance.currentSession;
            //Debug.Log($"Minigame pulled data for: {currentSession.employeeName} on day {currentSession.currentDay}.");
            //Check to make sure we aren't on day 1, and that we have a corresponding serializefield for the day
            if(currentSession.currentDay!=0 && 
                    DayExpectedTriggerWait.Length >= currentSession.currentDay)
            {
                StartCoroutine(DegradeEfficiencyRoutine());
                //Actually start the sequence, starting with the day's default timer
                Invoke("nextTriggerTimer", DayExpectedTriggerWait[currentSession.currentDay-1]);
            }       
        }
        else
        {
            Debug.LogError("Couldn't pull session data from levelmanager! Minigames will not be triggered.");
        }
    }

    void nextTriggerTimer()
    {
        if(sirenChannels!=null){ //Nullcheck
            //Randomly pick the next game we're going to alert
            //THIS WILL NEED TO ACCOUNT FOR THE DAY WE'RE ON TO EXCLUDE UNLOCKING GAMES EARLY
            int nextGame = 0;

            nextGame = findNextTriggerableMinigame();


            //Trigger corresponding event with sirenChannels[nextGame]
            if(sirenChannels.Length<=nextGame){
                Debug.LogWarning("No corresponding siren channel found! List too short!");
                return;
            }
            else if(nextGame==-1){
                //Debug.Log("No triggerable minigame found!");
            }
            else{
                sirenChannels[nextGame].RaiseEvent();
            }

            //Randomize with currentSession.efficiency and rand

            // Normalize efficiency score between max and min multiplier
            float effMultiplier = currentSession.efficiency * 0.0003f + 0.7f;
            //Debug.Log($"Current mult with efficiency is: {effMultiplier}");

            // Random modifier (-10 to 10 seconds added randomly)
            int rawRandom = UnityEngine.Random.Range(-10, 11);

            //Debug.Log($"Expected minigame timer is: {DayExpectedTriggerWait[currentSession.currentDay-1]*effMultiplier+rawRandom}");
            //Recursive invoke, multiply by efficiency, add random seconds
            Invoke("nextTriggerTimer", DayExpectedTriggerWait[currentSession.currentDay-1]*effMultiplier+rawRandom);
        }
        else{
            Debug.LogWarning("No siren channels found!");
        }
    }

    int findNextTriggerableMinigame(){

        if(!isAnyAvailableSirens()){
            efficiencySubtract.RaiseEvent(effLossMegaFail);
            //Debug.Log("PAIN AND SUFFERING UPON YE");
            return -1;
        }

        int range = 0;
        int currentChoice = -1;
        //Restrict minigame choice by current day
        if(currentSession.currentDay>=dayEnableMinigame1){
            range++;
        }
        if(currentSession.currentDay>=dayEnableMinigame2){
            range++;
        }
        if(currentSession.currentDay>=dayEnableMinigame3){
            range++;
        }
        if(currentSession.currentDay>=dayEnableMinigame4){
            range++;
        }
        //Reroll choice until landing on an inactive terminal
        while(currentChoice<0||terminalSirensAreActive[currentChoice]){
            currentChoice = UnityEngine.Random.Range(0, range); 
        }
        //Return our choice without duplicates and adhering to dayEnableMinigame scheme
        return currentChoice;
    }

    bool isAnyAvailableSirens(){
        bool isThere = false;
        //Check available sirens against current day to determine if a minigame is available in relation to the day
        if(currentSession.currentDay<=1){
            return false;
        }
        else if(currentSession.currentDay>=dayEnableMinigame1&&!terminalSirensAreActive[0]){
            isThere=true;
        }
        else if(currentSession.currentDay>=dayEnableMinigame2&&(!terminalSirensAreActive[0]||!terminalSirensAreActive[1])){
            isThere=true;
        }
        else if(currentSession.currentDay>=dayEnableMinigame3&&(!terminalSirensAreActive[0]||!terminalSirensAreActive[1]||!terminalSirensAreActive[2])){
            isThere=true;
        }
        else if(currentSession.currentDay>=dayEnableMinigame3&&(!terminalSirensAreActive[0]||!terminalSirensAreActive[1]||!terminalSirensAreActive[2]||!terminalSirensAreActive[3])){
            isThere=true;
        }


        return isThere;
    }

    IEnumerator DegradeEfficiencyRoutine()
    {
        //Set our delay
        var delay = new WaitForSeconds(1f); 
        
        //Yes, this is meant to be an infinite loop
        while(true)
        {
            //This stops it from crashing things, making sure it only happens once per second.
            yield return delay;
            
            int totalActiveSirens = 0;
            
            //Count amount of active sirens
            for(int i = 0; i < terminalSirensAreActive.Length; i++)
            {
                if(terminalSirensAreActive[i])
                {
                    totalActiveSirens++;
                }
            }
            
            // Send signal with amount of sirens active to subtract each second.
            if(totalActiveSirens > 0)
            {
                efficiencySubtract.RaiseEvent(totalActiveSirens*effLossPerSirenPerSecond);
                //Debug.Log($"Tried to send signal!: {totalActiveSirens*effLossPerSirenPerSecond}");
            }
        }
    }

    //How we track when our own signals are sent out to start a minigame siren
    public void siren2TurnedOn(){
        terminalSirensAreActive[0] = true;
    }
    public void siren3TurnedOn(){
        terminalSirensAreActive[1] = true;
    }
    public void siren4TurnedOn(){
        terminalSirensAreActive[2] = true;
    }
    public void siren5TurnedOn(){
        terminalSirensAreActive[3] = true;
    }

    //How we track when minigames have been completed
    public void siren2TurnedOff(){
        if(terminalSirensAreActive[0])
        {
            efficiencyAdd.RaiseEvent(effGainOnComplete);
            terminalSirensAreActive[0] = false;
        }
    }
    public void siren3TurnedOff(){
        if(terminalSirensAreActive[1])
        {
            efficiencyAdd.RaiseEvent(effGainOnComplete);
            terminalSirensAreActive[1] = false;
        }
    }
    public void siren4TurnedOff(){
        if(terminalSirensAreActive[2]){
            efficiencyAdd.RaiseEvent(effGainOnComplete);
            terminalSirensAreActive[2] = false;
        }
    }
    public void siren5TurnedOff(){
        if(terminalSirensAreActive[3]){
            efficiencyAdd.RaiseEvent(effGainOnComplete);
            terminalSirensAreActive[3] = false;
        }
    }

}
