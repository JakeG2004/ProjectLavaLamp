using UnityEngine;

public class FrequencyManipulator : MonoBehaviour
{
    [SerializeField] private float defaultWidth;
    [SerializeField] private float defaultHeight;
    [SerializeField] private float defaultPos;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        //Grab Spriterenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite != null)
        {
            //Calculate scale required to hit your exact width/height
            Bounds spriteBounds = spriteRenderer.sprite.bounds;
            float scaleX = defaultWidth / spriteBounds.size.x;
            float scaleY = defaultHeight / spriteBounds.size.y;

            //Apply the scale to the transform
            transform.localScale = new Vector3(scaleX, scaleY, 1f);
        }

        //Actually set position
        //transform.position = new Vector3(defaultPos, transform.position.y, transform.position.z);
    }


    // Maps 0-1 input to a 0.5-1.5 multiplier of the default height.
    public virtual void OnHeightSignalReceived(float signalValue)
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null) return;

        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f; 
        float targetHeight = defaultHeight * multiplier;

        //Calculate and apply new Y scale
        float scaleY = targetHeight / spriteRenderer.sprite.bounds.size.y;
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    //Maps 0-1 input to a 0.5-1.5 multiplier of the default width.
    public virtual void OnWidthSignalReceived(float signalValue)
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null) return;

        //Map 0-1 to 0.5-1.5 range
        float multiplier = signalValue + 0.5f;
        float targetWidth = defaultWidth * multiplier;

        //Calculate and apply new X scale
        float scaleX = targetWidth / spriteRenderer.sprite.bounds.size.x;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}
