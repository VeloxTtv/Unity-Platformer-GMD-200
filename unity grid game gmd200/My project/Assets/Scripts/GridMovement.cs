using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridMovement : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Vector2Int _gridPos;
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private AnimationCurve _moveCurve;
    Tween _moveTween;

    public UIManager uiManager;

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && _gridManager.moveValid(_gridPos.x + 1, _gridPos.y))
        {
            GoToPosition(_gridPos + new Vector2Int(1,0));
        }
        if (Input.GetKeyDown(KeyCode.A) && _gridManager.moveValid(_gridPos.x - 1, _gridPos.y))
        {
            GoToPosition(_gridPos + new Vector2Int(-1, 0));
        }
        if (Input.GetKeyDown(KeyCode.W) && _gridManager.moveValid(_gridPos.x, _gridPos.y + 1))
        {
            GoToPosition(_gridPos + new Vector2Int(0, 1));
        }
        if (Input.GetKeyDown(KeyCode.S) && _gridManager.moveValid(_gridPos.x, _gridPos.y - 1))
        {
            GoToPosition(_gridPos + new Vector2Int(0, -1));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            uiManager.ToggleDeathScreen();
        }
    }
}
