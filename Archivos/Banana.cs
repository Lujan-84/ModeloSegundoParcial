using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Archivos
{
    public class Banana: Fruta
    {
        protected string _paisOrigen;

        public string Nombre
        {
            get { return "Banana"; }
        }

        public override bool TieneCarozo
        {
            get { return false; }
            set {  }
        }

        public Banana(string color, double peso, string paisOrigen)
            : base(color, peso)
        {
            _paisOrigen = paisOrigen;
        }

        public override string FrutasToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.FrutasToString());
            stringBuilder.AppendLine($"Pais de Origen: {_paisOrigen}");
            stringBuilder.AppendLine($"Tiene Carozo: {this.TieneCarozo}");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return FrutasToString();
        }
    }
}
