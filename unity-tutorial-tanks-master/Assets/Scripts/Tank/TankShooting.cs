using UnityEngine;
using UnityEngine.UI;
using System;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    public Transform OtherTank;
    public float initVelocity = 30f;
    public float gravity = -1f;
    public float angle;

    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }


    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
		m_AimSlider.value = m_MinLaunchForce;

		if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired) {
			// At max charge, not yet fired.
			m_CurrentLaunchForce = m_MaxLaunchForce;
			Fire();

		} else if (Input.GetButtonDown(m_FireButton)) {
			// Have we pressed the Fire button for the first time?
			m_Fired = false;
			m_CurrentLaunchForce = m_MinLaunchForce;

			m_ShootingAudio.clip = m_ChargingClip;
			m_ShootingAudio.Play();

		} else if (Input.GetButton(m_FireButton) && !m_Fired) {
			// Holding the fire button, not yet fired.
			m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

			m_AimSlider.value = m_CurrentLaunchForce;

		} else if (Input.GetButtonUp(m_FireButton) && !m_Fired) {
			// We releasted the fire button, having not fired yet.
			Fire();

		}
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
		m_Fired = true;

		Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        //Angle();
        //m_FireTransform.Rotate(angle, 0.0f, 0.0f);

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

		m_ShootingAudio.clip = m_FireClip;
		m_ShootingAudio.Play();

		m_CurrentLaunchForce = m_MinLaunchForce;
    }

    private void Angle()
    {
        initVelocity = 30f;
        // Calculate the distances from the projedctile to the objective tank
        float distanceX = Vector3.Distance(m_FireTransform.position, OtherTank.position);
        float distanceY = OtherTank.position.y;

        // Calculate the angle considering objective x and y.
        float squareVel = initVelocity * initVelocity;
        float quadVel = initVelocity * initVelocity * initVelocity * initVelocity;
        float squareX = distanceX * distanceX;
        float tanAngle = (squareVel + (float)Math.Sqrt((double)(quadVel - gravity * (gravity * squareX + 2 * distanceY * squareVel)))) / (gravity * distanceX);
        angle = (float)Math.Atan(tanAngle);
    }

}