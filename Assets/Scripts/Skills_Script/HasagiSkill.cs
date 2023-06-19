using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasagiSkill : MonoBehaviour
{

    public int hasagi_level = 1;
    public float hasagi_coolDown = 5f;
    public float hasagi_damage = 9999f;
    public float hasagi_distance = 10f;
    public float hasagi_speed = 10f;
    public bool isCooldown = false;
    public string prefabPath = "Prefabs/HasagiSkill"; // Đường dẫn của Prefab trong thư mục "Resources/Assets"
    public GameObject ball;
    public void ActivateHasagiSkill()
    {
        if (isCooldown)
        {
            Debug.Log("HasagiSkill is on cooldown!");
            return;
        }

        // Thực hiện chức năng của skill Hasagi ở đây, ví dụ:
        // Tạo lốc xoáy gió và gây sát thương vô cực
        var prefab = Resources.Load("HasagiSkills") as GameObject;
       
            // Tạo một instance của Prefab
            ball = Instantiate(prefab, transform.position, Quaternion.identity);
            // Các thao tác khác với Prefab sau khi tải lên
            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
            // Áp dụng lực để bắn lên
            ballRigidbody.AddForce(Vector2.up * hasagi_speed, ForceMode2D.Impulse);

        // Bắt đầu thời gian hồi skill
        StartCooldown();
        //FinishCooldown();
    }

    private void StartCooldown()
    {
        isCooldown = true;
        Invoke("HasagiFinishCooldown", hasagi_coolDown);
        
    }

    private void HasagiFinishCooldown()
    {
        isCooldown = false;
        Debug.Log("Hasagi skill is ready!");
        Destroy(ball);
    }
}
