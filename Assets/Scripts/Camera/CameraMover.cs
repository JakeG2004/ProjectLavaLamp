using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO cameraMove;
    [SerializeField] private bool direction;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")))
        {
            cameraMove.RaiseEvent(direction);
        }
    }

}