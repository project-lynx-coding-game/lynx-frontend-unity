using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Agent : Object
{
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public Animator Animator;

    private float epsilon = 0.0001f;

    public async Task MoveTo(Vector3 position)
    {
        float step = speed * Time.deltaTime;

        Animator.SetBool("walking", true);
        while (Vector3.Distance(transform.position, position) > epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, step);
            await Task.Yield();
        }

        Animator.SetBool("walking", false);
    }

    public async Task LookInDirection(Vector3 direction)
    {
        float step = rotationSpeed * Time.deltaTime;
        direction.y = 0; // keep the direction strictly horizontal

        while (Vector3.Dot(transform.forward, direction) < 1 - epsilon)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
            await Task.Yield();
        }
    }

    public void SetAnimatorBool(string name, bool value)
    {
        Animator.SetBool(name, value);
    }
}
