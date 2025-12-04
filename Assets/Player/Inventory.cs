using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> OnSlotChanged;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform mountPoint;
    [SerializeField] private Item[] slots;
    [SerializeField] private Item[] alwaysOnSlots;
    private int currentSlot = -1;


    void Awake()
    {

        EquipWeapon(0);
        OnSlotChanged?.Invoke(slots[currentSlot]);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].player = gameObject;
            slots[i].cameraPivot = cameraPivot;
        }

        for (int i = 0; i < alwaysOnSlots.Length; i++)
        {
            alwaysOnSlots[i].player = gameObject;
            alwaysOnSlots[i].cameraPivot = cameraPivot;
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
        OnSlotChanged?.Invoke(slots[currentSlot]);
    }

    public Item GetCurrent()
    {
        return slots[currentSlot];
    }
}
