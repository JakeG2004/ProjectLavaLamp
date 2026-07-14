using UnityEngine;

/// <summary>
/// General event channel that broadcasts and carries choice payload.
/// </summary>
[CreateAssetMenu(menuName = "Events/Choice Event Channel", fileName = "ChoiceEventChannel")]
public class ChoiceEventChannelSO : GenericEventChannelSO<choice>
{
}
