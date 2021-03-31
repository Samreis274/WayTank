using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Para distrubuir as informações entre uma classe para outra
[System.Serializable]
public struct Link
{
    //varial de controle de direção(UNI so para ir, BI para ir e voltar)
    public enum direction { UNI, BI }
    //primeiro ponto do link
    public GameObject node1;
    //segundo ponto do link
    public GameObject node2;
    // varial de direção
    public direction dir;
}
public class Wpmanager1 : MonoBehaviour
{   
    //Array dos way points
    public GameObject[] waypoints;
    //Array dos links  
    public Link[] links;
    //Criando o objeto graph
    public Graph graph = new Graph();
    // Start is called before the first frame update
    void Start()
    {
        //contagem dos waypoints
        if (waypoints.Length > 0)
        {   
            //Para completar o array dos waypoints
            foreach (GameObject wp in waypoints)
            {
                //Criar as linhas entro os ways no array dos waypoints
                graph.AddNode(wp);
            }
            //Para completar o array dos Links
            foreach (Link l in links)
            {
                //Para criar as arestas entre os links
                graph.AddEdge(l.node1, l.node2);
                //Condicao caso as arestas entre os links forem BI
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }// Update is called once per frame
    void Update()
    {
        //desenha as linhas e as arestar entre os pontos
        graph.debugDraw();
    }
}