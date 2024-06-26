using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public class Durazno : Fruta
    {
        protected int _cantPelusa;

        public string Nombre
        {
            get { return "Durazno"; }
        }

        public override bool TieneCarozo
        {
            get { return true; }
            set { }
        }

        public Durazno(string color, double peso, int cantidadPelusa)
            : base (color, peso)
        {
            _cantPelusa = cantidadPelusa;
        }

        public override string FrutasToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.FrutasToString());
            stringBuilder.AppendLine($"Tiene Carozo: {this.TieneCarozo}");
            stringBuilder.AppendLine($"Cantidad Pelusa: {_cantPelusa}");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return FrutasToString();
        }
    }
}
