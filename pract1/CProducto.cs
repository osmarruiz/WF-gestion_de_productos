using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract1
{
    public class CProducto
    {
        //atributos
        private int Codigo;
        private String Nombre;
        private int Precio;
        private int Cantidad;

        //propiedades
        public int codigo
        {
            get
            {
                return Codigo;
            }
            set
            {
                this.Codigo = value;
            }
        }
        public String nombre
        {
            get
            {
                return Nombre;
            }
            set
            {
                this.Nombre = value;
            }
        }

        public int precio
        {
            get
            {
                return Precio;
            }
            set
            {
                this.Precio = value;
            }
        }

        public int cantidad
        {
            get
            {
                return Cantidad;
            }
            set
            {
                this.Cantidad = value;
            }
        }

        //metodo
        public CProducto(int cod, String nomb, int prec, int cant)
        {
            this.Codigo = cod;
            this.Nombre = nomb;
            this.Precio = prec;
            this.Cantidad = cant;

        }



    }
}
