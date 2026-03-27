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

    [HideInInspector] public enemyController enemy;
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
        seq.OnComplete(() =>
        {
            if (enemy != null)
                enemy.reactToMove();
            else
                Debug.LogError("enemy is not assigned on " + gameObject.name);
        });
        seq.SetLink(gameObject);
        _moveTween = seq;
    }

    void Start()
    {
        _playerGridPos.Value = new Vector2Int(0, 0);
    }

    void Update()
    {
        if (Time.time > _actionTime)
        {
            Vector2Int move = Vector2Int.zero;

            if (Input.GetKeyDown(KeyCode.D)) move = new Vector2Int(1, 0);
            else if (Input.GetKeyDown(KeyCode.A)) move = new Vector2Int(-1, 0);
            else if (Input.GetKeyDown(KeyCode.W)) move = new Vector2Int(0, 1);
            else if (Input.GetKeyDown(KeyCode.S)) move = new Vector2Int(0, -1);

            if (move != Vector2Int.zero)
            {
                Vector2Int targetPos = _playerGridPos.Value + move;
                if (_gridManager.moveValid(targetPos.x, targetPos.y))
                {
                    GoToPosition(targetPos);
                    _actionTime = Time.time + _cooldownTime;
                }
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
