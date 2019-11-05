using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRing : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particleRing;
    private float[] particleAngle;//粒子偏转角
    private float[] particleRadius;//粒子半径
    private float[] convergent;//收束半径
    private float[] divergent;//发散半径
    public int particleNum = 2000;
    public float radius = 5.0f;
    public float minRadius = 3.0f;
    public float speed = 0.1f;
    public int level = 10;
    public int count = 0;
    public bool conFlag = false;//控制收束
    public bool divFlag = false;//控制发散
    public float convergentSpeed = 3.0f;

    void Start()
    {
        particleRing = new ParticleSystem.Particle[particleNum];
        particleAngle = new float[particleNum];
        particleRadius = new float[particleNum];
        convergent = new float[particleNum];
        divergent = new float[particleNum];
        particleSystem.maxParticles = particleNum;
        particleSystem.Emit(particleNum);
        particleSystem.GetParticles(particleRing);
        for (int i = 0; i < particleNum; i++)
        {
            particleAngle[i] = Random.Range(0.0f, 360.0f);//随机角度
            float tempMin = Random.Range(minRadius * 0.8f, minRadius * 1.2f);
            float tempMax = Random.Range(radius * 0.8f, radius * 1.2f);
            particleRadius[i] = Random.Range(tempMin, tempMax);//随机半径，形成环形.
            convergent[i] = particleRadius[i]*0.6f;
            divergent[i] = particleRadius[i];
       
            float rad = particleAngle[i] / 180 * Mathf.PI;
            particleRing[i].position = new Vector3(particleRadius[i] * Mathf.Cos(rad), 
                particleRadius[i] * Mathf.Sin(rad), 0.0f);
        }
        particleSystem.SetParticles(particleRing, particleNum);
    }

    void Update()
    {
        count++;
        if (count % 100 == 0)
        {
            //收束或发散
            if (conFlag)
            {
                conFlag = false;
                divFlag = true;
            }
            else
            {
                conFlag = true;
                divFlag = false;
            }
        }

        for (int i = 0; i < particleNum; i++)
        {
            if (i % 2 == 0)
            { 
                particleAngle[i] += (i % level + 1) * speed;//逆时针
                particleAngle[i] %= 360;
            }
            else
            {
                particleAngle[i] -= (i % level + 1) * speed;//顺时针
                if (particleAngle[i] < 0) particleAngle[i] += 360;
            }

            

            if (conFlag)//收束
            {
                if(particleRadius[i]>convergent[i])
                    particleRadius[i] -= convergentSpeed * (particleRadius[i] / convergent[i]) * Time.deltaTime;

            }
            if(divFlag)//发散
            {
                if (particleRadius[i] < divergent[i])
                {
                    particleRadius[i] += convergentSpeed * (divergent[i] / particleRadius[i]) * Time.deltaTime;
                }
            }
            
            float rad = particleAngle[i] / 180 *  Mathf.PI;
            particleRing[i].position = new Vector3(particleRadius[i] * Mathf.Cos(rad),
                particleRadius[i] * Mathf.Sin(rad), 0.0f);
        }
        particleSystem.SetParticles(particleRing, particleNum);
    }
}
