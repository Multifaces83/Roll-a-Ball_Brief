using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue;
    
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ScenarioData _scenario;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _goldStarPrefab;
    [SerializeField] private GameObject _winStarPrefab;
    [SerializeField] private GameObject _winParticulesPrefab;
    

    //MOVEMENT
    private float movementX;
    private float movementY;
    private float _speed = 5.0f;

    //STAR EFFECTS ON PLAYER
    private float clingInterval = 0.3f;
    private float clingduration = 30f;
    private Color clingColorFirst = Color.white;
    private Color clingColorNext = Color.yellow;
    private AudioSource clingSound;
    private bool soundPlayed = false;
    private bool clingEnabled = false;
    //private bool _reset = false;
    void Start()
    {
        //Get Sound in Component
        clingSound = GetComponent<AudioSource>();

        
        //Check first Scene
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Level - 1")
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("ScoreValue");
        }
        if(scene.name == "Level - 2")
        {
            PlayerPrefs.SetInt("ScoreValue",  8);
        }
        if(scene.name == "Level - 3")
        {
            PlayerPrefs.SetInt("ScoreValue", 16);
        }
        
        //Get Rigidbody Component
        _rigidbody = GetComponent<Rigidbody>();
     
        //Get Score
        _scoreText.text = PlayerPrefs.GetString("Score");
        ScoreValue = PlayerPrefs.GetInt("ScoreValue");
    }

    void Update()
    { 
        if(clingEnabled)
        {
            ClingCling();
        }
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        _rigidbody.AddForce(movement*_speed);
    }
    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Debug.Log(other.gameObject.transform.position);
            Destroy(other.gameObject);
            UpdateScore();
        }
        if (other.gameObject.CompareTag("Star"))      
         {          
        
            clingEnabled = true;
            Destroy(other.gameObject);
            _speed += 1.0f;
        
         }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            UpdateScore();
        }        
    }


    private void UpdateScore()
    {
        int i = 0;
        if (i < _scenario.WallPositions.Length) {
            
                Instantiate(_wallPrefab, _scenario.WallPositions[ScoreValue].WallPosition, _scenario.WallPositions[ScoreValue].WallRotation);
                Instantiate(_goldStarPrefab, _scenario.StarPositions[ScoreValue].StarPosition, _scenario.StarPositions[ScoreValue].StarRotation);
            i++;
        }
    
        ScoreValue++;
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        _scoreText.text = PlayerPrefs.GetString("Score");
        



        if (ScoreValue == 8)
        {            
            PlayerPrefs.SetInt("ScoreValue", ScoreValue);
            SceneManager.LoadScene("Level - 2");


        }
        else if (ScoreValue == 16)
        {
            PlayerPrefs.SetInt("ScoreValue", ScoreValue);
            SceneManager.LoadScene("Level - 3");


        }
        // else if (ScoreValue == 24)
        // {
        //     ScoreValue = 0;
        //     PlayerPrefs.DeleteKey("Score");
        //     PlayerPrefs.DeleteKey("ScoreValue");
        //     SceneManager.LoadScene("Level - 1");
        // }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("ScoreValue");
    }


    private void ClingCling()
{
    if (clingduration >= 0)
    {
        Renderer color = GetComponent<Renderer>();
        Renderer emissive = GetComponent<Renderer>();

        if (!soundPlayed)
        {
            clingSound.Play();
            soundPlayed = true;
        }

        //color.material.color = Color.Lerp(clingColorFirst, clingColorNext, Mathf.PingPong(Time.time, clingInterval));
        emissive.material.EnableKeyword("_EMISSION");
        emissive.material.SetColor("_EmissionColor", Color.Lerp(clingColorFirst, clingColorNext, Mathf.PingPong(Time.time, clingInterval)));
        clingduration -= Time.deltaTime;
    }
    else
    {
        Renderer emissive = GetComponent<Renderer>();
        emissive.material.DisableKeyword("_EMISSION");
        //color = actualColor;
        clingSound.Stop();
        soundPlayed = false;
    }
}
}
