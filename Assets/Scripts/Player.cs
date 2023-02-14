using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue;
    private float _speed = 10.0f;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ScenarioData _scenario;
    [SerializeField] private GameObject _wallPrefab;
    //private bool _reset = false;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Level - 1")
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("ScoreValue");
        }
        

        _rigidbody = GetComponent<Rigidbody>();
        // if (_reset)
        // {
        //     _scoreText.text = "Score : 0";
        //     _reset = false;
        // }
        _scoreText.text = PlayerPrefs.GetString("Score");
        ScoreValue = PlayerPrefs.GetInt("ScoreValue");
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * _speed * Time.deltaTime, ForceMode.Impulse);
        }
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
        if (collision.gameObject.CompareTag("Star"))
        {

        }
    }


    private void UpdateScore()
    {
        int i = 0;
        if (i < _scenario.WallPositions.Length) {
            
                Instantiate(_wallPrefab, _scenario.WallPositions[ScoreValue].WallPosition, _scenario.WallPositions[ScoreValue].WallRotation);
            i++;
        }
        /*else {
            Debug.Log("ta mere");
        }*/
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
        else if (ScoreValue == 24)
        {
            ScoreValue = 0;
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("ScoreValue");
            SceneManager.LoadScene("Level - 1");
        }
    }

}
