using System;

public interface IAmmoHandler
{
    public event Action<int> OnAmmoChanged;
    public void AddAmmo(int ammoAmount);
    public void SubstractAmmo(int ammoAmount);
    public int GetAmmo();
}
