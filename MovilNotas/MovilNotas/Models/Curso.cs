using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
    public class CursoFlat
    {
        [JsonProperty("profesor_materia_id")]
        public int ProfesorMateriaId { get; set; }

        [JsonProperty("id_materia")]
        public int IdMateria { get; set; }

        [JsonProperty("id_jornada")]
        public int IdJornada { get; set; }   // Importante: usar ID

        [JsonProperty("id_nivel")]
        public int IdNivel { get; set; }     // Importante: usar ID

        [JsonProperty("id_paralelo")]
        public int IdParalelo { get; set; }  // Importante: usar ID

        [JsonProperty("carrera")]
        public string Carrera { get; set; }

        [JsonProperty("materia")]
        public string Materia { get; set; }

        [JsonProperty("jornada")]
        public string Jornada { get; set; }   // Mantén el nombre para mostrar

        [JsonProperty("nivel")]
        public string Nivel { get; set; }     // Mantén el nombre para mostrar

        [JsonProperty("paralelo")]
        public string Paralelo { get; set; }  // Mantén el nombre para mostrar
    }




    public class Nivel
	{
		public int NivelId { get; set; }
		public string Nombre { get; set; }
		public List<Jornada> Jornadas { get; set; }
	}

	public class Jornada
	{
		public int JornadaId { get; set; }
		public string Nombre { get; set; }
		public List<Paralelo> Paralelos { get; set; }
	}

	public class Paralelo
	{
		public int ParaleloId { get; set; }
		public string Nombre { get; set; }
		public List<Materia> Materias { get; set; }
	}

	public class Materia
	{
		public int MateriaId { get; set; }
		public string Nombre { get; set; }
	}

}

