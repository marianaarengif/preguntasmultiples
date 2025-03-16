namespace models
{
    public class PreguntaMultiple
    {
        public string Pregunta { get; set; }
        public string Respuesta1 { get; set; }
        public string Respuesta2 { get; set; }
        public string Respuesta3 { get; set; }
        public string Respuesta4 { get; set; }
        public string RespuestaCorrecta { get; set; }
        public string Versiculo { get; set; }
        public string Dificultad { get; set; }

        public PreguntaMultiple(string pregunta, string respuesta1, string respuesta2, string respuesta3, string respuesta4, string respuestaCorrecta, string versiculo, string dificultad)
        {
            Pregunta = pregunta;
            Respuesta1 = respuesta1;
            Respuesta2 = respuesta2;
            Respuesta3 = respuesta3;
            Respuesta4 = respuesta4;
            RespuestaCorrecta = respuestaCorrecta;
            Versiculo = versiculo;
            Dificultad = dificultad;
        }
    }
}
