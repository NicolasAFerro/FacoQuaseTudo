using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacoQuaseTudo
{
    public partial class Cliente : Form
    {
        //São atributos privados da classe, onde:
        //bnCliente: É um objeto do tipo BindingSource, usado para realizar o binding de dados entre controles e fontes de dados.
        //bInclusao: É uma flag booleana que indica se a aplicação está em modo de inclusão.
        //dsCliente: São objetos do tipo DataSet que representam conjuntos de dados.
        private BindingSource bnCliente = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsCliente = new DataSet();
       

        public Cliente()
        {   
            InitializeComponent();
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            // VAI CARREGAR OS DADOS NO DATAGRID
            try
            {
                ClassCliente novoCliente = new ClassCliente();
                dsCliente.Tables.Add(novoCliente.Listar());
                bnCliente.DataSource = dsCliente.Tables["Clientes"];
                dgvClientes.DataSource = bnCliente;
                bnvCliente.BindingSource = bnCliente;



                txtID.DataBindings.Add("TEXT", bnCliente, "Id");
                txtNome.DataBindings.Add("TEXT", bnCliente, "Nome");
                txtQuadra.DataBindings.Add("TEXT", bnCliente, "Quadra");
                txtLote.DataBindings.Add("TEXT", bnCliente, "Lote");
                txtTelefone.DataBindings.Add("TEXT", bnCliente, "Telefone"); 
                rtxtObservacao.DataBindings.Add("TEXT", bnCliente, "Observacao");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void Cliente_Click(object sender, EventArgs e)
        {
          

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (tabCliente.SelectedIndex == 0)
            {
                tabCliente.SelectTab(1);
            }

            // PARA OS BOTÕES APAGAREM CONFORME FAZ AS CONDIÇÕES

            //bnCliente.AddNew();



            txtNome.Enabled = true;
            txtQuadra.Enabled = true;
            txtLote.Enabled = true;
            txtTelefone.Enabled = true;
            rtxtObservacao.Enabled = true;



            btnAdicionar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;

           // bnCliente.AddNew();

            bInclusao = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ClassCliente RegCliente = new ClassCliente();



            RegCliente.Nome = txtNome.Text;
            RegCliente.Quadra = txtQuadra.Text; 
            RegCliente.Lote = txtLote.Text; 
            RegCliente.Telefone = txtTelefone.Text;
            RegCliente.Observacao = rtxtObservacao.Text;

            if (bInclusao)
            {
                if (RegCliente.Incluir() > 0)
                {
                    MessageBox.Show("Cliente adicionado com sucesso!");



                    txtNome.Enabled = false;
                    txtQuadra.Enabled = false;
                    txtLote.Enabled = false;
                    txtTelefone.Enabled = false; 
                    rtxtObservacao.Enabled=false;



                    btnAdicionar.Enabled = true;
                    btnAlterar.Enabled = true;
                    btnExcluir.Enabled = true;
                    btnSalvar.Enabled = false;
                    btnCancelar.Enabled = false;



                    bInclusao = false;



                    // recarrega o grid
                    dsCliente.Tables.Clear();
                    dsCliente.Tables.Add(RegCliente.Listar());
                    bnCliente.DataSource = dsCliente.Tables["Clientes"];
                }
                else
                {
                    MessageBox.Show("Erro ao gravar Cliente!");
                }
            }
            else
            {
                RegCliente.IdCliente = Convert.ToInt32(txtID.Text);



                if (RegCliente.Alterar() > 0)
                {
                    MessageBox.Show("Cliente alterado com sucesso!", "SUCESSO");




                    txtNome.Enabled = false;
                    txtQuadra.Enabled = false;
                    txtLote.Enabled = false;
                    txtTelefone.Enabled = false;
                    rtxtObservacao.Enabled = false;



                    btnAdicionar.Enabled = true;
                    btnAlterar.Enabled = true;
                    btnExcluir.Enabled = true;
                    btnSalvar.Enabled = false;
                    btnCancelar.Enabled = false;



                    // recarrega o grid
                    dsCliente.Tables.Clear();
                    dsCliente.Tables.Add(RegCliente.Listar());
                    bnCliente.DataSource = dsCliente.Tables["Clientes"];
                }
                else
                {



                    MessageBox.Show("Erro ao alterar Cliente!");
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (tabCliente.SelectedIndex == 0)
            {
                tabCliente.SelectTab(1);
            }



            txtNome.Enabled = true;
            txtQuadra.Enabled = true;
            txtLote.Enabled = true;
            txtTelefone.Enabled = true; 
            rtxtObservacao.Enabled=true;



            btnAdicionar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;



            bInclusao = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            lblNome.ForeColor = Color.Red;
            if (tabCliente.SelectedIndex == 0)
            {
                tabCliente.SelectTab(1);
            }
            if (MessageBox.Show("Confirma exclusão do cliente: "+ txtNome.Text + "\n" + "QUADRA: "+txtQuadra.Text+ " LOTE: "+ txtLote.Text, "Yes or No",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ClassCliente RegCliente = new ClassCliente();
                RegCliente.IdCliente = Convert.ToInt32(txtID.Text);



                if (RegCliente.Excluir() > 0)
                {
                    MessageBox.Show("Cliente excluido com sucesso!", "SUCESSO");
                    lblNome.ForeColor = Color.Black;



                    // recarrega o grid
                    dsCliente.Tables.Clear();
                    dsCliente.Tables.Add(RegCliente.Listar());
                    bnCliente.DataSource = dsCliente.Tables["Clientes"];
                }
                else
                {
                    lblNome.ForeColor = Color.Black;
                    MessageBox.Show("Erro ao excluir contato!", "ERRO");
                }
            }
            else
                lblNome.ForeColor = Color.Black;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bnCliente.CancelEdit();



            txtNome.Enabled = false;
            txtQuadra.Enabled = false;
            txtLote.Enabled = false;
            txtTelefone.Enabled = false;
            rtxtObservacao.Enabled = false;


            btnAdicionar.Enabled = true;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;



            bInclusao = false;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    
}
