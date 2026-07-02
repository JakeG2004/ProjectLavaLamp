using UnityEngine;

public class FrequencyManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO startFrequencyInteract;
    [SerializeField] private VoidEventChannelSO stopInteract;
    [SerializeField] private GameObject offScreen;
    [SerializeField] private GameObject manipulatedFrequency;
    [SerializeField] private GameObject randomizedFrequency;
    private bool hasStartedFrequency;
    
    void Awake()
    {
        hasStartedFrequency = false;

        TurnScreenOff(true);
    }

    public void StartFrequencyInteraction()
    {
        if (hasStartedFrequency)
        {
            return;
        }

        hasStartedFrequency = true;
        //Turn on the two frequencies
        manipulatedFrequency.SetActive(true);
        randomizedFrequency.SetActive(true);

        if (startFrequencyInteract != null)
        {
            startFrequencyInteract.RaiseEvent();
        }
        TurnScreenOff(false);
    }

    public void TurnScreenOff(bool isActive)
    {
        offScreen.SetActive(isActive);
    }

    public void StopFrequencyInteraction()
    {
        AudioManager.Instance.PlaySound(MixerType.SFX, SoundType.MinigameComplete, 1f, transform.position);

        if (stopInteract != null)
        {
            stopInteract.RaiseEvent();
        }

        hasStartedFrequency = false;

        //Turn off the two frequencies
        manipulatedFrequency.SetActive(false);
        randomizedFrequency.SetActive(false);

        TurnScreenOff(true);

    }
}
