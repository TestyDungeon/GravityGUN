using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform mountPoint;
    [SerializeField] private Item[] slots;
    private int currentSlot = -1;


    void Awake()
    {

        EquipWeapon(0);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].player = gameObject;
            slots[i].cameraPivot = cameraPivot;
        }

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            // Number keys start at Alpha1 = "1"
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                EquipWeapon(i);
            }
        }
    }

    private void EquipWeapon(int slot)
    {
        if (slot == currentSlot || slot < 0 || slot > slots.Length)
            return;

        Vector3 pos = Vector3.zero;
        if (currentSlot >= 0)
        {
            pos = slots[currentSlot].transform.localPosition;
            slots[currentSlot].OnUnequip();
        }

        slots[slot].OnEquip();
        currentSlot = slot;
        slots[currentSlot].transform.localPosition = pos;
    }

    public Item GetCurrent()
    {
        return slots[currentSlot];
    }
}
