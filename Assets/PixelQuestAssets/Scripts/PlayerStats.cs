using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.U2D;

public class PlayerStats : MonoBehaviour
{
    // Respawn 
    public Transform respawnPoint; // Keeps track of where the player we respawn at 
    public Transform Screen;
    public Transform DeathScreen;
    public Transform FinishScreen;
    public Transform DoorPoint;
    public Sprite DoorMain;

    // Player stats 
    public float playerLife = 3;   // How much health the player currently has 
    public int currentCoins = 0;   // How many coins has the player collected 
    private float playerMaxHealth = 6   ; // What is the max health the player can have 
    private int maxCoins = 0; // What is the amount of coins in the level 
    public Boolean finish = false;

    PQPlayerMovement playerMovement;
    PlayerJumping playerJumping;


    // Rigidbody 
    private Rigidbody2D rigidbody2D; // Controls player speed 

    // Tags 
    private const string deathTag = "Death";
    private const string healthTag = "Health";
    private const string coinTag = "Coin";
    private const string respawnTag = "Respawn";
    private const string finishTag = "Finish";

    // UI 
    public Image Heart1Image;            // Update the Heart Image of the player 
    public Image Heart2Image;
    public Image Heart3Image;
    public TextMeshProUGUI coinText;    // Update the text showing coins collected 
    public GameObject CoinParent;       // Parent we check to see how many coins are in the level 

    public Sprite FullLife;
    public Sprite HalfLife;
    public Sprite EmptyLife;

    public SpriteRenderer PlayerSprite;

    public float uispeed = 500;
    public float deathx = 0;

    // Auido 
    public AudioSource deathSFX;  // Death sound effect 
    public AudioSource coinSFX;   // Coin pick up sound effect 

    void Start()
    {
        // Connect to the rigidbody 
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Looks at the Coin Parent Game Object and check how many children it has, thats' the number
        // Of coins that are in the level, each time we destroy a coin the childCount would lower 
        // So we save the inforomation at the start of the game 
        maxCoins = CoinParent.transform.childCount;
        // Updates the UI to show the proper values of the level 
        coinText.text = currentCoins + "/" + maxCoins;
    }
    private void Update()
    {
        if (playerLife >= 2){
            Heart1Image.sprite = FullLife;
        }
        if (playerLife == 1)
        {
            Heart1Image.sprite = HalfLife;
        }
        if (playerLife <= 0)
        {
            Heart1Image.sprite = EmptyLife;
        }
        if (playerLife >= 4)
        {
            Heart2Image.sprite = FullLife;
        }
        if (playerLife == 3)
        {
            Heart2Image.sprite = HalfLife;
        }
        if (playerLife <= 2)
        {
            Heart2Image.sprite = EmptyLife;
        }
        if (playerLife >= 6)
        {
            Heart3Image.sprite = FullLife;
        }
        if (playerLife == 5)
        {
            Heart3Image.sprite = HalfLife;
        }
        if (playerLife <= 4)
        {
            Heart3Image.sprite = EmptyLife;
        }

        if (playerLife <= 0)
        {
            // Gets the name of the level we're currently in 
            DeathScreen.position = Vector2.MoveTowards(DeathScreen.position, Screen.position, uispeed * Time.deltaTime);                        // Reload that level 
        }
        if (finish == true)
        {
            rigidbody2D.velocity = Vector2.zero;
            FinishScreen.position = Vector2.MoveTowards(FinishScreen.position, Screen.position, uispeed * Time.deltaTime);
            rigidbody2D.position = Vector2.MoveTowards(rigidbody2D.position, DoorPoint.position, uispeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag){
            // Coin tag, dicates what happens after player touches coins 
            case coinTag:
                {
                    // Plays Sound Effect 
                    coinSFX.Play();
                    // Increase the value of coins by 1
                    currentCoins++;
                    // Updates the UI 
                    coinText.text = currentCoins + "/" + maxCoins;
                    // Destroys the coins
                    Destroy(collision.gameObject);
                    break;
                }
            // Death tag, dicates what happens after player touches something that could kill them
            case deathTag:
                {
                    // Play Sound Effect
                    deathSFX.Play();
                    // Make the speed zero 
                    rigidbody2D.velocity = Vector2.zero;
                    // Moves the player to the respawn point 
                    // Take away players life 
                    playerLife--;
                    // Updates the UI 
                    //heartImage.fillAmount = playerLife / playerMaxHealth;
                    // If the player has lost all of their lives reset the level 
                    transform.position = respawnPoint.position;
                    if (playerLife <= 0)
                    {
                        PlayerSprite.enabled = false;
                        rigidbody2D.velocity = Vector2.zero;
                    }
                    break;
                }
            // Health tag, dicates what happens after player touches a heart
            case healthTag:
                {
                    // Checks if the player is full on health, if they are nothing happens 
                    if(playerLife < playerMaxHealth)
                    {
                        // If the player is missing health we increase their life
                        playerLife++;
                        // Update the UI to show new health 
                        // Destroy the health object 
                        Destroy(collision.gameObject);
                    }
                    break;
            }
            // Respawn tag, dicates what happens after player touches a respawnpoint 
            case respawnTag:
                {
                    // We look for a child object called RespawnPoint, and we copy it's position
                    // This will be the new spot the player respawns from 
                    respawnPoint.position = collision.transform.Find("RespawnPoint").position;
                    break;
                }
            // Finish tag, dicates what happens after player touches a respawnpoint 
            case finishTag:
                {
                    finish = true;
                    break;
                }
        }
    }

}
