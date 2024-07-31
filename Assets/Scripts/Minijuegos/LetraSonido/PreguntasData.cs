using UnityEngine;

[CreateAssetMenu(fileName = "Nuevas PreguntasData", menuName = "PreguntasData")]
public class PreguntasData : ScriptableObject
{
    [System.Serializable]
    public struct Pregunta
    {
        public Sprite imagen;
        public AudioClip audio;
        public string[] respuestas;
        public AudioClip[] respuestasAudios;
        public int indiceRespuestaCorrecta;
    }

    public Pregunta[] preguntas;
}
