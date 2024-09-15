using UnityEngine;

public class AirplaneMover : MonoBehaviour
{
    // ���������� ������ �������� ��������� ���������. ����� �������� ����� �� ���������, � ����� �� ������������ ��� ������������ �����
    [SerializeField] private Vector3 moveDirection = Vector3.forward; // ����������� ��������
    [SerializeField] private float moveSpeed = 5f; // �������� ��������
    [SerializeField] private float rollSpeedMin = 5f; // ����������� �������� �����
    [SerializeField] private float rollSpeedMax = 15f; // ������������ �������� �����
    [SerializeField] private float pitchSpeedMin = 3f; // ����������� �������� �����������
    [SerializeField] private float pitchSpeedMax = 10f; // ������������ �������� �����������
    [SerializeField] private float rollAmount = 15f; // ������������ ���� �����
    [SerializeField] private float pitchAmount = 5f; // ������������ ���� �����������

    private float rollOffset;
    private float pitchOffset;
    private float rollSpeed;
    private float pitchSpeed;

    private void Start()
    {
        // ������������� ��������� �������� ��� �������� � ��������� ��������
        rollSpeed = Random.Range(rollSpeedMin, rollSpeedMax);
        pitchSpeed = Random.Range(pitchSpeedMin, pitchSpeedMax);
        rollOffset = Random.Range(0f, Mathf.PI * 2); // ��������� ��������� �������� ��� �����
        pitchOffset = Random.Range(0f, Mathf.PI * 2); // ��������� ��������� �������� ��� �����������
    }

    void Update()
    {
        // ������� ������
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ��������� ������ �����
        rollOffset += rollSpeed * Time.deltaTime;
        float roll = Mathf.Sin(rollOffset) * rollAmount;

        // ��������� ������ �����������
        pitchOffset += pitchSpeed * Time.deltaTime;
        float pitch = Mathf.Sin(pitchOffset) * pitchAmount;

        // ��������� ���� � �����������
        transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y, roll);
    }
}
