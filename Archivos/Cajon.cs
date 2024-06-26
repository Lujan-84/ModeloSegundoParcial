using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Runtime.CompilerServices;

namespace Archivos
{
    public delegate void PrecioExtendido(object sender);
   
    public class Cajon<T> : ISerializar
    {
        public event PrecioExtendido eventoPrecio;
        protected int _capacidad;
        protected List<T> _elementos;
        protected double _precioUnitario;
        


        public List<T> Elementos
        {
            get { return _elementos; }
        }

        public int Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }
        public double PrecioTotal
        {
            get
            {
                double total = _precioUnitario * _elementos.Count;
                if (total > 55)
                {
                    
                    eventoPrecio.Invoke(total);
                }
               
                return total; 
            }
        }

        
        public Cajon()
        {
            _elementos = new List<T>();
        }

        public Cajon(double precio, int capacidad) : this(capacidad)
        {
            _precioUnitario = precio;
            
        }

        public Cajon(int capacidad):this()
        {
            _capacidad = capacidad;
        }

        public static Cajon<T> operator +(Cajon<T> cajon, T elemento)
        {
            if (cajon._elementos.Count < cajon._capacidad)
            {
                cajon._elementos.Add(elemento);
            }
            else
            {
                throw new CajonLlenoException("El cajón ya se encuentra lleno");
            }
            return cajon;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Capacidad: {_capacidad}");
            sb.AppendLine($"Precio Totla: {PrecioTotal}");
            sb.AppendLine($"Cantidad elementos: {_elementos.Count}\n");          
            foreach ( T elemnto in _elementos )
            {
                sb.AppendLine( elemnto.ToString() );
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public bool Xml(string path)
        {
            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Cajon<T>));

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
