using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public HouseData houseData; // Reference to HouseData scriptable object
    public Wander wander;
    [SerializeField] private NavMeshAgent characterAgent;
    [SerializeField] private GameObject playerHouse;
    public TMP_Text rssText;
    public int wood;
    public int stone;
    public int steel;
    public int coins;
    public GameObject zonePrefab;
    public GameObject housePrefab;
    public bool zoning = false;
    private GameObject instantiatedZone;
    public bool canPlaceHouse = true;
    public TMP_Text homeUI;
    public TMP_Text nameText;
    public GameObject returnHome;
    public TMP_Text job;

    void Start()
    {
        houseData = FindObjectOfType<HouseData>(); // Find and assign HouseData scriptable object
        returnHome.SetActive(false);
    }

    void Update()
    {
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        canPlaceHouse = true; // Assume the house can be placed unless proven otherwise

        if (instantiatedZone != null)
        {
            foreach (GameObject house in houses)
            {
                if (house.transform.position.x == instantiatedZone.transform.position.x &&
                    house.transform.position.z == instantiatedZone.transform.position.z)
                {
                    canPlaceHouse = false;
                    break; // No need to check further, as we've found a house at the same position
                }
            }
        }
        rssText.text = "Wood: " + wood + "\n" + "Stone: " + stone + "\n" + "Metal: " + steel + "\n" + "Coins: " + coins;
        if (zoning && Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(instantiatedZone.transform.root.gameObject);
            instantiatedZone = null;
            zoning = false;
        }
        if (zoning && Input.GetMouseButtonDown(0) && canPlaceHouse)
        {
            Instantiate(housePrefab, instantiatedZone.transform.position, Quaternion.identity);
            Destroy(instantiatedZone.transform.root.gameObject);
            instantiatedZone = null;
            zoning = false;
        }
    }

    public void UpdateSelectionUI(int CharacterID, GameObject playerObj)
    {
        characterAgent = playerObj.GetComponent<NavMeshAgent>();
        playerHouse = houseData.characterHouses[CharacterID];
        wander = playerObj.GetComponent<Wander>();
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse is clicking on a UI element, do nothing for now
        }
        else
        {
            // Mouse is clicking on something other than a UI element
            if (Input.GetMouseButtonDown(0))
            {
                // Perform a raycast to detect objects clicked by the mouse
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (!hit.collider.gameObject.CompareTag("Floor") && !hit.collider.gameObject.CompareTag("Untagged"))
                    {
                        // Display the tag of the clicked object
                        nameText.text = hit.collider.gameObject.tag;
                    }
                    else
                    {
                        nameText.text = "";
                    }
                }
            }
        }


        if (CharacterID > 0)
        {
            if (playerHouse == null)
            {
                homeUI.text = "Homeless";
                returnHome.gameObject.SetActive(false);
            }
            else if (playerHouse != null)
            {
                homeUI.text = "";
                returnHome.gameObject.SetActive(true);
            }
            if (nameText.text != "Person")
            {
                homeUI.gameObject.SetActive(false);
                returnHome.gameObject.SetActive(false);
                job.gameObject.SetActive(false);
                print("setfalse");
            }
            else
            {
                homeUI.gameObject.SetActive(true);
                job.gameObject.SetActive(true);
            }
        }
        else
        {
            homeUI.gameObject.SetActive(false);
            returnHome.gameObject.SetActive(false);
            job.gameObject.SetActive(false);
        }
    }
    public void ReturnHome()
    {
        characterAgent.SetDestination(playerHouse.transform.position);
        wander.isWandering = false;
    }
    public void OnButtonClick()
    {
        if (!zoning)
        {
            instantiatedZone = Instantiate(zonePrefab, Vector3.zero, Quaternion.identity);
            zoning = true;
        }
    }
}
