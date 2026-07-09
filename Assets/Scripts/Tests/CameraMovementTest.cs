using UnityEngine;

public class CameraMovementTest : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO boolSignal;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            boolSignal?.RaiseEvent(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            boolSignal?.RaiseEvent(false);
        }
    }
}