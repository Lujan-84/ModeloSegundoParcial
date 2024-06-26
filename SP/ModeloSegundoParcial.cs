using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archivos;

namespace SP
{
    

    public partial class ModeloSegundoParcial : Form
    {
        private Manzana _manzana;
        private Banana _banana;
        private Durazno _durazno;

        public Cajon<Manzana> c_manzanas;
        public Cajon<Banana> c_bananas;
        public Cajon<Durazno> c_duraznos;
        
        public ModeloSegundoParcial()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Nombre, Apellido");
        }

        //Crear una instancia de cada clase e inicializar los atributos del form _manzana, _banana y _durazno. 
        private void btnPunto1_Click(object sender, EventArgs e)
        {
            this._manzana = new Manzana("verde", 2, "rio negro");
            this._banana = new Banana("amarillo", 5, "ecuador");
            this._durazno = new Durazno("rojo", 2.5, 53);

            MessageBox.Show(this._manzana.ToString());
            MessageBox.Show(this._banana.ToString());
            MessageBox.Show(this._durazno.ToString());

        }

        //Métodos
        //ToString: Mostrará en formato de tipo string, la capacidad, la cantidad total de elementos, el precio total 
        //y el listado de todos los elementos contenidos en el cajón. Reutilizar código.
        //Sobrecarga de operador
        //(+) Será el encargado de agregar elementos al cajón, siempre y cuando no supere la capacidad del mismo.
        private void btnPunto2_Click(object sender, EventArgs e)
        {
            this.c_manzanas = new Cajon<Manzana>(1.58, 3);
            this.c_bananas = new Cajon<Banana>(15.96, 4);
            this.c_duraznos = new Cajon<Durazno>(21.5, 1);

            this.c_manzanas += new Manzana("roja", 1, "neuquen");
            this.c_manzanas += this._manzana;        
            this.c_manzanas += new Manzana("amarilla", 3, "san juan");

            this.c_bananas += new Banana("verde", 3, "brasil");
            this.c_bananas += this._banana;

            this.c_duraznos += this._durazno;

            MessageBox.Show(this.c_manzanas.ToString());
            MessageBox.Show(this.c_bananas.ToString());
            MessageBox.Show(this.c_duraznos.ToString());

        }

        //Implementar (implicitamente) ISerializar en Cajon y manzana
        //Implementar (explicitamente) IDeserializar en manzana
        //Los archivos .xml guardarlos en el escritorio
        private void btnPunto3_Click(object sender, EventArgs e)
        {
            Fruta aux = null;
            
            string pathEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathManzana = Path.Combine(pathEscritorio, "Manzana.xml");
            string pathCajon = Path.Combine(pathEscritorio, "Cajon.xml");
          
            // AGREGAR
            // Serealizacion implicita de manzana
            if (this._manzana.Xml(pathManzana))
            {
                MessageBox.Show("Manzana serializada OK");
            }
            else
            {
                MessageBox.Show("NO Serializado");
            }

            // Deserealizacion explicita de manzana
            if (((IDeserializar)this._manzana).Xml(pathManzana, out aux))
            {
                MessageBox.Show("Manzana deserializada OK");
                // Agregar
                MessageBox.Show(((Manzana)aux).ToString());
            }
            else
            {
                MessageBox.Show("NO Deserializado");
            }
            
            // Serealizacion de cajon de manzanas
            if (this.c_manzanas.Xml(pathCajon))
            {
                MessageBox.Show("Cajon de Manzanas serializado OK");
            }
            else
            {
                MessageBox.Show("NO Serializado");
            }

        }

        //Si se intenta agregar frutas al cajón y se supera la cantidad máxima, se lanzará un CajonLlenoException, 
        //cuyo mensaje explicará lo sucedido.
        private void btnPunto4_Click(object sender, EventArgs e)
        {
            
            //implementar estructura de manejo de excepciones
            try
            {
                c_duraznos += _durazno;
            }
            catch(CajonLlenoException ex)
            {
                MessageBox.Show(ex.Message);
            }
            // AGREGAR
            
        }

        //Si el precio total del cajon supera los 55 pesos, se disparará el evento EventoPrecio. 
        //Diseñarlo (de acuerdo a las convenciones vistas) en la clase cajon. 
        //Crear el manejador necesario para que se imprima en un archivo de texto: 
        //la fecha (con hora, minutos y segundos) y el total del precio del cajón en un nuevo renglón.
        private void btnPunto5_Click(object sender, EventArgs e)
        {
            //Asociar manejador de eventos y crearlo en la clase Manejadora (de instancia).
            // Llamar a la excepcion correspondiente
            // AGREGAR
            c_bananas.eventoPrecio += new PrecioExtendido(new ManejadorEventos().Manejadora);

            try
            {
                this.c_bananas += new Banana("verde", 2, "argentina");

                this.c_bananas += new Banana("amarilla", 4, "ecuador");

                MessageBox.Show(this.c_bananas.PrecioTotal.ToString());
            }

            catch (CajonLlenoException  ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Obtener de la base de datos (ModeloSegundoParcial) el listado de frutas:
        //frutas { id(autoincremental - numérico) - nombre(cadena) - peso(numérico) - precio(numérico) }. 
        //Invocar al método ObtenerListadoFrutas.
        // Mostrarlo por mensaje de dialogo
        private void btnPunto6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ObtenerListadoFrutas());
        }

        //Agregar en la base de datos las frutas del formulario (_manzana, _banana y _durazno).
        //Invocar al metodo AgregarFrutas():bool
        private void btnPunto7_Click(object sender, EventArgs e)
        {
            // AGREGAR
            if(AgregarFrutas(this))
            {
                MessageBox.Show("Se agregaron las frutas a la Base de Datos");
            }
            else
            {
                MessageBox.Show("NO se agregaron las frutas a la Base de Datos");
            }           
        }

        //Obtener de la base de datos (msp_lab_II) el listado de frutas:
        //frutas { id(autoincremental - numérico) - nombre(cadena) - peso(numérico) - precio(numérico) }. 
        private static string ObtenerListadoFrutas()
        {
            string conexionString = @"Data Source=DESKTOP-EKB4GBA\SQLEXPRESS;Initial Catalog=ModeloSegundoParcial;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(conexionString);
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                using (SqlCommand sqlCom = new SqlCommand())
                {                    
                    sqlCom.Connection = conexion;
                    sqlCom.CommandType = System.Data.CommandType.Text;
                    sqlCom.CommandText = "SELECT id, nombre, peso, precio FROM frutas";
                    conexion.Open();

                    using (SqlDataReader reader = sqlCom.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stringBuilder.AppendLine("Id: " + reader["id"].ToString() + " " +
                                "Nombre: " + reader["nombre"].ToString() + " " +
                                "Peso: " + reader["peso"].ToString() + " " +
                                "Precio: " + reader["precio"].ToString() + "\n\n");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                stringBuilder.Clear();
                stringBuilder.AppendLine("Ocurrió un error al acceder a la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                stringBuilder.Clear();
                stringBuilder.AppendLine("Ocurrió un error inesperado: " + ex.Message);
            }
            return stringBuilder.ToString();
        }

        //Agregar en la base de datos las frutas del formulario (_manzana, _banana y _durazno).
        private static bool AgregarFrutas(ModeloSegundoParcial frm)
        {
            string conexionString = @"Data Source=DESKTOP-EKB4GBA\SQLEXPRESS;Initial Catalog=ModeloSegundoParcial;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(conexionString);
            
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    conexion.Open();
                    command.Connection = conexion;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO frutas (nombre, peso, precio) " +
                        "VALUES (@nombreManzana, @pesoManzana, @precioManzana)," +
                        "(@nombreBanana, @pesoBanana, @precioBanana)," +
                        "(@nombreDurazno, @pesoDurazno, @precioDurazno)";
                    command.Parameters.AddWithValue("@nombreManzana", frm._manzana.Nombre);
                    command.Parameters.AddWithValue("@pesoManzana", frm._manzana.Peso);
                    command.Parameters.AddWithValue("@precioManzana", frm.c_manzanas.PrecioTotal/frm.c_manzanas.Elementos.Count);
                    command.Parameters.AddWithValue("@nombreBanana", frm._banana.Nombre);
                    command.Parameters.AddWithValue("@pesoBanana", frm._banana.Peso);
                    command.Parameters.AddWithValue("@precioBanana", frm.c_bananas.PrecioTotal/frm.c_bananas.Elementos.Count);
                    command.Parameters.AddWithValue("@nombreDurazno", frm._durazno.Nombre);
                    command.Parameters.AddWithValue("@pesoDurazno", frm._durazno.Peso);
                    command.Parameters.AddWithValue("@precioDurazno", frm.c_duraznos.PrecioTotal/ frm.c_duraznos.Elementos.Count);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }   
}
