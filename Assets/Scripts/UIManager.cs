using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletText;
    [SerializeField] private GameObject healthText;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        TMP_Text tmp_text;
        tmp_text = bulletText.GetComponent<TMP_Text>();
        tmp_text.text = PlayerController.bullet.ToString() + " |" + PlayerController.bulletCountClip.ToString();
    }
}