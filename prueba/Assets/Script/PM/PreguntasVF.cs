using UnityEngine;

namespace models
{
    public class PreguntaFalsoVerdadero
    {
        private string pregunta;
        private bool respuestaCorrecta; // true = Verdadero, false = Falso
        private string versiculo;
        private string dificultad;

        public PreguntaFalsoVerdadero() { }

        public PreguntaFalsoVerdadero(string pregunta, bool respuestaCorrecta, string versiculo, string dificultad)
        {
            this.pregunta = pregunta;
            this.respuestaCorrecta = respuestaCorrecta;
            this.versiculo = versiculo;
            this.dificultad = dificultad;
        }

        public string Pregunta { get => pregunta; set => pregunta = value; }
        public bool RespuestaCorrecta { get => respuestaCorrecta; set => respuestaCorrecta = value; }
        public string Versiculo { get => versiculo; set => versiculo = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }
    }
}