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
    public partial class Servico : Form
    {
        
        private BindingSource bnCliente = new BindingSource();
        private BindingSource bnServico = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsServico = new DataSet();
        private DataSet dsTipo = new DataSet();
        private DataSet dsCliente = new DataSet();
        public Servico()
        {
            InitializeComponent();
        }

        private void Servico_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    try
                    {
                        ClassServico novoServico = new ClassServico();
                        dsServico.Tables.Add(novoServico.Listar());
                        bnServico.DataSource = dsServico.Tables[0];// <------- o ERRO ESTAVA AQUI, TABLES [0]
                                                                   //bnCliente.DataSource = dsCliente.Tables["Clientes"];
                        dgvServicos.DataSource = bnServico;
                        bnvServico.BindingSource = bnServico;

                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    try
                    {
                        txtID.DataBindings.Add("TEXT", bnServico, "Id");
                        txtValor.DataBindings.Add("Text", bnServico, "Valor");
                        dtpData.DataBindings.Add("TEXT", bnServico, "Data");
                        // carrega dados do tipo
                        ClassTipo ObjT = new ClassTipo();
                        dsTipo.Tables.Add(ObjT.Listar());
                        cmbTipo.DataSource = dsTipo.Tables[0];
                        //CAMPO QUE SERÁ MOSTRADO PARA O USUÁRIO
                        cmbTipo.DisplayMember = "Descricao";
                        //CAMPO QUE É A CHAVE DA TABELA TIPO E QUE LIGA COM A TABELA DE DESPESA
                        cmbTipo.ValueMember = "Id";///<--- O ERRO ESTAVA AQUI, COMO MUDEI O NOME DA COLUNA NO LISTA MUDOU A FK
                        //linkar combox do tipo
                        cmbTipo.DataBindings.Add("SelectedValue",
                             bnServico, "Tipos_Id"); // AJUSTAR DROPDOWNSTYLE PARA DropDownList PARA NAO DEIXAR DIGITAR AQUI O NOME DA COLUNA DO DB
                                                    // 
                        txtObservacao.DataBindings.Add("TEXT", bnServico, "Observacao");
                     
                        

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                 
                }catch(Exception ex)
                {
                    throw ex;
                }
                    

               


             

                //COMBO BOX CLIENTE
                ClassCliente listaCliente = new ClassCliente();
                dsCliente.Tables.Add(listaCliente.ListarTeste());
                //Define a fonte de dados do ComboBox como a tabela de clientes
                cbxClientes.DataSource = dsCliente.Tables[0];
                // Configurar a exibição e os valores membros do ComboBox
                cbxClientes.DisplayMember = "NomeCompleto"; // Substitua "Nome" pelo nome correto da coluna que contém o nome do cliente
                cbxClientes.ValueMember = "Id";      // Substitua "Id" pelo nome correto da coluna que contém o ID do cliente
                cbxClientes.DataBindings.Add("SelectedValue", bnServico, "Clientes_id");

               



            }
            catch (Exception ex)
            {
                
                MessageBox.Show( ex.ToString());
            }

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (tabServico.SelectedIndex == 0)
            {
                tabServico.SelectTab(1);
            }



            //bnServico.AddNew();



            txtValor.Enabled = true;
            dtpData.Enabled = true;
            txtObservacao.Enabled = true;
            cmbTipo.Enabled = true;
            //cmbCliente.Enabled = true;



            cmbTipo.SelectedIndex = 1;
            cbxClientes.SelectedIndex = 0;

            btnAdicionar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;



            bInclusao = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            decimal valor;

            // validar os dados
            if (!Decimal.TryParse(txtValor.Text, out valor))
            {
                MessageBox.Show("Valor inválido!");
            }
            else
            {
                ClassServico RegServ = new ClassServico();



                RegServ.Valor = valor;
                RegServ.DataServico =dtpData.Value;
                RegServ.Observacao = txtObservacao.Text;
              
                RegServ.Tipos_Id = Convert.ToInt32(cmbTipo.SelectedValue.ToString());
                RegServ.Clientes_id = Convert.ToInt32(cbxClientes.SelectedValue.ToString());

                if (bInclusao)
                {
                    if (RegServ.Incluir() > 0)
                    {
                        MessageBox.Show("Servico adicionado com sucesso!");



                        txtValor.Enabled = false;
                        dtpData.Enabled = false;
                        txtObservacao.Enabled = false;
                        cmbTipo.Enabled = false;
                        //cbxClientes.Enabled = false;



                        btnAdicionar.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnSalvar.Enabled = false;
                        btnCancelar.Enabled = false;



                        bInclusao = false;



                        // recarrega o grid
                        dsServico.Tables.Clear();
                        dsServico.Tables.Add(RegServ.Listar());
                        bnServico.DataSource = dsServico.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Servico!");
                    }
                }
                else
                {
                    RegServ.Id = Convert.ToInt32(txtID.Text);



                    if (RegServ.Alterar() > 0)
                    {
                        MessageBox.Show("Servico alterado com sucesso!", "SUCESSO");



                        txtValor.Enabled = false;
                        dtpData.Enabled = false;
                        txtObservacao.Enabled = false;
                        cmbTipo.Enabled = false;
                       // cbxClientes.Enabled = false;



                        btnAdicionar.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnSalvar.Enabled = false;
                        btnCancelar.Enabled = false;



                        // recarrega o grid
                        dsServico.Tables.Clear();
                        dsServico.Tables.Add(RegServ.Listar());
                        bnServico.DataSource = dsServico.Tables[0];
                    }
                    else
                    {



                        MessageBox.Show("Erro ao alterar Servico!");
                    }
                }
            }

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (tabServico.SelectedIndex == 0)
            {
                tabServico.SelectTab(1);
            }



            txtValor.Enabled = true;
            dtpData.Enabled = true;
            txtObservacao.Enabled = true;
            cmbTipo.Enabled = true;
            //cbxClientes.Enabled = true;



            btnAdicionar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;



            bInclusao = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            lblCliente.ForeColor = Color.Red;
            if (tabServico.SelectedIndex == 0)
            {
                tabServico.SelectTab(1);
            }
            if (MessageBox.Show("Confirma exclusão do serviço do cliente:"+cbxClientes.Text, "Yes or No",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ClassServico RegDesp = new ClassServico();
                RegDesp.Id = Convert.ToInt32(txtID.Text);



                if (RegDesp.Excluir() > 0)
                {
                    MessageBox.Show("Serviço excluída com sucesso!", "SUCESSO");
                    lblCliente.ForeColor = Color.Black;


                    // recarrega o grid
                    dsServico.Tables.Clear();
                    dsServico.Tables.Add(RegDesp.Listar());
                    bnServico.DataSource = dsServico.Tables[0];
                }
                else
                {
                    lblCliente.ForeColor = Color.Black;
                    MessageBox.Show("Erro ao excluir contato!", "ERRO");
                }
            }else
                lblCliente.ForeColor = Color.Black;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bnServico.CancelEdit();



            txtValor.Enabled = false;
            dtpData.Enabled = false;
            txtObservacao.Enabled = false;
            cmbTipo.Enabled = false; 
            //cbxClientes.Enabled = false;



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
