using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    private GameObject player;

    public static InteractionManager Instance { get; set; }
    public GameObject Player { get => player; }
    public Weapon hoverWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    public Keycard keycard = null;
    public Door door = null;
    public Alien alien = null;
    public int keycards = 0;
    public float interactionDistance = 10f;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if(objectHitByRaycast.GetComponent<Weapon>())
            {
                hoverWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();

                if (Vector3.Distance(hoverWeapon.transform.position, player.transform.position) < interactionDistance)
                {

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject); 
                    }
                    
                }
                    
            }

            // AmmoBox
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {
                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();

                if (Vector3.Distance(hoveredAmmoBox.transform.position, player.transform.position) < interactionDistance)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                        Destroy(objectHitByRaycast.gameObject);
                    }
                }

            }

            // keycard
            if (objectHitByRaycast.GetComponent<Keycard>())
            {
                keycard = objectHitByRaycast.gameObject.GetComponent<Keycard>();

                if (Vector3.Distance(keycard.transform.position, player.transform.position) < interactionDistance)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        keycards += 1;
                        Destroy(objectHitByRaycast.gameObject);
                    }
                }

            }

            // door
            if (objectHitByRaycast.GetComponent<Door>())
            {
                door = objectHitByRaycast.gameObject.GetComponent<Door>();



                if (Vector3.Distance(door.transform.position, player.transform.position) < interactionDistance)
                {

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if(keycards >= 1)
                        {
                            keycards--;
                            SoundManager.Instance.doorOpening.Play();
                            door.GetComponent<Animation>().Play("open");
                            if(keycards < 0)
                            {
                                keycards = 0;
                            }
                        }
                    }

                }
            }

            // alien
            if (objectHitByRaycast.GetComponent<Alien>())
            {
                alien = objectHitByRaycast.gameObject.GetComponent<Alien>();


                if (Vector3.Distance(alien.transform.position, player.transform.position) < interactionDistance)
                { 
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        SoundManager.Instance.alienUnleashed.Play();
                        Invoke("EndGame", 3);
                    }
                }

            }
        }
    }

    void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }
}
