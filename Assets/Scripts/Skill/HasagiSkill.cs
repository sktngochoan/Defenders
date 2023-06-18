using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasagiSkill : MonoBehaviour, ISkill
{
    public GameObject hasagiPrefabs;
    public float hasagiSpeed = 10f;
    public string hasagiPrefabPath = "Assets/HasagiSkill";

    public void Use()
    {
        // Tạo một instance của Prefab fireball
        GameObject hasagi = Instantiate(hasagiPrefabs, transform.position, Quaternion.identity);

        // Lấy component Rigidbody2D của fireball
        Rigidbody2D rb = hasagi.GetComponent<Rigidbody2D>();

        // Thiết lập vận tốc di chuyển cho fireball
        rb.velocity = Vector2.up * hasagiSpeed;
    }
}
