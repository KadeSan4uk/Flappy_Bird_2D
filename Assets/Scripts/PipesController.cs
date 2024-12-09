using System.Collections.Generic;
using UnityEngine;


public class PipesController : MonoBehaviour
{
    public Transform SpawnTransforPoint;
    public Transform DestroyTransformPoint;

    public Transform TopPipePrefab;
    public Transform BottomPipePrefab;

    public float MovePipeSpeed = 2f;
    public float PipesGenerationDelay = 1.5f;

    public float GenerationMaxHight = 2.5f;
    public float GenerationMinHight = -1.2f;

    private float _nextGenerationPipeTime;

    private List<Transform> _pipes = new List<Transform>();
    private List<Transform> _toRemove = new();


    private void Update()
    {
        if (_nextGenerationPipeTime < Time.time)
            GenerateNewPipes();

        MovePipes();
    }

    private void MovePipes()
    {
        foreach (var pipe in _pipes)
        {
            pipe.Translate(Vector2.left * MovePipeSpeed * Time.deltaTime);

            if (pipe.position.x <= DestroyTransformPoint.position.x)
            {
                _toRemove.Add(pipe);
            }
        }

        foreach (var pipe in _toRemove)
        {
            _pipes.Remove(pipe);
            Destroy(pipe.gameObject);
        }

        _toRemove.Clear();
    }

    public void GenerateNewPipes()
    {
        _nextGenerationPipeTime = Time.time + PipesGenerationDelay;
        var checkChance = Random.Range(0, 100);

        if (checkChance < 20)
        {
            //Great Top
            InstantiatePipe(TopPipePrefab, Random.Range(GenerationMinHight, GenerationMaxHight));
        }
        else if (checkChance > 80)
        {
            //Great Bottom          
            InstantiatePipe(BottomPipePrefab, Random.Range(GenerationMinHight, GenerationMaxHight));
        }
        else
        {
            float randomY = Random.Range(GenerationMinHight, GenerationMaxHight);

            InstantiatePipe(BottomPipePrefab, randomY);
            InstantiatePipe(TopPipePrefab, randomY);            
        }
    }

    private void InstantiatePipe(Transform pipePrefab, float randomY)
    {
        Transform pipe = Instantiate(pipePrefab);
        pipe.position = SpawnTransforPoint.position;

        var pipePosition = pipe.position;
        pipePosition.y = randomY;
        pipe.position = pipePosition;

        _pipes.Add(pipe);
    }
}
