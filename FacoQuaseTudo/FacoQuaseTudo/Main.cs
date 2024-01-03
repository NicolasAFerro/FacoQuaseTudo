using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FacoQuaseTudo
{
    public partial class Main : Form
    {
        //CRIEI UM TIPO DE DADO PARA FAZER A CONEXÃO
        public static SqlConnection conexao;
        private void Main_Load(object sender, EventArgs e)
        {

            try
            {
                // aqui a conexão vai depende da sua máquina da escola ou particular
                // Conexão com o Banco de dados
                conexao = new SqlConnection("Data Source=localhost\\sqlexpress02;Initial Catalog=FacoQuaseTudo;Integrated Security=True;Pooling=False");
                conexao.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro de banco de dados =/" + ex.Message, "Erro");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Outros Erros =/" + ex.Message, "Erro");
            }
        }
        public Main()
        {
            InitializeComponent();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Cliente>().Count() > 0)
            {
                Application.OpenForms["Cliente"].BringToFront();
            }
            else
            {
                Cliente objCliente = new Cliente();
                objCliente.MdiParent = this;
                objCliente.WindowState = FormWindowState.Maximized;
                objCliente.Show();
            }
        }

        private void serviçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Servico>().Count() > 0)
            {
                Application.OpenForms["Servico"].BringToFront();
            }
            else
            {
                Servico objServico = new Servico();
                objServico.MdiParent = this;
                objServico.WindowState = FormWindowState.Maximized;
                objServico.Show();
            }
        }

        private void buscarServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<BuscaServicos>().Count() > 0)
            {
                Application.OpenForms["BuscaServicos"].BringToFront();
            }
            else
            {
                BuscaServicos objBuscaServicos = new BuscaServicos();
                objBuscaServicos.MdiParent = this;
                objBuscaServicos.WindowState = FormWindowState.Maximized;
                objBuscaServicos.Show();
            }

        }
       

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
