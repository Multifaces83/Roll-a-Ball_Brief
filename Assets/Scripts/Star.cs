using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class Star : MonoBehaviour
{
    private Quaternion rotateStar;
    [SerializeField] private TMP_Text timerText;
    private float timer = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.ToString("0");
        timer -= Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.up, 30 * Time.deltaTime);
    }
}
