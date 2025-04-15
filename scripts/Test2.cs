using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Test2 : MonoBehaviour
{
    public float velocidad;
    public Rigidbody rb;
    //public Camera camara;
    //public Vector3 offset = new Vector3(0, 3, -5);
    public AudioSource musica;
    public AudioClip sceneMusic;
    public TMP_Text textoEstrellas;
    private int Puntos = 0;
    private int Vidas = 3;

    void Awake()
    {
        transform.position = new Vector3(385, 1.5f, 156);
    }
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        //offset = camara.transform.position;
        musica = GetComponent<AudioSource>();
        if (sceneMusic != null)
        {
            musica.clip = sceneMusic;
            musica.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puntos") )
        {
            other.gameObject.SetActive(false);
            Debug.Log("Punto recogido");
            Puntos++;
            if (Puntos == 6)
            {
                if(SceneManager.GetActiveScene().name == "dos")
                {
                    SceneManager.UnloadSceneAsync("dos");
                    SceneManager.LoadScene("Final");
                }
                else if(SceneManager.GetActiveScene().name == "main")
                {
                    SceneManager.UnloadSceneAsync("main");
                    SceneManager.LoadScene("dos");
                }
                
            }
            textoEstrellas.text = "Puntos: " + Puntos + " Vidas: " + Vidas;
        }
        if(other.gameObject.CompareTag("Muerte"))
        {
            Vidas--;
            //baked.PlayAy();
            if (Vidas == 0)
            {
                SceneManager.UnloadSceneAsync("dos");
                SceneManager.LoadScene("main");
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.position = new Vector3(0, 1, 0);
                textoEstrellas.text = "Puntos: " + Puntos + " Vidas: " + Vidas;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movHorizontal, 0.0f, movVertical);
        rb.AddForce(movimiento * velocidad);
        //camara.transform.position = transform.position + offset;

    }

}