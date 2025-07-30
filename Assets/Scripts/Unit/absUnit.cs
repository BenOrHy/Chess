using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class absUnit : MonoBehaviour
{
    public int unitType;
    public Vector3 mainPos;
    public List<Vector3> Points = new List<Vector3>();
    public GameObject pointObject;
    public bool initiate = true;
    public int moveCount = 0;

    public GameObject[] PointObjSet = new GameObject[64];
    private GameObject pointContainer;

    void Awake()
    {
        position();
    }

    public void position()
    {
        mainPos = this.GetComponent<Snap>().initiateLoc;
        Points.Clear();

        if (unitType == 1)
        {
            if (this.tag.Equals("Black"))
            {
                Vector3 front1 = new Vector3(mainPos.x, mainPos.y + 1, 0);
                Vector3 front2 = new Vector3(mainPos.x, mainPos.y + 2, 0);

                if (IsInsideBoard(front1) && !IsUnitAt(front1))
                    Points.Add(front1);

                if (initiate && IsInsideBoard(front2) && !IsUnitAt(front1) && !IsUnitAt(front2))
                    Points.Add(front2);
            }
            else if (this.tag.Equals("White")){
                Vector3 front1 = new Vector3(mainPos.x, mainPos.y - 1, 0);
                Vector3 front2 = new Vector3(mainPos.x, mainPos.y - 2, 0);

                if (IsInsideBoard(front1) && !IsUnitAt(front1))
                    Points.Add(front1);

                if (initiate && IsInsideBoard(front2) && !IsUnitAt(front1) && !IsUnitAt(front2))
                    Points.Add(front2);
            }
        }

        if (unitType == 2)
        {
            Vector2Int[] knightMoves = {
                new Vector2Int(+1, +2), new Vector2Int(-1, +2),
                new Vector2Int(+1, -2), new Vector2Int(-1, -2),
                new Vector2Int(+2, +1), new Vector2Int(-2, +1),
                new Vector2Int(+2, -1), new Vector2Int(-2, -1)
            };

            foreach (var move in knightMoves)
            {
                Vector3 newPos = new Vector3(mainPos.x + move.x, mainPos.y + move.y, 0);
                if (!IsInsideBoard(newPos)) continue;
                if (IsBlockedBySameTeam(newPos)) continue;

                Points.Add(newPos);
            }
        }

        if (unitType == 3)
        {
            Vector3[] dirs = {
                new Vector3(+1, +1, 0), new Vector3(+1, -1, 0),
                new Vector3(-1, -1, 0), new Vector3(-1, +1, 0)
            };

            foreach (var dir in dirs)
            {
                for (int i = 1; i < 8; i++)
                {
                    Vector3 newPos = mainPos + dir * i;
                    if (!IsInsideBoard(newPos)) break;

                    if (IsBlockedBySameTeam(newPos)) break;

                    Points.Add(newPos);
                    if (IsEnemyAt(newPos)) break;
                }
            }
        }

        if (unitType == 4)
        {
            Vector3[] dirs = {
                Vector3.right, Vector3.left, Vector3.up, Vector3.down
            };

            foreach (var dir in dirs)
            {
                for (int i = 1; i < 8; i++)
                {
                    Vector3 newPos = mainPos + dir * i;
                    if (!IsInsideBoard(newPos)) break;

                    if (IsBlockedBySameTeam(newPos)) break;

                    Points.Add(newPos);
                    if (IsEnemyAt(newPos)) break;
                }
            }
        }

        if (unitType == 5)
        {
            Vector3[] dirs = {
                Vector3.right, Vector3.left, Vector3.up, Vector3.down,
                new Vector3(+1, +1, 0), new Vector3(+1, -1, 0),
                new Vector3(-1, -1, 0), new Vector3(-1, +1, 0)
            };

            foreach (var dir in dirs)
            {
                for (int i = 1; i < 8; i++)
                {
                    Vector3 newPos = mainPos + dir * i;
                    if (!IsInsideBoard(newPos)) break;

                    if (IsBlockedBySameTeam(newPos)) break;

                    Points.Add(newPos);
                    if (IsEnemyAt(newPos)) break;
                }
            }
        }

        if (unitType == 6)
        {
            Vector3[] kingMoves = {
                new Vector3(+1, +1, 0), new Vector3(-1, +1, 0),
                new Vector3(+1, -1, 0), new Vector3(-1, -1, 0),
                new Vector3(+1,  0, 0), new Vector3(-1,  0, 0),
                new Vector3( 0, +1, 0), new Vector3( 0, -1, 0)
            };

            foreach (var offset in kingMoves)
            {
                Vector3 newPos = mainPos + offset;
                if (!IsInsideBoard(newPos)) continue;
                if (IsBlockedBySameTeam(newPos)) continue;

                Points.Add(newPos);
            }
        }
    }

    bool IsInsideBoard(Vector3 pos)
    {
        return pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8;
    }

    bool IsUnitAt(Vector3 pos)
    {
        return Physics2D.OverlapCircle(pos, 0.1f) != null;
    }

    bool IsBlockedBySameTeam(Vector3 pos)
    {
        Collider2D hit = Physics2D.OverlapCircle(pos, 0.1f);
        return hit != null && hit.gameObject != this.gameObject && hit.CompareTag(this.tag);
    }

    bool IsEnemyAt(Vector3 pos)
    {
        Collider2D hit = Physics2D.OverlapCircle(pos, 0.1f);
        return hit != null && hit.gameObject != this.gameObject && !hit.CompareTag(this.tag);
    }
}
