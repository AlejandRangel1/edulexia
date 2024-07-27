using UnityEngine;

[CreateAssetMenu(fileName = "Nuevas PreguntasData", menuName = "PreguntasData")]
public class PreguntasData : ScriptableObject
{
    [System.Serializable]
    public struct Pregunta
    {
        public string preguntaText;
        public string[] respuestas;
        public Sprite imagen;
        public AudioClip audio;
        public int indiceRespuestaCorrecta;
    }

    public Pregunta[] preguntas;
}
