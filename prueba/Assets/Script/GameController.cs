using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using models;

public class GameController : MonoBehaviour
{
    string lineaLeida = "";
    List<PreguntaMultiple> listaPreguntasMultiples;
    List<PreguntaFalsoVerdadero> listaPreguntasFV;
    List<PreguntaAbierta> listaPreguntasAbiertas;

    List<object> preguntasDisponibles;
    int rondaActual = 1; // 1 = Fácil, 2 = Difícil
    object preguntaActual;
    string respuestaCorrecta;

    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textResp1;
    public TextMeshProUGUI textResp2;
    public TextMeshProUGUI textResp3;
    public TextMeshProUGUI textResp4;

    public GameObject panelPreguntasMultiples;
    public GameObject panelPreguntasFV;
    public GameObject panelPreguntasAbiertas;

    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;

    void Start()
    {
        listaPreguntasMultiples = new List<PreguntaMultiple>();
        listaPreguntasFV = new List<PreguntaFalsoVerdadero>();
        listaPreguntasAbiertas = new List<PreguntaAbierta>();

        preguntasDisponibles = new List<object>();

        LecturaPreguntasMultiples();
        LecturaPreguntasFalsoVerdadero();
        LecturaPreguntasAbiertas();

        CargarPreguntasFaciles();
        MostrarPregunta();
    }

    public void CargarPreguntasFaciles()
    {
        preguntasDisponibles.Clear();
        preguntasDisponibles.AddRange(listaPreguntasMultiples.FindAll(p => p.Dificultad.ToLower() == "facil"));
        preguntasDisponibles.AddRange(listaPreguntasFV.FindAll(p => p.Dificultad.ToLower() == "facil"));
        preguntasDisponibles.AddRange(listaPreguntasAbiertas.FindAll(p => p.Dificultad.ToLower() == "facil"));
    }

    public void CargarPreguntasDificiles()
    {
        preguntasDisponibles.Clear();
        preguntasDisponibles.AddRange(listaPreguntasMultiples.FindAll(p => p.Dificultad.ToLower() == "dificil"));
        preguntasDisponibles.AddRange(listaPreguntasFV.FindAll(p => p.Dificultad.ToLower() == "dificil"));
        preguntasDisponibles.AddRange(listaPreguntasAbiertas.FindAll(p => p.Dificultad.ToLower() == "dificil"));
    }

    public void MostrarPregunta()
    {
        if (preguntasDisponibles.Count == 0)
        {
            if (rondaActual == 1)
            {
                rondaActual = 2;
                CargarPreguntasDificiles();
                MostrarPregunta();
            }
            else
            {
                Debug.Log("Juego terminado.");
            }
            return;
        }

        int index = UnityEngine.Random.Range(0, preguntasDisponibles.Count);
        preguntaActual = preguntasDisponibles[index];
        preguntasDisponibles.RemoveAt(index);

        panelPreguntasMultiples.SetActive(false);
        panelPreguntasFV.SetActive(false);
        panelPreguntasAbiertas.SetActive(false);

        if (preguntaActual is PreguntaMultiple pm)
        {
            textPregunta.text = pm.Pregunta;
            textResp1.text = pm.Respuesta1;
            textResp2.text = pm.Respuesta2;
            textResp3.text = pm.Respuesta3;
            textResp4.text = pm.Respuesta4;
            respuestaCorrecta = pm.RespuestaCorrecta;

            panelPreguntasMultiples.SetActive(true);
        }
        else if (preguntaActual is PreguntaFalsoVerdadero pfv)
        {
            textPregunta.text = pfv.Pregunta;
            respuestaCorrecta = pfv.RespuestaCorrecta ? "Verdadero" : "Falso";

            panelPreguntasFV.SetActive(true);
        }
        else if (preguntaActual is PreguntaAbierta pa)
        {
            textPregunta.text = pa.Pregunta;
            respuestaCorrecta = pa.RespuestaCorrecta;

            panelPreguntasAbiertas.SetActive(true);
        }
    }

    public void ComprobarRespuesta(TextMeshProUGUI respuestaSeleccionada)
    {
        bool esCorrecta = respuestaSeleccionada.text.Equals(respuestaCorrecta, StringComparison.OrdinalIgnoreCase);
        panelCorrecto.SetActive(esCorrecta);
        panelIncorrecto.SetActive(!esCorrecta);
    }

    public void MostrarRespuesta()
    {
        textPregunta.text += "\n\nRespuesta: " + respuestaCorrecta;
    }

    public void SiguientePregunta()
    {
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        MostrarPregunta();
    }

    public void LecturaPreguntasMultiples()
    {
        try
        {
            StreamReader sr = new StreamReader("Assets/Files/ArchivoPreguntasM.txt");
            while ((lineaLeida = sr.ReadLine()) != null)
            {
                string[] linea = lineaLeida.Split("-");
                listaPreguntasMultiples.Add(new PreguntaMultiple(linea[0], linea[1], linea[2], linea[3], linea[4], linea[5], linea[6], linea[7].Trim()));
            }
            sr.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Error en preguntas múltiples: " + e);
        }
    }

    public void LecturaPreguntasFalsoVerdadero()
    {
        try
        {
            StreamReader sr = new StreamReader("Assets/Files/preguntasFalso_Verdadero.txt");
            while ((lineaLeida = sr.ReadLine()) != null)
            {
                string[] linea = lineaLeida.Split("-");
                listaPreguntasFV.Add(new PreguntaFalsoVerdadero(linea[0], linea[1].ToLower() == "true", linea[2], linea[3].Trim()));
            }
            sr.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Error en preguntas F/V: " + e);
        }
    }

    public void LecturaPreguntasAbiertas()
    {
        try
        {
            StreamReader sr = new StreamReader("Assets/Files/ArchivoPreguntasAbiertas.txt");
            while ((lineaLeida = sr.ReadLine()) != null)
            {
                string[] linea = lineaLeida.Split("-");
                listaPreguntasAbiertas.Add(new PreguntaAbierta(linea[0], linea[1], linea[2], linea[3].Trim()));
            }
            sr.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Error en preguntas abiertas: " + e);
        }
    }
}
