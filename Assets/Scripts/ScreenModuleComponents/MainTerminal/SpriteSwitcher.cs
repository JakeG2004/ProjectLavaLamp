using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    //This is a modified Junction script with the spline and randomization removed.
    private SpriteRenderer spriteRenderer;

    [Tooltip("TrueSprite will be displayed first if isInTruePosition is checked.")]
    [SerializeField] private Sprite trueSprite;
    [SerializeField] private Sprite switchSprite;
    public bool isInTruePosition; //Keeps track of our state so we can alternate cleanly


    // Initialize
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if(isInTruePosition){
            spriteRenderer.sprite=trueSprite;
        }
        else{
            spriteRenderer.sprite=switchSprite;
        }
    }

    public void OnSwitch(){ //When the corresponding switch is hit (assign both switch up and switch down to call this)
        if(isInTruePosition){
            isInTruePosition = false;
            spriteRenderer.sprite=switchSprite;
            //Debug.Log("SwitchPos");
        }
        else{
            isInTruePosition = true;
            spriteRenderer.sprite=trueSprite;
            //Debug.Log("TruePos");
        }
    }
}
