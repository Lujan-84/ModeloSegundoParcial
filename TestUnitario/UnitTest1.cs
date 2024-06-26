using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Archivos;

namespace TestUnitario
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PrecioTotal_DebeDispararEventoPrecio_CuandoElTotalExcede55()
        {
            // Arrange
            Cajon<Manzana> cajon = new Cajon<Manzana>(10, 10);
            bool eventoDisparado = false;
            double precioTotal = 0;

            cajon.eventoPrecio += (total) =>
            {
                eventoDisparado = true;
                precioTotal = (double)total;
            };

            // Act
            cajon += new Manzana("roja", 1.2, "neuquen");
            cajon += new Manzana("verde", 1.3, "rio negro");
            cajon += new Manzana("amarilla", 1.1, "san juan");
            cajon += new Manzana("roja", 1.4, "mendoza");
            cajon += new Manzana("verde", 1.5, "salta");
            cajon += new Manzana("amarilla", 1.0, "jujuy");
            precioTotal = cajon.PrecioTotal;
            // Assert
            Assert.IsTrue(eventoDisparado);
            Assert.AreEqual(60, precioTotal, 0.01);
        }

        // Generar prueba que debe lanzar la excepcion cuando el cajon este lleno
        // TestMethod: AgregarFruta_DebeLanzarCajonLlenoException_CuandoElCajonEstaLleno
        [TestMethod]
        [ExpectedException(typeof(CajonLlenoException))]
        public void AgregarFruta_DebeLanzarCajonLlenoException_CuandoElCajonEstaLleno()
        {
            // Arrange
            Cajon<Banana> cajon = new Cajon<Banana>(12, 2);
            cajon += new Banana("amarilla", 150, "Ecuador");
            cajon += new Banana("verde", 180, "Brasil");

            // Act
            cajon += new Banana("amarilla", 169, "Ecuador");

            //Assert
            // Manejado por ExpectedException
        }
    }
}
