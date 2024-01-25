using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform[] _gameObjects;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 200f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private int _currentPointIndex = 0;
    private float _travelTime;
    private float _currentTime;

    private void Awake()
    {
        SetNextPoints();
    }

    private void SetNextPoints()
    {
        var nextPointIndex = (_currentPointIndex + 1) % _gameObjects.Length;
        _startPosition = _gameObjects[_currentPointIndex].transform.position;
        _endPosition = _gameObjects[nextPointIndex].transform.position;
        _travelTime = Vector3.Distance(_startPosition, _endPosition) / _speed;
        _currentPointIndex = nextPointIndex;
    }

    private void Update()
    {
        transform.Rotate(0f, _rotationSpeed * Time.deltaTime, 0f);

        _currentTime += Time.deltaTime;
        var progress = _currentTime / _travelTime;
        transform.position = Vector3.Lerp(_startPosition, _endPosition, progress);

        if (!(_currentTime > _travelTime)) return;
        SetNextPoints();
        _currentTime = 0;
    }
}