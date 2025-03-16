using UnityEngine;

namespace models
{
    public class PreguntaAbierta
    {
        private string pregunta;
        private string respuestaCorrecta;
        private string versiculo;
        private string dificultad;

        public PreguntaAbierta() { }

        public PreguntaAbierta(string pregunta, string respuestaCorrecta, string versiculo, string dificultad)
        {
            this.pregunta = pregunta;
            this.respuestaCorrecta = respuestaCorrecta;
            this.versiculo = versiculo;
            this.dificultad = dificultad;
        }

        public string Pregunta { get => pregunta; set => pregunta = value; }
        public string RespuestaCorrecta { get => respuestaCorrecta; set => respuestaCorrecta = value; }
        public string Versiculo { get => versiculo; set => versiculo = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }
    }
}