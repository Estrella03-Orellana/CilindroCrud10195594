using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilindroCrud10195594
{
    [Table("Cilindro")]
    public class Cilindro
    {
            [PrimaryKey]
            [AutoIncrement]
            [Column("id")]
            public int Id { get; set; }
            [Column("radio")]
            public string Radio { get; set; }
            [Column("altura")]
            public string Altura { get; set; }
            [Column("resultadoArea")]
            public string ResultadoArea { get; set; }
            [Column("resultadoVolumen")]
            public string ResultadoVolumen { get; set; }
    }
    }

