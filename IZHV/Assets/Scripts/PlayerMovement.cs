using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject collector;
    private PlayerStatistics playerStatistics;

    private Rigidbody2D rb;
    private Vector2 movement;
    public int moveSpeed;
    private int currentMoveSpeed;
    private PlayerInput playerInput;

    private InputAction slowAction;
    void Start()
    {
        playerStatistics = GetComponent<PlayerStatistics>();
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;

        playerInput = GetComponent<PlayerInput>();
        slowAction = playerInput.actions["slow"];
    }

    void Update()
    {
        if (slowAction.IsPressed()) currentMoveSpeed = moveSpeed - 2;
        else currentMoveSpeed = moveSpeed;

        if (slowAction.WasReleasedThisFrame()) collector.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (playerStatistics.playerStatus != PlayerStatus.ALIVE) return;
        rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime);
    }
    public void OnSlow()
    {
        collector.SetActive(true);
    }
    public void OnBreak()
    {
        if (playerStatistics.playerStatus != PlayerStatus.BUBBLED) return;
        playerStatistics.RemoveWetnessWhenInBubble();
    }
    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();

    }
}
