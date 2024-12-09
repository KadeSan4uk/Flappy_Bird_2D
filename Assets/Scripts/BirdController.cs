using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Rigidbody2D rigibody;
    public Collider2D _birdCollider;
    public Material backgroundMaterial;
    public Material platformMaterial;
    public PipesController pipesController;
    private int _points;

    public float forsePower = 1.5f;

    private bool gameOver;

    private void Awake()
    {
        backgroundMaterial.SetFloat("_MoveSpeed", 0.07f);
        platformMaterial.SetFloat("_MoveSpeed", 0.197f);
    }

    private void Update()
    {
        if (gameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigibody.AddForce(Vector2.up * forsePower, ForceMode2D.Impulse);
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-10, 50, rigibody.linearVelocityY * .2f));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _points += 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameOver = true;
        Time.timeScale = 0;
        Debug.Log($"{_points}");

        _birdCollider.enabled = false;
        pipesController.enabled = false;

        backgroundMaterial.SetFloat("_MoveSpeed", 0);
        platformMaterial.SetFloat("_MoveSpeed", 0);

        StartCoroutine(UnfreezeGame());
    }

    private IEnumerator UnfreezeGame()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
    }
}
