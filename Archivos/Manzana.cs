using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Archivos
{
    public class Manzana: Fruta, IDeserializar , ISerializar
    {
        protected string _provinciaOrigen;
        

        public string Nombre
        {
            get { return "Manzana"; }
        }

        public string ProvinciaOrigen
        {
            get { return _provinciaOrigen; }
            set { _provinciaOrigen = value; }
        }

        public override bool TieneCarozo
        {
            get { return false; }
            set { }
        }

        public Manzana()
        {

        }

        public Manzana (string color, double peso, string provincia)
            : base(color, peso)
        {
            _provinciaOrigen = provincia;
        }

        public override string FrutasToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.FrutasToString());
            stringBuilder.AppendLine($"Provincia de Origen: {this.ProvinciaOrigen}");
            stringBuilder.AppendLine($"Tiene Carozo: {this.TieneCarozo}");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return FrutasToString();
        }

        bool IDeserializar.Xml(string path, out Fruta fruta)
        {
            try
            {
                XmlSerializer deserializador = new XmlSerializer(typeof(Manzana));
                using(StreamReader reader = new StreamReader(path))
                {
                    fruta = (Fruta)deserializador.Deserialize(reader);
                    return true;
                }
           
            }
            catch (Exception)
            {
                fruta = null;
                return false;
            }
        
        }

        public bool Xml(string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Manzana));
                using (StreamWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, this);
                    return true;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}
