using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public Transform cameraPivot;
    public string itemName;
    
    
    public void OnEquip()
    {
        gameObject.SetActive(true);
    }

    public void OnUnequip()
    {
        gameObject.SetActive(false);
    }
}
