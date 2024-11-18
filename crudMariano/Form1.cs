using crudMariano.Database;

namespace crudMariano
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataUser db = new DataUser();

                var user = db.GetUser(nombre.Text)
                    ?? throw new Exception("Usuario no encontrado");

                if (true)
                {
                    Form2 nuevoFormulario = new Form2();
                    nuevoFormulario.Show();
                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrecta");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }
    }
}
