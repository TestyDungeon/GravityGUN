using TMPro;
using UnityEngine;

public class AmmoIndicatorUI : MonoBehaviour
{
    private Inventory inventory;
    private IAmmoHandler ammoHandler;
    [SerializeField] private TextMeshProUGUI text;

    void Awake()
    {
        if (inventory == null)
            inventory = FindAnyObjectByType<Inventory>();

    }

    void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnSlotChanged += OnSlotChanged;
            Debug.Log("SUBBED INV");
        }
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnSlotChanged -= OnSlotChanged;

        if (ammoHandler != null)
            ammoHandler.OnAmmoChanged -= OnAmmoChanged;
    }

    private void OnSlotChanged(Item item)
    {
        if (ammoHandler != null)
            ammoHandler.OnAmmoChanged -= OnAmmoChanged;

        if (item is IAmmoHandler handler)
        {
            ammoHandler = handler;
            ammoHandler.OnAmmoChanged += OnAmmoChanged;
            text.SetText(ammoHandler.GetAmmo().ToString());
        }
        else
        {
            ammoHandler = null;
            text.SetText("");
        }
        Debug.Log("Slot Changed");
    }

    private void OnAmmoChanged(int ammo)
    {
        text.SetText(ammo.ToString());
    }
}
