using UnityEngine;

public class Rocket_Launcher : Gun
{

    [SerializeField] private GameObject rocket;

    private GameObject newRocket;


    protected override void Shoot()
    {
        newRocket = Instantiate(rocket, cameraPivot.position, cameraPivot.rotation);
        newRocket.GetComponent<Rocket>().rocketStart = cameraPivot;
        newRocket.GetComponent<Rocket>().damage = damage;
        newRocket.GetComponent<MeshRenderer>().enabled = false;
    }

}
