using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacementConditional : MonoBehaviour
{
	[SerializeField] private GameObject[] placementNodes; //the nodes to be activated/deactivated based on the presence of the conditionalColliders
	[SerializeField] private GameObject[] conditionalParts;
	private GameObject[] conditionalColliders; //the colliders required to trigger the conditional;
    [SerializeField] private bool isActivation; //whether we are activating the placementNodes on condition or not
	[SerializeField] private bool recursionMode; //for when the conditional is for linearly recursive connecting pieces
	[SerializeField] private string recursionPartName; //Name of the linearly recursive part
	private bool conditionalComplete;
	void Awake()
    {
		conditionalComplete = false;
		if(isActivation == true)
		{
			for(int i = 0; i < placementNodes.Length; i++)
			{
				placementNodes[i].SetActive(false);
			}
		}
		conditionalColliders = new GameObject[conditionalParts.Length];
		for(int i = 0; i < conditionalParts.Length; i++)
		{
			string removeThis = "(Clone)";
			string colliderName = conditionalParts[i].name + "Collider";
			colliderName = colliderName.Replace(removeThis, "");
			Transform collider = conditionalParts[i].transform.Find(colliderName);
			conditionalColliders[i] = collider.gameObject;
		}
    }

	public void placementConditionalCheck()
	{
		if(conditionalComplete == false)
		{
			if(recursionMode == true)
			{
				string placementName = recursionPartName + "PlacementNode";
				if(transform.Find(placementName) != null)
				{
					Transform child = transform.Find(placementName);
					placementNodes[0] = child.gameObject;
				}
			}
			LinkedList<GameObject> usedColliders = new LinkedList<GameObject>();
			LinkedList<GameObject> usedConditional = new LinkedList<GameObject>();
			
			int successCondition = conditionalColliders.Length;
			int currentCondition = 0;
			foreach(Transform child in transform)
			{
				//check if child is already used by a condition
				bool usedChild = false;
				LinkedListNode<GameObject> current = usedColliders.First;
				while(current != null)
				{
					if(current.Value.transform == child)
					{
						usedChild = true;
						break;
					}
					current = current.Next;
				}
				if(usedChild == true)
				{
					continue;
				}
				
				//check if child matches a condition
				for(int i = 0; i < conditionalColliders.Length; i++)
				{
					bool usedCondition = false;
					LinkedListNode<GameObject> currentConditional = usedConditional.First;
					while(currentConditional != null)
					{
						if(currentConditional.Value.transform == conditionalColliders[i])
						{
							usedCondition = true;
							break;
						}
						currentConditional = currentConditional.Next;
					}
					if(usedCondition == true)
					{
						continue;
					}
					if(child.name == conditionalColliders[i].name)
					{
						usedColliders.AddLast(child.gameObject);
						usedConditional.AddLast(conditionalColliders[i]);
						currentCondition++;
						break;
					}
				}

				//check if all conditions are met
				if(successCondition == currentCondition)
				{
					placementConditionalSuccess();
				}
			}
		}
	}
	
	private void placementConditionalSuccess()
	{
		if(isActivation == true)
		{
			for(int i = 0; i < placementNodes.Length; i++)
			{
				placementNodes[i].SetActive(true);
			}
		}
		else
		{
			for(int i = 0; i < placementNodes.Length; i++)
			{
				if(placementNodes[i] != null)
				{
					Destroy(placementNodes[i]);
				}
			}
		}
		conditionalComplete = true;
	}
}
