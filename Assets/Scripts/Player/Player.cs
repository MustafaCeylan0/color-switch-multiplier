using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum ControllerType
    {
        touchInput,
        mouseInput
    }

    //[SerializeField] private ControllerType controllerType;

    private ArrayList collidedMultipliers = new ArrayList();

    [Header("Controller")] [SerializeField]
    private GameObject playerGO;

    private Rigidbody2D playerRB;

    [Header("Player Values")] public int playerID = -1;
    [SerializeField] private float pushingPower = 5;


    [SerializeField] [Tooltip("The proportion of the x value in the pushing vector.")]
    private float xValue = 1;

    [SerializeField] [Tooltip("The proportion of the y value in the pushing vector.")]
    private float yValue = 2;

    private Vector2 rightPushVector;

    private Vector2 leftPushVector;

    [SerializeField] [Tooltip("The rotating value of the player.")]
    private float angularVelocity = 500;


    [Header("Player")] [SerializeField] private int playerIndex;
    [SerializeField] private Transform parentTransform;
    public string color;
    private Color currentColor;
    private Color[] colors;


    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private GameObject moveParticle;

    [SerializeField] private float rightClickParticleRotationX;
    [SerializeField] private float leftClickParticleRotationX;

    [SerializeField] [Tooltip("The multiplier of the move particles offset to the player.")]
    private float offsetMultiplier = 1;

    private Vector3 leftJumpOffset;
    private Vector3 rightJumpOffset;
    private float screenMidpoint;
    public bool isControlsActive = true;
    private SFXManager sfxManager;
    [SerializeField] private GameObject[] playerObjects;


    private void Awake()
    {
        colors = FindObjectOfType<GameValues>().gameColors;
        if (playerIndex != PlayerPrefs.GetInt("Player Object Index"))
        {
            Debug.Log("Player's current index is " + playerIndex + ", original index must be " +
                      PlayerPrefs.GetInt("Player Object Index") + ".");
            GameObject newPlayer = Instantiate(playerObjects[PlayerPrefs.GetInt("Player Object Index")],
                transform.position,
                quaternion.identity);
            newPlayer.GetComponent<Player>().color = color;
            newPlayer.GetComponent<Player>().setColor(false);
            //newPlayer.GetComponent<Player>().color = color;
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Player"))
        {
            parentTransform = FindObjectOfType<PlayerParent>().gameObject.transform;
            transform.SetParent(parentTransform);
        }


        //Setting the offset for jumping particles so they dont look like they come out of the player gameobject.
        leftJumpOffset = offsetMultiplier *
                         new Vector2(playerGO.transform.localScale.x / -1.5f, playerGO.transform.localScale.y / 1.5f);
        rightJumpOffset = offsetMultiplier *
                          new Vector2(playerGO.transform.localScale.x / 1.5f, playerGO.transform.localScale.y / 1.5f);
        playerRB = playerGO.GetComponent<Rigidbody2D>();
        if (color == null)
        {
            color = "RED";
        }
        else
        {
            color = color.ToUpper();
        }

        if (gameObject.CompareTag("Player"))
        {
            setColor(false);
        }
    }

    public void rescue(string formerColor, ArrayList alreadyCollidedMultipliers, Vector2 formerVelocity)
    {
        parentTransform = FindObjectOfType<PlayerParent>().gameObject.transform;
        color = formerColor;
        setColor(false);
        transform.SetParent(parentTransform);
        gameObject.tag = "Player";
        isControlsActive = true;
        playerRB.bodyType = RigidbodyType2D.Dynamic;
        playerRB.velocity = formerVelocity;
        foreach (var alreadyCollidedMultiplier in alreadyCollidedMultipliers)
        {
            collidedMultipliers.Add(alreadyCollidedMultiplier);
        }

        FindObjectOfType<PlayerParent>().totalPlayerAmount++;
    }

    private void Start()
    {
        //CONTROLLER
        /*rotateRight = rotateToRight();
        rotateLeft = rotateToLeft();*/

        rightPushVector = new Vector2(xValue, yValue);
        leftPushVector = new Vector2(-xValue, yValue);
        screenMidpoint = Screen.width / 2f;
        Invoke(nameof(setID), .5f);
    }

    private void setID()
    {
        if (playerID == -1) // Checking if the player is the original player.
        {
            playerID = 0;
            FindObjectOfType<LevelManagement>().latestPlayerID = 0;
        }
    }


    private void leftSideClick()
    {
        if (playerRB)
        {
            playerRB.angularVelocity = angularVelocity;
            playerRB.velocity = pushingPower * leftPushVector;
        }
    }

    private void rightSideClick()
    {
        if (playerRB)
        {
            playerRB.angularVelocity = -angularVelocity;
            playerRB.velocity = pushingPower * rightPushVector;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftSideClick();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightSideClick();
        }

#else
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < screenMidpoint)
            {
                leftSideClick();
            }
            else
            {
                rightSideClick();
            }
        }
#endif
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Obstacle":
                GameObject otherGO = other.gameObject;
                ObstacleIndividual otherOI = otherGO.GetComponent<ObstacleIndividual>();


                if (otherOI.color.ToUpper() != this.color.ToUpper())
                {
                    GetComponent<Collider2D>().enabled = false;
                    die();
                }

                break;
            case "Color Changer":
                if (other.GetComponent<ColorChanger>().color != color)
                {
                    color = other.GetComponent<ColorChanger>().color;
                    setColor(true);
                }

                break;
            case "Multiplier":
                if (!collidedMultipliers.Contains(other.gameObject))
                {
                    multiply(other);
                }

                break;
            case "Block":
                if (other.GetComponent<Block>().blockColor.ToLower() != color.ToLower())
                {
                    other.GetComponent<Block>().takeDamage();
                    die();
                }

                break;
            case "Unrescued Player":
                Player otherPlayerScript = other.GetComponent<Player>();
                otherPlayerScript.rescue(color, collidedMultipliers, playerRB.velocity);


                break;
            case "Finish Line":
                if (isControlsActive)
                {
                    StartCoroutine(levelFinished());
                    FindObjectOfType<PlayerParent>().totalPlayerAmount--;

                    FindObjectOfType<FinishLine>().updateFinishLineText();
                }

                break;
            case "Addition":

                other.GetComponent<Addition>().add(gameObject, transform, playerRB.velocity, parentTransform);
                break;
            case "Subtraction":

                other.GetComponent<Substracter>().substract();
                break;
        }
    }

    public void doubleThePlayer()
    {
        GameObject clone = Instantiate(gameObject,
            transform.position,
            Quaternion.identity);
        FindObjectOfType<PlayerParent>().totalPlayerAmount++;
        foreach (GameObject multiplier in collidedMultipliers)
        {
            clone.GetComponent<Player>().addCollidedMultiplier(multiplier);
        }

        clone.GetComponent<Rigidbody2D>().velocity = playerRB.velocity;
        clone.transform.SetParent(parentTransform);
    }

    private void multiply(Collider2D other)
    {
        FindObjectOfType<SFXManager>().multiply();
        collidedMultipliers.Add(other.gameObject);
        for (int i = 0; i < other.GetComponent<MultiplierLine>().multiplier - 1; i++)
        {
            GameObject clone = Instantiate(gameObject,
                transform.position,
                Quaternion.identity);
            FindObjectOfType<PlayerParent>().totalPlayerAmount++;
            foreach (GameObject multiplier in collidedMultipliers)
            {
                clone.GetComponent<Player>().addCollidedMultiplier(multiplier);
            }

            clone.GetComponent<Rigidbody2D>().velocity = playerRB.velocity;
            clone.transform.SetParent(parentTransform);
        }
    }

    public void addCollidedMultiplier(GameObject multiplier)
    {
        collidedMultipliers.Add(multiplier);
    }

    public void die()
    {
        foreach (var particleSystem in explosionParticle.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.startColor = currentColor;
        }

        FindObjectOfType<SFXManager>().death();

        Destroy(gameObject);

        Instantiate(explosionParticle, transform.position, quaternion.identity);
        FindObjectOfType<PlayerParent>().totalPlayerAmount--;
    }

    public void setColor(bool isPaint)
    {
        if (isPaint)
        {
            FindObjectOfType<SFXManager>().paintPlayer();
        }

        switch (color.ToUpper())
        {
            case "RED":
                gameObject.GetComponent<SpriteRenderer>().color = colors[0];
                currentColor = colors[0];


                break;
            case "PURPLE":
                gameObject.GetComponent<SpriteRenderer>().color = colors[1];
                currentColor = colors[1];


                break;
            case "GREEN":
                gameObject.GetComponent<SpriteRenderer>().color = colors[2];
                currentColor = colors[2];


                break;
            case "YELLOW":
                gameObject.GetComponent<SpriteRenderer>().color = colors[3];
                currentColor = colors[3];


                break;
        }
    }

    public IEnumerator levelFinished()
    {
        isControlsActive = false;
        while (true)
        {
            rightSideClick();
            yield return new WaitForSeconds(.2f);
            leftSideClick();
            yield return new WaitForSeconds(.2f);
        }
    }
}