using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float colorChangeSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(SpawnPipesRoutine());
    }

    private IEnumerator SpawnPipesRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            GameObject pipe = ObjectPooler.Instance.GetPooledPipe();
            if (pipe != null)
            {
                float currentMinY = -2f;
                float currentMaxY = 3f;

                if (GameManager.Instance != null && GameManager.Instance.currentDifficulty != null)
                {
                    currentMinY = GameManager.Instance.currentDifficulty.minY;
                    currentMaxY = GameManager.Instance.currentDifficulty.maxY;
                }

                float randomY = Random.Range(currentMinY, currentMaxY);
                pipe.transform.position = new Vector3(transform.position.x, randomY, 0);

                AdjustPipeGap(pipe);

                Color currentColor = Color.HSVToRGB(Mathf.Repeat(Time.time * colorChangeSpeed, 1f), 1f, 1f);
                SetPipeColor(pipe, currentColor);

                pipe.SetActive(true);
            }

            float currentInterval = 2f;
            if (GameManager.Instance != null && GameManager.Instance.currentDifficulty != null)
            {
                currentInterval = GameManager.Instance.currentDifficulty.spawnInterval;
            }

            yield return new WaitForSeconds(currentInterval);
        }
    }

    private void AdjustPipeGap(GameObject pipeMaster)
    {
        Transform topPipe = pipeMaster.transform.Find("Top");
        Transform bottomPipe = pipeMaster.transform.Find("Bottom");
        Transform scoreArea = pipeMaster.transform.Find("ScoreArea");

        if (topPipe != null && bottomPipe != null)
        {
            float min = 2.5f, max = 4f;
            if (GameManager.Instance != null && GameManager.Instance.currentDifficulty != null)
            {
                min = GameManager.Instance.currentDifficulty.minGapSize;
                max = GameManager.Instance.currentDifficulty.maxGapSize;

                if (min <= 0 || max <= 0)
                {
                    min = 2.5f;
                    max = 4f;
                }
            }
            float gap = Random.Range(min, max);

            SpriteRenderer topSr = topPipe.GetComponent<SpriteRenderer>();
            SpriteRenderer botSr = bottomPipe.GetComponent<SpriteRenderer>();

            float topHalfHeight = topSr != null ? topSr.bounds.extents.y : 10f;
            float botHalfHeight = botSr != null ? botSr.bounds.extents.y : 10f;

            topPipe.localPosition = new Vector3(0, (gap / 2f) + topHalfHeight, 0);
            bottomPipe.localPosition = new Vector3(0, -(gap / 2f) - botHalfHeight, 0);

            if (scoreArea != null)
            {
                BoxCollider2D scoreCollider = scoreArea.GetComponent<BoxCollider2D>();
                if (scoreCollider != null)
                {
                    scoreCollider.size = new Vector2(scoreCollider.size.x, gap);
                }
            }
        }
    }

    private void SetPipeColor(GameObject pipeMaster, Color color)
    {
        Transform topPipe = pipeMaster.transform.Find("Top");
        Transform bottomPipe = pipeMaster.transform.Find("Bottom");

        if (topPipe != null)
        {
            SpriteRenderer sr = topPipe.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = color;
        }

        if (bottomPipe != null)
        {
            SpriteRenderer sr = bottomPipe.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = color;
        }
    }
}