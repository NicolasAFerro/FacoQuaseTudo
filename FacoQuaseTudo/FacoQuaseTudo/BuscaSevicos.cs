using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacoQuaseTudo
{
    public partial class BuscaServicos : Form
    {
        public BuscaServicos()
        {
            InitializeComponent();
            cbxCliente.SelectedIndexChanged += cbxCliente_SelectedIndexChanged;
        }
        private BindingSource bnCliente = new BindingSource();
        private BindingSource bnServicos = new BindingSource();
        //private bool bInclusao = false;
        private DataSet dsServico = new DataSet();
        private DataSet dsCliente = new DataSet();

        private void BuscaServicos_Load(object sender, EventArgs e)
        {
            // Carrega a lista de serviços no DataGridView
            ClassServico novoServico = new ClassServico();
            dsServico.Tables.Add(novoServico.Listar());
            bnServicos.DataSource = dsServico.Tables[0];// <------- o ERRO ESTAVA AQUI, TABLES [0]
                                              //bnCliente.DataSource = dsCliente.Tables["Clientes"];
            dgvServicos.DataSource = bnServicos;


            // Carrega a lista de clientes no ComboBox
            ClassCliente listaCliente = new ClassCliente();
            dsCliente.Tables.Add(listaCliente.ListarTeste());

            // Define a fonte de dados do ComboBox como a tabela de clientes
            cbxCliente.DataSource = dsCliente.Tables[0];
            // Configurar a exibição e os valores membros do ComboBox
            cbxCliente.DisplayMember = "NomeCompleto"; // Substitua "Nome" pelo nome correto da coluna que contém o nome do cliente
            cbxCliente.ValueMember = "Id";      // Substitua "Id" pelo nome correto da coluna que contém o ID do cliente

        }

        private void cbxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarDataGridView();
        }
        private void AtualizarDataGridView()
        {
            if (cbxCliente.SelectedItem != null)
            {
                // Obtém a linha de dados associada ao item selecionado
                DataRowView selectedRow = (DataRowView)cbxCliente.SelectedItem;

                // Obtém o valor do ID do cliente da linha de dados
                int idClienteSelecionado = Convert.ToInt32(selectedRow["Id"]);

                // Filtre os dados do DataTable com base no ID do cliente
                DataView dvServicos = new DataView(dsServico.Tables[0]);
                dvServicos.RowFilter = $"Clientes_id = {idClienteSelecionado}";

                // Atualize o BindingSource e, por sua vez, o DataGridView
                bnServicos.DataSource = dvServicos;
            }
        }
    }
}
