using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public abstract class Fruta
    {
        protected string _color;
        protected double _peso;

        public abstract bool TieneCarozo { get; set; }

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public double Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }

        public Fruta()
        {

        }
        public Fruta(string color, double peso)
        {
            _color = color;
            _peso = peso;
        }

        public virtual string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Color: {this.Color}");
            sb.AppendLine($"Peso: {this.Peso}");
            return sb.ToString();
        }
    }
}
