using UnityEngine;

public class Pipe : MonoBehaviour
{
    private float speed;

    private void OnEnable()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentDifficulty != null)
        {
            speed = GameManager.Instance.currentDifficulty.pipeSpeed;
        }
        else
        {
            speed = 2f;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -15f)
        {
            gameObject.SetActive(false);
        }
    }
}