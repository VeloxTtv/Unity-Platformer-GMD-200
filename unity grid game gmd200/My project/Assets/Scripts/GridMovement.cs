using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridMovement : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Vector2IntVariable _playerGridPos;
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private float _cooldownTime = 1f;
    [SerializeField] private float _actionTime = 0f;
    Tween _moveTween;

    public UIManager uiManager;

    void GoToPosition(Vector2Int pos)
    {
        Debug.Log($"Moving to {pos}, gridManager is {(_gridManager == null ? "NULL" : "assigned")}");
        if (_moveTween != null && _moveTween.IsActive())
            return;

        _playerGridPos.Value = pos;
        GameObject tile = _gridManager.GetTile(_playerGridPos.Value.x, _playerGridPos.Value.y);
        Vector3 targetPos = tile.transform.position;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetPos, _moveDuration).SetEase(_moveCurve));
        seq.SetLink(gameObject);
        _moveTween = seq;
    }

    void Update()
    {

        if (Time.time > _actionTime)
        {
            if (Input.GetKeyDown(KeyCode.D) && _gridManager.moveValid(_playerGridPos.Value.x + 1, _playerGridPos.Value.y))
            {
                GoToPosition(_playerGridPos.Value + new Vector2Int(1, 0));

                _actionTime = Time.time + _cooldownTime;
            }
            if (Input.GetKeyDown(KeyCode.A) && _gridManager.moveValid(_playerGridPos.Value.x - 1, _playerGridPos.Value.y))
            {
                GoToPosition(_playerGridPos.Value + new Vector2Int(-1, 0));

                _actionTime = Time.time + _cooldownTime;
            }
            if (Input.GetKeyDown(KeyCode.W) && _gridManager.moveValid(_playerGridPos.Value.x, _playerGridPos.Value.y + 1))
            {
                GoToPosition(_playerGridPos.Value + new Vector2Int(0, 1));

                _actionTime = Time.time + _cooldownTime;
            }
            if (Input.GetKeyDown(KeyCode.S) && _gridManager.moveValid(_playerGridPos.Value.x, _playerGridPos.Value.y - 1))
            {
                GoToPosition(_playerGridPos.Value + new Vector2Int(0, -1));

                _actionTime = Time.time + _cooldownTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            uiManager.ToggleWinScreen();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            uiManager.ToggleDeathScreen();
        }
    }
}
