using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FacoQuaseTudo
{
    internal class ClassCliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Quadra { get; set; }
        public string Lote { get; set; }
        public string Telefone { get; set; }
        public string Observacao { get; set; }

        public int idcliente// assim eu consigo validar , da forma curta na class servico;
        {
            get { return IdCliente; }
            set { IdCliente = value; }
        }
        public string NomeCliente
        {
            get { return Nome; }
            set { Nome = value; }
        }
        public string QuadraCliente// assim eu consigo validar , da forma curta na class servico;
        {
            get { return Quadra; }
            set { Quadra = value; }
        }
        public string LoteCliente
        {
            get { return Lote; }
            set { Lote = value; }
        }

        public DataTable Listar()
        {
            SqlDataAdapter daCliente;

            DataTable dtCliente = new DataTable();

            try
            {
                //daDespesa = new SqlDataAdapter("SELECT * FROM Clientes ORDER BY Nome", Main.conexao);
                daCliente = new SqlDataAdapter("SELECT * FROM Clientes ", Main.conexao);
                daCliente.Fill(dtCliente);
                daCliente.FillSchema(dtCliente, SchemaType.Source);
            }
            catch (Exception)
            {
                throw;
            }
            return dtCliente;
        }
        public DataTable ListarTeste()
        {
            SqlDataAdapter daDespesa;

            DataTable dtDespesa = new DataTable();

            try
            {
                string sqlQuery = " SELECT Id,Nome, Quadra, Lote, (CAST(Id AS NVARCHAR)+' '+Nome+' ' +Quadra + ' ' + Lote) AS NomeCompleto FROM Clientes;";
                daDespesa = new SqlDataAdapter(sqlQuery, Main.conexao);
                daDespesa.Fill(dtDespesa);
                daDespesa.FillSchema(dtDespesa, SchemaType.Source);
            }
            catch (Exception)
            {
                throw;
            }
            return dtDespesa;
        }

        public int Incluir()
        {
            int retorno = 0;
            try
            {
                // Cria um comando SQL para inserção
                SqlCommand mycommand = new SqlCommand("INSERT INTO CLIENTES VALUES (@NomeCliente,@QuadraCliente,@LoteCliente,@TelefoneCliente,@ObservacaoCliente)", Main.conexao);

                // Adiciona parâmetros ao comando
                mycommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@QuadraCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@LoteCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@TelefoneCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@ObservacaoCliente", SqlDbType.VarChar));

                // Define os valores dos parâmetros
                mycommand.Parameters["@NomeCliente"].Value = Nome;
                mycommand.Parameters["@QuadraCliente"].Value = Quadra;
                mycommand.Parameters["@LoteCliente"].Value = Lote;
                mycommand.Parameters["@TelefoneCliente"].Value = Telefone;
                mycommand.Parameters["@ObservacaoCliente"].Value = Observacao;

                // Executa o comando e obtém o número de linhas afetadas
                retorno = mycommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        public int Alterar()
        {
            int retorno = 0;
            try
            {
                // Cria um comando SQL para atualização
                SqlCommand mycommand = new SqlCommand("UPDATE CLIENTES SET Nome=@NomeCliente,Quadra=@QuadraCliente," +
                    "Lote=@LoteCliente,Telefone=@TelefoneCliente, Observacao=@ObservacaoCliente WHERE Id = @IdCliente", Main.conexao);
                // Adiciona parâmetros ao comando 
                mycommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@QuadraCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@LoteCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@TelefoneCliente", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@ObservacaoCliente", SqlDbType.VarChar));

                // Define os valores dos parâmetros 
                mycommand.Parameters["@IdCliente"].Value = IdCliente;
                mycommand.Parameters["@NomeCliente"].Value = Nome;
                mycommand.Parameters["@QuadraCliente"].Value = Quadra;
                mycommand.Parameters["@LoteCliente"].Value = Lote;
                mycommand.Parameters["@TelefoneCliente"].Value = Telefone;
                mycommand.Parameters["@ObservacaoCliente"].Value = Observacao;

                // Executa o comando e obtém o número de linhas afetadas
                retorno = mycommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        public int Excluir()
        {
            int nReg = 0;
            try
            {
                // Cria um comando SQL para exclusão
                SqlCommand mycommand = new SqlCommand("DELETE FROM Clientes WHERE Id=@IdCliente", Main.conexao);

                // Adiciona parâmetros ao comando
                mycommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int));
                mycommand.Parameters["@IdCliente"].Value = IdCliente;

                // Executa o comando e obtém o número de linhas afetadas
                nReg = mycommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return nReg;
        }
        public DataTable ListaCurta() //metodo
        {
            SqlDataAdapter daCliente;

            DataTable dtCliente = new DataTable();

            try
            {
                string sqlQuery = "SELECT Id FROM Clientes";
                //string sqlQuery = "SELECT Id, (Nome, Quadra, Lote FROM Clientes"; 
                //string sqlQuery = "SELECT Id, (Nome + ' ' + Quadra + ' ' + Lote) AS NomeCompleto FROM Clientes";
                daCliente = new SqlDataAdapter(sqlQuery, Main.conexao);
                daCliente.Fill(dtCliente);
                daCliente.FillSchema(dtCliente, SchemaType.Source);
            }
            catch (Exception)
            {
                throw; //levantar uma exceção
            }
            return dtCliente;
        }

    }
}
