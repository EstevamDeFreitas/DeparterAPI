using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CategoriaDTO
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
    }
}
