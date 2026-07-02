using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FrequencySlider : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO sliding;
    [SerializeField] private Camera moduleCamera;
    [SerializeField] [Range(0f, 1f)] private float startingValue;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    private bool isBeingControlled;
    private bool isActive;
    private Vector2 screenTop;
    private Vector2 screenBottom;
    private float currentValue;
    private float mouseValueOffset;
    //These attributes are unique to FrequencySlider
    [SerializeField] private float returnDuration = 0.5f;
    private Coroutine returnCoroutine;
    [SerializeField] private float driftSpeed = 0.1f;
    private Coroutine driftCoroutine;
    private bool driftEnabled;   // stays true across grabs, only cleared by reset
    [SerializeField] private FrequencyRandomizer randomizedFrequency;
    [SerializeField] private AssociatedValue associatedValue;

    public enum AssociatedValue 
    { 
        Period,
        Amplitude ,
        Offset 
    }

    void Awake()
    {
        currentValue = startingValue;
        SetPosition(currentValue);

        screenTop = moduleCamera.WorldToScreenPoint(top.position);
        screenBottom = moduleCamera.WorldToScreenPoint(bottom.position);
    }

    void OnMouseDown()
    {
        isBeingControlled = true;
        PauseDriftCoroutine(); // pause only — driftEnabled stays true
        mouseValueOffset = GetMouseValue() - currentValue;
        StartCoroutine(TakeUserInput());
    }
    
    void OnMouseUp()
    {
        isBeingControlled = false;
        ResumeDriftCoroutine();
    }
    
    public void OnModuleInteract()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    
    public void OnModuleStopInteract()
    {
        isBeingControlled = false;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    
    private IEnumerator TakeUserInput()
    {
        while (isBeingControlled)
        {
            Slide();
            yield return null;
        }
    }
    
    private void Slide()
    {
        currentValue = Mathf.Clamp(GetMouseValue() - mouseValueOffset, 0f, 1f);
        SetPosition(currentValue);
        
        if (sliding != null)
        {
            sliding.RaiseEvent(currentValue);
        }
    }
    
    private float GetMouseValue()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        float dotProduct = Vector2.Dot(screenTop - screenBottom, mousePosition - screenBottom);
        return dotProduct / (screenTop - screenBottom).sqrMagnitude;
    }
    
    private void SetPosition(float newValue)
    {
        transform.position = (top.position - bottom.position) * newValue + bottom.position;
    }

    public void ReturnToStart()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        returnCoroutine = StartCoroutine(ReturnToStartRoutine());
    }

    private IEnumerator ReturnToStartRoutine()
    {
        isBeingControlled = false; // make sure input coroutine isn't fighting this one
        StopDrift(); //Make sure drift isn't interfering, and prime it for restart next time the minigame begins


        float elapsed = 0f;
        float startValue = currentValue;

        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / returnDuration);
            t = Mathf.SmoothStep(0f, 1f, t); // ease in/out, feels nicer than linear

            currentValue = Mathf.Lerp(startValue, startingValue, t);
            SetPosition(currentValue);

            if (sliding != null)
            {
                sliding.RaiseEvent(currentValue);
            }

            yield return null;
        }

        currentValue = startingValue;
        SetPosition(currentValue);
        if (sliding != null)
        {
            sliding.RaiseEvent(currentValue);
        }

        returnCoroutine = null;
    }

    public void StartDrift()
    {
        driftEnabled = true;
        ResumeDriftCoroutine();
    }

    public void StopDrift()
    {
        driftEnabled = false;
        PauseDriftCoroutine();
    }

    private void PauseDriftCoroutine()
    {
        if (driftCoroutine != null)
        {
            StopCoroutine(driftCoroutine);
            driftCoroutine = null;
        }
    }

    private void ResumeDriftCoroutine()
    {
        if (!driftEnabled || isBeingControlled)
        {
            return; // don't resume if disabled entirely, or currently held
        }
        PauseDriftCoroutine(); // avoid double-running
        driftCoroutine = StartCoroutine(DriftRoutine());
    }

    private IEnumerator DriftRoutine()
    {
        while (driftEnabled && !isBeingControlled)
        {
            float direction = currentValue >= GetTargetValue() ? 1f : -1f;
            if (Mathf.Approximately(currentValue, GetTargetValue()))
            {
                direction = 1f;
            }

            currentValue = Mathf.Clamp01(currentValue + direction * driftSpeed * Time.deltaTime);
            SetPosition(currentValue);

            if (sliding != null)
            {
                sliding.RaiseEvent(currentValue);
            }

            if ((currentValue <= 0f && direction < 0f) || (currentValue >= 1f && direction > 0f))
            {
                break; // hit a rail, nothing more to do
            }

            yield return null;
        }

        driftCoroutine = null;
    }

    //Used to get the value corresponding to what this slider is associated with
    private float GetTargetValue()
    {
        switch (associatedValue)
        {
            case AssociatedValue.Period:
                return randomizedFrequency.getPeriod();
            case AssociatedValue.Amplitude:
                return randomizedFrequency.getAmplitude();
            case AssociatedValue.Offset:
                return randomizedFrequency.getOffset();
            default:
                Debug.LogWarning("Unhandled WaveParameter, defaulting to 0");
                return 0f;
        }
    }

}
