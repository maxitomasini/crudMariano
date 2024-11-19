using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using crudMariano.Database.Entities;
using crudMariano.Database;

namespace crudMariano
{
    public partial class Form2 : Form
    {
        private DataClient ClientDb;
        private DataProducts ProductDb;
        private DataSale SaleDb;
        public Form2()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ClientDb = new DataClient();
            ProductDb = new DataProducts();
            SaleDb = new DataSale();
            InitializeComponent();
        }

        private List<Client> clientes = new List<Client>();
        private List<Product> productos = new List<Product>();
        private List<Sale> ventas = new List<Sale>();
        private List<SaleDetails> ventaDetalle = new List<SaleDetails>();

        int indiceFila = -1;
        public string moduloActual = "";
        int VentaEnCurso = 0;
        double totalGralVenta = 0.00;

        //------------------------------------------------  Botones  ---------------------------------------------------------------
        #region botones
        //-----------------------------  Boton cerrar  -------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-----------------------------  Boton usuarios  -----------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            ocultar_grupos();
            moduloActual = "client";
            groupBox1.Size = new Size(458, 129);
            groupBox1.Location = new Point(41, 92);
            groupBox1.Visible = true;
            dataGridView1.Visible = true;
            dataGridView1.Location = new Point(41, 227);
            dataGridView1.Size = new Size(685, 271);
            CargarDatos();
        }

        //----------------------------  Boton actualizar usuarios  -------------------
        private void button5_Click(object sender, EventArgs e)
        {
            if ((indiceFila < 0 && textBox1.Text == "") || (indiceFila > 0 && textBox1.Text == "" && id.Text == ""))
            {
                msje("No Hay datos para actualizar");
                return;
            }
            int idReg = 0;
            if (indiceFila < 0) { idReg = clientes.Count() + 1; }

            if (indiceFila >= 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[indiceFila];
                idReg = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
            }

            Client nuevoUsuario = new Client
            {
                Id = idReg,
                Name = textBox1.Text,
                LastName = textBox2.Text,
                Dni = Convert.ToInt32(textBox5.Text)
            };

            if (indiceFila < 0)
            {
                clientes.Add(nuevoUsuario);
                ClientDb.InsertClient(nuevoUsuario);
            }
            else
            {
                clientes[indiceFila] = nuevoUsuario;
                ClientDb.UpdateClient(nuevoUsuario);
            }
            LimpiarDatos();
            ActualizarDataGrid();
            msje("Datos Actualizados");
            espera(1);
            dataGridView1.Enabled = true;
            CargarDatos();
        }

        //----------------------------  Boton Borrar usuario  ------------------------
        private void button6_Click(object sender, EventArgs e)
        {
            if (id.Text == "")
            {
                MessageBox.Show("Debe seleccionar el item para eliminar");
                return;
            }
            int idfila = Convert.ToInt32(id.Text);
            if (idfila > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el item " + idfila + "?", "Confirmacion", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    int num = idfila;
                    Client? elemento = clientes.FirstOrDefault(x => x.Id == num);
                    if (elemento != null)
                    {
                        clientes.Remove(elemento);
                        ClientDb.DeleteClient(num);
                        LimpiarDatos();
                        ActualizarDataGrid();
                        msje("Elemento eliminado");
                        espera(1);
                        dataGridView1.Enabled = true;
                    }
                }
            }
        }

        //----------------------------  Boton productos  ----------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            ocultar_grupos();
            moduloActual = "product";
            groupBox2.Size = new Size(458, 129);
            groupBox2.Location = new Point(41, 92);
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
            dataGridView1.Location = new Point(41, 227);
            dataGridView1.Size = new Size(685, 271);
            CargarDatos();
        }

        //----------------------------  Boton actualizar producto  ------------------
        private void button7_Click(object sender, EventArgs e)
        {
            if ((indiceFila < 0 && textBox3.Text == "") || (indiceFila > 0 && textBox3.Text == "" && id.Text == ""))
            {
                msje("No Hay datos para actualizar");
                return;
            }
            int idReg = 0;
            if (indiceFila < 0) { idReg = productos.Count() + 1; }

            if (indiceFila >= 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[indiceFila];
                idReg = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
            }

            Product nuevoProducto = new Product
            {
                Id = idReg,
                Name = textBox3.Text,
                Price = Convert.ToDouble(textBox4.Text),
                Stock = Convert.ToInt32(textBox6.Text),
            };

            if (indiceFila < 0)
            {
                productos.Add(nuevoProducto);
                ProductDb.InsertProduct(nuevoProducto);
            }
            else
            {
                productos[indiceFila] = nuevoProducto;
                ProductDb.UpdateProduc(nuevoProducto);
            }
            LimpiarDatos();
            ActualizarDataGrid();
            msje("Datos Actualizados");
            espera(1);
            dataGridView1.Enabled = true;
            CargarDatos();
        }

        //----------------------------  Boton borrar pruductos  ----------------------
        private void button8_Click(object sender, EventArgs e)
        {
            if (id.Text == "")
            {
                MessageBox.Show("Debe seleccionar el producto para eliminar");
                return;
            }
            int idfila = Convert.ToInt32(id.Text);
            if (idfila > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el item " + idfila + "?", "Confirmacion", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    int num = idfila;
                    Product elemento = productos.FirstOrDefault(x => x.Id == num);
                    if (elemento != null)
                    {
                        productos.Remove(elemento);
                        ProductDb.DeleteProduct(num);
                        LimpiarDatos();
                        ActualizarDataGrid();
                        msje("Elemento eliminado");
                        espera(1);
                        CargarDatos();
                        dataGridView1.Enabled = true;
                    }
                }
            }
        }

        //----------------------------  Boton venta  ---------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            ocultar_grupos();
            moduloActual = "sale";
            groupBox3.Visible = true;
            llenarComboCliente();
            CargarDatos();
            dataGridView1.Visible = true;
            dataGridView1.Location = new Point(12, 236);
            dataGridView1.Size = new Size(500, 246);
            if (VentaEnCurso > 0)
            {
                groupBox4.Visible = true;
                dataGridView2.Visible = true;
            }
            ventaDetalle.Clear();
        }

        //---------------------------  Boton nueva venta  ----------------------------
        private void button10_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(comboCliente.SelectedValue) <= 0)
            {
                msje("Seleccione cliente para genrar una venta");
                return;
            }
            int idreg = 0;
            button10.Enabled = false;
            totalVenta.ReadOnly = true;
            comboCliente.Enabled = false;
            if (indiceFila < 0) { idreg = ventas.Count() + 1; }
            Sale nuevaVenta = new Sale
            {
                Id = idreg,
                UserId = Convert.ToInt32(comboCliente.SelectedValue),
                Date = DateTime.Now,
                Total = 0
            };
            if (indiceFila < 0)
            {
                ventas.Add(nuevaVenta);
            }
            button10.Visible = false;
            button9.Visible = true;
            ActualizarDataGrid();
            groupBox4.Location = new Point(346, 93);
            espera(1);
            CargarDatos();
            groupBox4.Text = "Venta detalle N° [" + VentaEnCurso + "]";
            llenarComboProducto();
            groupBox4.Visible = true;
            dataGridView1.Enabled = false;
            dataGridView2.Visible = true;
            dataGridView2.Location = new Point(515, 236);
            dataGridView2.Size = new Size(464, 246);
            ventaDetalle.Clear();

        }

        //---------------------------  Boton cerrar venta  ---------------------------
        private void button9_Click(object sender, EventArgs e)
        {
            if (totalGralVenta <= 0)
            {
                msje("Total pedido en 0! Verifique");
                return;
            }
            ActualizarTotalVenta(VentaEnCurso, totalGralVenta);
             CargarDatos();
            VentaEnCurso = 0; totalGralVenta = 0;
            totalVenta.Text = "0.00";
            LimpiarDatos();
            dataGridView2.Visible = false;
            groupBox4.Visible = false;
        }

        //---------------------------  Boton agregar venta  -------------------------
        private void button12_Click(object sender, EventArgs e)
        {
            if (VentaEnCurso <= 0)
            {
                msje("Verifique pedido en curso!");
                return;
            }
            if (Convert.ToInt32(cant.Text) <= 0 || Convert.ToDouble(precio.Text) <= 0)
            {
                msje("Verifique cantidad y precio por favor");
                return;
            }

            /* using (MySqlConnection conexion = conexionDB.ObtenerConexion())
             {
                 string consulta = "INSERT INTO pedidos_items (Idpedido, Idproducto, Cantidad, Precio) VALUES ('" + PedidoEnCurso + "', '" + comboProducto.SelectedValue + "', '" + cant.Text + "', '" + precio.Text + "')";
                 ejecutarTransaccion(consulta);
             }
             CargarItems("SELECT it.*, p.nombre as producto FROM pedidos_items it INNER JOIN productos p On p.id = it.Idproducto WHERE Idpedido=" + PedidoEnCurso + " ORDER BY Id ASC");
             */
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = ventaDetalle;
            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Width = 60;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Refresh();
            totalGralVenta += Convert.ToDouble(cant.Text) * Convert.ToDouble(precio.Text);
            totalVenta.Text = totalGralVenta.ToString();
            cant.Text = "1"; precio.Text = "0.00";
        }

        //---------------------------  Numerador productos  --------------------------
        private void comboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Decimal selectedValue = 0;
            if (Decimal.TryParse(comboProducto.SelectedValue.ToString(), out selectedValue))
            {
                precio.Text = BuscarPrecioProducto(selectedValue);
            }
        }

        #endregion


        private void msje(string str)
        {
            label5.Text = str;
            label5.Visible = true;
            label5.Refresh();
            espera(3);
            label5.Text = "";
            label5.Visible = false;
        }

        public void espera(int nn)
        {
            Thread.Sleep(nn * 1000);
        }

        private void LimpiarDatos()
        {
            id.Text = "";
            indiceFila = -1;
            //Usuarios           
            textBox1.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            //Producto
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            //Ventas
            comboCliente.Enabled = true;
            button10.Visible = true;
            button10.Enabled = true;
            button9.Visible = false;
            dataGridView2.DataSource = null;
            ventas.Clear();
        }

        private void ActualizarDataGrid()
        {
            dataGridView1.DataSource = null;

            if (moduloActual == "client")
            {
                dataGridView1.DataSource = clientes;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 185;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Refresh();
            }
            if (moduloActual == "product")
            {
                dataGridView1.DataSource = productos;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Refresh();
            }
            if (moduloActual == "sale")
            {
                dataGridView1.DataSource = ventas;
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 60;
                dataGridView1.Columns[4].Width = 180;
                dataGridView1.Width = 550;
            }
        }

        private void ActualizarTotalVenta(int p, double t)
        {
            Sale nuevaVenta = new Sale
            {
                Id = p,
                Total = t,
            };
            SaleDb.UpdateSale(nuevaVenta);
        }

        private void CargarDatos()
        {
            try
            {
                id.Text = "";
                dataGridView1.Enabled = true;
                if (moduloActual == "client") { clientes.Clear(); }
                if (moduloActual == "product") { productos.Clear(); }
                if (moduloActual == "sale") { ventas.Clear(); }

                if (ClientDb != null)
                {

                    if (moduloActual == "client")
                    {
                        clientes = ClientDb.GetAllClients();
                    }
                    if (moduloActual == "product")
                    {
                        productos = ProductDb.GetAllProducts();
                    }
                    if (moduloActual == "sale")
                    {
                        ventas = SaleDb.GetAllSale();
                    }
                }
                else
                {
                    MessageBox.Show("La coneccion no esta configurada correctamente");
                }
                ActualizarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos " + ex.Message);
            }
        }

        private void ocultar_grupos()
        {
            id.Text = "";
            indiceFila = -1;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            dataGridView2.Visible = false;
        }

        private void llenarComboCliente()
        {
            if (ClientDb != null)
            {
                clientes.Clear();
                clientes.Add(new Client { Id = 0, Name = "Seleccione" });
                clientes = ClientDb.GetAllClients();
            }
            else
            {
                MessageBox.Show("La conexion no esta configurada correctamente");
            }
            comboCliente.DataSource = clientes;
            comboCliente.DisplayMember = "Name";
            comboCliente.ValueMember = "Id";
        }

        private void llenarComboProducto()
        {
            if (ProductDb != null)
            {
                productos.Clear();
                productos.Add(new Product { Id = 0, Name = "Seleccione" });
                productos = ProductDb.GetAllProducts();
            }
            else
            {
                MessageBox.Show("La conexion no esta configurada correctamente");
            }
            comboProducto.DataSource = productos;
            comboProducto.DisplayMember = "Name";
            comboProducto.ValueMember = "Id";
        }

        private string BuscarPrecioProducto(Decimal p)
        {
            double pre = 0;
            if (ProductDb != null)
            {
                ProductDb.GetAllProducts(Convert.ToString(p));
            }
            else
            {
                MessageBox.Show("La conexion no esta configurada correctamete.");
            }
            return pre.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                indiceFila = dataGridView1.CurrentRow.Index;
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                if (moduloActual == "client")
                {
                    id.Text = filaSeleccionada.Cells["Id"].Value.ToString();
                    textBox1.Text = filaSeleccionada.Cells["Name"].Value.ToString();
                    textBox2.Text = filaSeleccionada.Cells["LastName"].Value.ToString();
                    textBox5.Text = filaSeleccionada.Cells["Dni"].Value.ToString();
                }
                if (moduloActual == "product")
                {
                    id.Text = filaSeleccionada.Cells["Id"].Value.ToString();
                    textBox3.Text = filaSeleccionada.Cells["Name"].Value.ToString();
                    textBox4.Text = filaSeleccionada.Cells["Price"].Value.ToString();
                    textBox6.Text = filaSeleccionada.Cells["Stock"].Value.ToString();
                }
                if (moduloActual == "sale")
                {
                    dataGridView2.DataSource = null;
                    id.Text = filaSeleccionada.Cells["Id"].Value.ToString();
                    VentaEnCurso = Convert.ToInt32(id.Text);
                    id.Text = "";
                    comboCliente.SelectedValue = Convert.ToInt32(filaSeleccionada.Cells["userId"].Value);
                    totalVenta.Text = Convert.ToDouble(filaSeleccionada.Cells["total"].Value).ToString();
                    totalGralVenta = Convert.ToDouble(totalVenta.Text);
                    comboCliente.Enabled = false;
                    button10.Enabled = false;
                    button9.Visible = true;
                    //  CargarItems("SELECT it.*, p.nombre as producto FROM pedidos_items it INNER JOIN productos p On p.id = it.idProducto WHERE idpedido='" + PedidoEnCurso + "' ORDER BY id ASC");
                    dataGridView2.Visible = true;
                    dataGridView2.Location = new Point(515, 236);
                    dataGridView2.Size = new Size(464, 246);
                    dataGridView2.DataSource = ventaDetalle;
                    dataGridView2.Columns[0].Width = 60;
                    dataGridView2.Columns[1].Width = 60;
                    dataGridView2.Columns[2].Width = 80;
                    dataGridView2.Columns[3].Width = 60;
                    dataGridView2.Columns[4].Width = 60;
                    groupBox4.Visible = true;
                    groupBox4.Location = new Point(346, 93);
                    groupBox4.Text = "Items Pedido N° [" + VentaEnCurso + "]";
                    llenarComboProducto();
                    dataGridView1.Enabled = false;
                    dataGridView2.Visible = true;
                    dataGridView2.Location = new Point(515, 236);
                    dataGridView2.Size = new Size(464, 246);
                    dataGridView2.Refresh();
                    
                }
            }
            dataGridView1.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

       
    }


}

