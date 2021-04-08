using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Followpath : MonoBehaviour
{
    //tranform dos pontos
    Transform goal;
    //velocidade do objeto
    float speed = 5.0f;
    //contor na hora da curva
    float accuracy = 1.0f;
    //velocidade da curva 
    float rotSpeed = 2.0f;

    //criação do objeto wpmanager
    public GameObject WPManager;
    //array do waypoints
    GameObject[] wps;
    //pega os nos nodes criados no wpManager
    GameObject currentNode;
    //Pontos sempre voltar a 0
    int currentWP = 0;
    //pega o Graph
    Graph g;

    // Start is called before the first frame update
    public void Start()
    {
            //pega o componente waypoints do WpManager
            wps = WPManager.GetComponent<Wpmanager1>().waypoints;
           //Pega o componente graph do WpManager
            g = WPManager.GetComponent<Wpmanager1>().graph;
            //Declara q o wps sempre sera 0
            currentNode = wps[0];
        }
    //Leva o objeto para um ponto especifico seguindo os waypoint, no caso heliporto
    public void GoToHeli()
        {
            g.AStar(currentNode, wps[1]);
            currentWP = 0;
        }
        //Leva o objeto para um ponto especifico seguindo os waypoints, no caso as ruinas 
        public void GoToRuin()
        {
            g.AStar(currentNode, wps[5]);
            currentWP = 0;
        }
        public void GoToReser()
        {
            g.AStar(currentNode, wps[8]);
         currentWP = 0;
        }


    void LateUpdate()
    {
        //Da a condicao se o Graph for igual a zero e o currentWP for igual a zero roda a condiçao
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;
        //O nó que estará mais próximo neste momento
        currentNode = g.getPathPoint(currentWP);
        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
        if (Vector3.Distance(
        g.getPathPoint(currentWP).transform.position,
        transform.position) < accuracy)
        {
            //adiciona um ponto no currentWP
            currentWP++;
        }
        //condicao para current for menor q o graph
        if (currentWP < g.getPathLength())
        {
            //Controle de movimentacao do objeto para os pontos
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,this.transform.position.y, goal.position.z);
           
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
