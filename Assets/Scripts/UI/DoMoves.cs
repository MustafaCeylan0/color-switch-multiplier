using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoMoves : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float jumpX;
    [SerializeField] private float jumpY;
    [SerializeField] private float jumpInterval;

    private Vector2 jumpVectorRight;
    private Vector2 jumpVectorLeft;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        jumpVectorRight = new Vector2(jumpX, jumpY);
        jumpVectorLeft = new Vector2(-jumpX, jumpY);
        StartCoroutine(jumpRightLeft());
    }

    private IEnumerator jumpRightLeft()
    {
        while (true)
        {
            _rigidbody2D.velocity = jumpVectorRight;
            yield return new WaitForSeconds(jumpInterval);
            _rigidbody2D.velocity = jumpVectorLeft;
            yield return new WaitForSeconds(jumpInterval);
        }
    }
}