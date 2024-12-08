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
            Transform pipe = Instantiate(TopPipePrefab);
            pipe.position = SpawnTransforPoint.position;

            var pipePosition = pipe.position;
            pipePosition.y = Random.Range(GenerationMinHight, GenerationMaxHight);
            pipe.position = pipePosition;
            _pipes.Add(pipe);
        }
        else if (checkChance > 80)
        {
            //Great Bottom
            Transform pipe = Instantiate(BottomPipePrefab);
            pipe.position = SpawnTransforPoint.position;

            var pipePosition = pipe.position;
            pipePosition.y = Random.Range(GenerationMinHight, GenerationMaxHight);
            pipe.position = pipePosition;
            _pipes.Add(pipe);
        }
        else
        {
            Transform pipeTop = Instantiate(TopPipePrefab);
            Transform pipeBottom = Instantiate(BottomPipePrefab);
            pipeTop.position = SpawnTransforPoint.position;
            pipeBottom.position = SpawnTransforPoint.position;

            var randomHigtPosition = Random.Range(GenerationMinHight, GenerationMaxHight);

            var pipePosition = pipeTop.position;
            pipePosition.y = randomHigtPosition;
            pipeTop.position = pipePosition;

            pipePosition = pipeBottom.position;
            pipePosition.y = randomHigtPosition;
            pipeBottom.position = pipePosition;

            _pipes.Add(pipeTop);
            _pipes.Add(pipeBottom);
        }





    }
}
