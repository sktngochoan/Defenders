using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasagiSkill : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    public int hasagi_level = 1;
    public float hasagi_coolDown = 1f;
    public float hasagi_damage = 9999f;
    public float hasagi_distance = 10f;
    public float hasagi_speed = 10f;
    public bool isCooldown = false;
    private GameObject hasagi;
    

    public void ActivateHasagiSkill()
    {
        if (isCooldown)
        {
            return;
        }
        var horizontalMoveJoystick = joystick.Horizontal;
        var verticalMoveJoystick = joystick.Vertical;

        if (horizontalMoveJoystick == 0 && verticalMoveJoystick == 0)
        {
            var prefab = Resources.Load("HasagiSkills") as GameObject;
            hasagi = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y,0), Quaternion.identity);
            Rigidbody2D ballRigidbody = hasagi.GetComponent<Rigidbody2D>();
            ballRigidbody.AddForce(Vector3.right * hasagi_speed, ForceMode2D.Impulse);
        }
        else
        {
            var prefab = Resources.Load("HasagiSkills") as GameObject;
            Vector2 forceDirection = new Vector2(horizontalMoveJoystick, verticalMoveJoystick).normalized;
            hasagi = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Rigidbody2D ballRigidbody = hasagi.GetComponent<Rigidbody2D>();
            ballRigidbody.AddForce(forceDirection * hasagi_speed, ForceMode2D.Impulse);
        }
        StartCooldown();
    }

    private void StartCooldown()
    {
        isCooldown = true;
        Invoke("HasagiFinishCooldown", hasagi_coolDown);
    }
    private void HasagiFinishCooldown()
    {
        isCooldown = false;
        Destroy(hasagi);
    }
}
