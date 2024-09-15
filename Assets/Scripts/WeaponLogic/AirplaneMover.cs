using UnityEngine;

public class AirplaneMover : MonoBehaviour
{
    // привольный скрипт движения бумажного самолётика. Можно спавнить врага из самолётика, а можно их использовать как проджектайлы босса
    [SerializeField] private Vector3 moveDirection = Vector3.forward; // Направление движения
    [SerializeField] private float moveSpeed = 5f; // Скорость движения
    [SerializeField] private float rollSpeedMin = 5f; // Минимальная скорость крена
    [SerializeField] private float rollSpeedMax = 15f; // Максимальная скорость крена
    [SerializeField] private float pitchSpeedMin = 3f; // Минимальная скорость покачивания
    [SerializeField] private float pitchSpeedMax = 10f; // Максимальная скорость покачивания
    [SerializeField] private float rollAmount = 15f; // Максимальный угол крена
    [SerializeField] private float pitchAmount = 5f; // Максимальный угол покачивания

    private float rollOffset;
    private float pitchOffset;
    private float rollSpeed;
    private float pitchSpeed;

    private void Start()
    {
        // Устанавливаем случайные значения для скорости и начальных смещений
        rollSpeed = Random.Range(rollSpeedMin, rollSpeedMax);
        pitchSpeed = Random.Range(pitchSpeedMin, pitchSpeedMax);
        rollOffset = Random.Range(0f, Mathf.PI * 2); // случайное начальное значение для крена
        pitchOffset = Random.Range(0f, Mathf.PI * 2); // случайное начальное значение для покачивания
    }

    void Update()
    {
        // Двигаем объект
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Добавляем эффект крена
        rollOffset += rollSpeed * Time.deltaTime;
        float roll = Mathf.Sin(rollOffset) * rollAmount;

        // Добавляем эффект покачивания
        pitchOffset += pitchSpeed * Time.deltaTime;
        float pitch = Mathf.Sin(pitchOffset) * pitchAmount;

        // Применяем крен и покачивание
        transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y, roll);
    }
}
