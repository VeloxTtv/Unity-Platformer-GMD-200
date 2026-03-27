using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private float _differenceInX;
    [SerializeField] private float _differenceInY;

    public Vector2Int _gridPos = new Vector2Int(0, 0);
    public GridManager _gridManager;
    public GameObject _player;
    public Vector2IntVariable _playerGridPos;
    
    Tween _moveTween;

    void GoToPosition(Vector2Int pos)
    {
        Debug.Log($"Moving to {pos}, gridManager is {(_gridManager == null ? "NULL" : "assigned")}");
        if (_moveTween != null && _moveTween.IsActive())
            return;

        _gridPos = pos;
        GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);
        Vector3 targetPos = tile.transform.position;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetPos, _moveDuration).SetEase(_moveCurve));
        seq.SetLink(gameObject);
        _moveTween = seq;
    }

    public void reactToMove()
    {
        float _differenceInX = _gridPos.x - _playerGridPos.Value.x;
        float _differenceInY = _gridPos.y - _playerGridPos.Value.y;

        Vector2Int targetPos;

        if (MathF.Abs(_differenceInX) >= MathF.Abs(_differenceInY))
        {
            // Move horizontally toward player
            int stepX = _differenceInX >= 0 ? -1 : 1;
            targetPos = _gridPos + new Vector2Int(stepX, 0);
        }
        else
        {
            // Move vertically toward player
            int stepY = _differenceInY >= 0 ? -1 : 1;
            targetPos = _gridPos + new Vector2Int(0, stepY);
        }

        // Validate before moving
        if (_gridManager.moveValid(targetPos.x, targetPos.y))
        {
            GoToPosition(targetPos);
        }
    }
}
