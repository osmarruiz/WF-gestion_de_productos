using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pract1
{
    //elaborado por Osmar Ernesto Ruiz Amador (20-01545-0)
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //lista que almacenara los datos
        public List<CProducto> producto = new List<CProducto>();
        private void timer1_Tick(object sender, EventArgs e)
        {
            //realiza el cambio de hora en el status label
            slbHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void ordenSalir_Click(object sender, EventArgs e)
        {
            //cierra el formulario
            this.Close();
        }
        private void ordenAcercade_Click(object sender, EventArgs e)
        {
            about();
        }
        private void ordenVerBH_Click(object sender, EventArgs e)
        {
            //si la ordenverBH no esta Checked, no se visualiza la barra de herramientas
            if (!ordenVerBH.Checked)
            {
                toolStrip1.Visible = false;
            }
            else
            {
                toolStrip1.Visible = true;
            }
        }
        private void ordenVerBE_Click(object sender, EventArgs e)
        {
            //si la ordenverBE no esta Checked, no se visualiza la barra de estado
            if (!ordenVerBE.Checked)
            {
                statusStrip1.Visible = false;
            }
            else
            {
                statusStrip1.Visible = true;
            }
        }
        private void ordenNuevo_Click(object sender, EventArgs e)
        {
            nuevo_clickST();

        }
        private void ordenCancelar_Click(object sender, EventArgs e)
        {
            cancel_click(sender, e);
        }
        private void ordenGuardar_Click(object sender, EventArgs e)
        {
            guardar_click();
        }
        private void ordenTotalR_Click(object sender, EventArgs e)
        {
            total_click();
        }
        private void btNuevo_Click(object sender, EventArgs e)
        {
            nuevo_clickST();
        }
        private void btAyuda_Click(object sender, EventArgs e)
        {
            about();
        }
        private void btCancelar_Click(object sender, EventArgs e)
        {
            cancel_click(sender, e);
        }
        private void btGuardar_Click(object sender, EventArgs e)
        {
            guardar_click();
        }
        private void btBuscar_Click(object sender, EventArgs e)
        {
            // al darle click buscar, comprueba que hay datos en la lista, si hay hace un foreach la busqueda se hacer por nombre
            bool encontrado = false;
            if(producto.Count == 0)
            {
                MessageBox.Show("no hay productos");
                lbBuscar.Text = "";
            }
            else
            {
               foreach (CProducto p in producto)
                {
                    if(lbBuscar.Text == p.nombre)
                    {
                        llenar_text(p);
                        lbBuscar.Text = "";
                        encontrado = true;
                        break;
                    }
                }
               if(!encontrado)
                {
                    MessageBox.Show("No se encontro");
                }
                lbBuscar.Text = "";
            }
        }
        private void cmProducto_Click(object sender, EventArgs e)
        {
            nuevo_clickST();
        }
        private void cmCancelar_Click(object sender, EventArgs e)
        {
            cancel_click(sender,e);
        }
        private void cmGuardar_Click(object sender, EventArgs e)
        {
            guardar_click();
        }
        private void cmTr_Click(object sender, EventArgs e)
        {
            total_click();
        }
        private void btnPrimero_Click(object sender, EventArgs e)
        {
            //desplazamiento al primero, utilizo First para realizarlo
            if (producto.Count != 0)
            {
                llenar_text(producto.First());

            }
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            //anterior, utilizo el codigo de registro -1 para desplazar hacia atras de la lista
            if (producto.Count != 0)
            {
                int i = 0;
                i = int.Parse(txtCodigo.Text) - 1;
                i--;
                if (i >= 0)
                {
                    llenar_text(producto[i]);
                }
            }
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            //utilizo el codigo de registro para dezplazar hacia delante de la lista
            if (producto.Count != 0)
            {
                int i = 0;
                i = int.Parse(txtCodigo.Text);
                if (i < producto.Count)
                {
                    llenar_text(producto[i]);
                }
            }
        }
        private void btnUltimo_Click(object sender, EventArgs e)
        {
            //desplazamiento al ultimo, utilizo Last para realizarlo
            if (producto.Count != 0)
            {
                llenar_text(producto.Last());
            }
        }
        public void about()
        {
            //genera el formulario acerda de.. generico
            Form dlg1 = new AboutBox1();
            dlg1.ShowDialog();
        }
        public void nuevo_clickST()
        {
            //al darle click a nuevo se habilitara algunos elementos y otros se deshabilitaran
            //ordenes de menustrip
            ordenGuardar.Enabled = true;
            ordenCancelar.Enabled = true;
            ordenNuevo.Enabled = false;

            //botones de toolstrip
            btGuardar.Enabled = true;
            btCancelar.Enabled = true;
            btNuevo.Enabled = false;
            btBuscar.Enabled = false;
            lbBuscar.Enabled = false;

            //ordenes del menu contextual
            cmGuardar.Enabled = true;
            cmCancelar.Enabled = true;
            cmProducto.Enabled = false;

            //habilitar los textbox
            txtCodigo.Enabled = true;
            txtNombre.Enabled = true;
            txtNombre.ReadOnly = false;
            txtCantidad.Enabled = true;
            txtCantidad.ReadOnly = false;
            txtPrecio.Enabled = true;
            txtPrecio.ReadOnly = false;

            //desactiva los botones de desplazamiento
            btnPrimero.Enabled = false;
            btnSiguiente.Enabled = false;
            btnUltimo.Enabled = false;
            btnAnterior.Enabled = false;

            //borra los datos de los textbox
            clear();

            //genera el codigo
            generar_cod();

            

        }
        private void cancel_click(object sender, EventArgs e)
        {
            //cancela el llenado de la lista, regresando al primero si hay datos en la lista
            Point_initial();
            if (producto.Count == 0)
            {
                clear();
            }
            else
            {
                btnPrimero_Click(sender, e);
            }
          
        }
        public void guardar_click()
        {
            //crea un objeto y lo almacenae en la lista sin cumple los requisitos, regresa al punto inicial
            CProducto product;
            if(validate())
            {
                product = new CProducto(int.Parse(txtCodigo.Text),txtNombre.Text,int.Parse(txtPrecio.Text),int.Parse(txtCantidad.Text));
                producto.Add(product);
                Point_initial();
                MessageBox.Show("se guardo correctamente");
            }
            else 
            {
                MessageBox.Show("llene los datos correctamente para guardar");
                clear();
                generar_cod();

            }
        }
        public void total_click()
        {
            //si hay datos en la lista hace un calculo de todo lo recaudado en los productos
            if(producto.Count > 0)
            {
                int total = 0;
                foreach (CProducto p in producto)
                {
                    total += p.cantidad * p.precio;
                }
                MessageBox.Show("total recaudado: " +  total);
            }
            else
            {
                MessageBox.Show("No hay productos");
            }
            
           
        }
        public void llenar_text(CProducto product)
        {
            //los textbox se llenan con los datos de la lista
            txtCodigo.Text = product.codigo.ToString();
            txtNombre.Text = product.nombre;
            txtPrecio.Text = product.precio.ToString();
            txtCantidad.Text = product.cantidad.ToString();
        }
        public void generar_cod()
        {
            //genera el codigo del producto
            if (producto.Count == 0)
            {
                txtCodigo.Text = "1";
            }
            else
            {
                int count = 0;
                count = producto.Count;
                count++;
                txtCodigo.Text = count.ToString();
            }
        }
        public void clear()
        {
            //limpia todo los text box
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtCantidad.Text = "";
            txtPrecio.Text = "";
        }
        public void Point_initial()
        {
            //ordenes de menustrip
            ordenGuardar.Enabled = false;
            ordenCancelar.Enabled = false;
            ordenNuevo.Enabled = true;

            //botones de toolstrip
            btGuardar.Enabled = false;
            btCancelar.Enabled = false;
            btNuevo.Enabled = true;
            btBuscar.Enabled = true;
            lbBuscar.Enabled = true;

            //ordenes del menu contextual
            cmGuardar.Enabled = false;
            cmCancelar.Enabled = false;
            cmProducto.Enabled = true;

            //habilitar los textbox
            txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            txtNombre.ReadOnly = true;
            txtCantidad.Enabled = false;
            txtCantidad.ReadOnly = true;
            txtPrecio.Enabled = false;
            txtPrecio.ReadOnly = true;

            //desactiva los botones de desplazamiento
            btnPrimero.Enabled = true;
            btnSiguiente.Enabled = true;
            btnUltimo.Enabled = true;
            btnAnterior.Enabled = true;
        }
        public bool validate()
        {
            //validaciones al guardar los datos
            if (txtNombre.Text == "" || txtPrecio.Text == "" || txtCantidad.Text == "")
            {
                MessageBox.Show("llene todos los campos");
                return false;
            }
            else if(!int.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("debe ingresar numeros");
                return false;
            }
            else if(!int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("debe ingresar numeros");
                return false;
            }
            return true;
        }

        
    }
}
