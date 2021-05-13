using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    public List<Transform> wayPoint;
    public int curWayPoint;
    float speed;

    NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
            Patrol();
    }
    void Patrol()
    {
        if(wayPoint.Count > 1)
        {
            if(wayPoint.Count > curWayPoint)
            {
                agent.SetDestination(wayPoint[curWayPoint].position);  //Говорим, что нужно дойти до точки
                float distance = Vector3.Distance(transform.position, wayPoint[curWayPoint].position); //Узнаем дистанцию до точки

                if(distance > 2.5f)  //Увеличиваем скорость
                {
                    speed += Time.deltaTime * 3;
                }
                else if (distance <= 2.5f && distance >= 1f)  //Поворот, когда близко подходим
                {
                    Vector3 direction = (wayPoint[curWayPoint].position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
                }
                else
                {
                    curWayPoint++;
                }
            }
            else if(wayPoint.Count == curWayPoint)
            {
                curWayPoint = 0;
            }
        }


        else if(wayPoint.Count == 1)
        {
            agent.SetDestination(wayPoint[0].position);
            float distance = Vector3.Distance(transform.position, wayPoint[curWayPoint].position);

            if (distance > 1.5f)
            {
                agent.isStopped = false;
                speed += Time.deltaTime * 3;
            }
            else
            {
                agent.isStopped = true;
                speed -= Time.deltaTime * 5;

                Vector3 direction = (wayPoint[0].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
            }
        }
        else
        {
            agent.isStopped = true;
            speed -= Time.deltaTime * 5;
        }
        speed = Mathf.Clamp(speed, 0, 1);
    }
}
