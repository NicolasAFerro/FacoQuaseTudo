using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.SqlTypes;
using System.Windows.Forms;


namespace FacoQuaseTudo
{
    internal class ClassServico
    {
        //Declaração das propriedades do objeto; 
        //prop dois tabs; 
        public int Id { get; set; }
        public decimal Valor { get; set; } 
        public DateTime DataServico { get; set;} 
        public string Observacao { get; set; }
        public int Tipos_Id { get; set; }
        public int Clientes_id { get; set; }

        public decimal ValorServico// assim eu consigo validar , da forma curta na class servico;
        {
            get { return Valor; }
            set { Valor = value; }
        }
        public DataTable Listar()
        {
            SqlDataAdapter daServico;

            DataTable dtDespesa = new DataTable();

            try
            {
                daServico = new SqlDataAdapter("SELECT * FROM SERVICOS ORDER BY Servicos.Data DESC", Main.conexao);
                //daServico = new SqlDataAdapter("SELECT Servicos.Id, Servicos.Data, Servicos.Valor, Servicos.Observacao FROM SERVICOS  ", Main.conexao);

                //daServico = new SqlDataAdapter("SELECT * FROM Servicos INNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id", Main.conexao);
                //string query = "SELECT Servicos.Id, Servicos.Data, Servicos.Valor, Servicos.Observacao, Clientes.Nome AS NomeCliente, Clientes.Quadra, Clientes.Lote, Tipos.Descricao AS DescricaoTipo\r\nFROM Servicos\r\nINNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id\r\nINNER JOIN Tipos ON Servicos.Tipos_Id = Tipos.Id ORDER BY\r\n  Servicos.Data DESC;";
                //string query = "SELECT SERVICOS.*, Clientes.Nome AS NomeCliente, Clientes.Quadra, Clientes.Lote, Tipos.Descricao AS DescricaoTipo\r\nFROM Servicos\r\nINNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id\r\nINNER JOIN Tipos ON Servicos.Tipos_Id = Tipos.Id ORDER BY\r\n  Servicos.Data DESC;";
                //string query = "SELECT SERVICOS.*, \r\n       Clientes.Nome, Clientes.Quadra, Clientes.Lote, \r\n       Tipos.Descricao\r\nFROM Servicos\r\nINNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id\r\nINNER JOIN Tipos ON Servicos.Tipos_Id = Tipos.Id\r\nORDER BY Servicos.Data DESC;";
                //daServico = new SqlDataAdapter(query, Main.conexao);
                daServico.Fill(dtDespesa);
                daServico.FillSchema(dtDespesa, SchemaType.Source); 
                
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
                // SqlCommand mycommand = new SqlCommand("INSERT INTO SERVICOS VALUES (@valorservico,@dataservico,@obsservico,@tipo_idtipo,@cliente_idcliente)", Main.conexao);
                //SqlCommand mycommand = new SqlCommand("INSERT INTO SERVICOS VALUES (@dataservico, @valorservico, @obsservico, @tipo_idtipo, @cliente_idcliente)", Main.conexao);
                // Adiciona parâmetros ao comando 
                SqlCommand mycommand = new SqlCommand("INSERT INTO SERVICOS (Valor,Data, Observacao, Tipos_Id, Clientes_id) VALUES ( @valorservico,@dataservico,  @obsservico, @tipo_idtipo, @cliente_idcliente)", Main.conexao);
                mycommand.Parameters.Add(new SqlParameter("@valorservico", SqlDbType.Decimal));
                mycommand.Parameters.Add(new SqlParameter("@dataservico", SqlDbType.DateTime));
                mycommand.Parameters.Add(new SqlParameter("@obsservico", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@tipo_idtipo", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@cliente_idcliente", SqlDbType.Int));

                // Define os valores dos parâmetros
                mycommand.Parameters["@valorservico"].Value = Valor;
                mycommand.Parameters["@dataservico"].Value = DataServico;
                mycommand.Parameters["@obsservico"].Value = Observacao;
                mycommand.Parameters["@tipo_idtipo"].Value = Tipos_Id;
                mycommand.Parameters["@cliente_idcliente"].Value = Clientes_id;

                // Executa o comando e obtém o número de linhas afetadas
                retorno = mycommand.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return retorno;
        }

        public int Alterar()
        {
            int retorno = 0;
            try
            {
                // Cria um comando SQL para atualização
                SqlCommand mycommand = new SqlCommand("UPDATE SERVICOS SET Valor=@valorservico,data=@dataservico," +
                    "Observacao=@obsservico,Tipos_id=@tipo_idtipo, Clientes_id=@cliente_idcliente WHERE Id = @IdServico", Main.conexao);
                // Adiciona parâmetros ao comando
                mycommand.Parameters.Add(new SqlParameter("@valorservico", SqlDbType.Decimal));
                mycommand.Parameters.Add(new SqlParameter("@dataservico", SqlDbType.DateTime));
                mycommand.Parameters.Add(new SqlParameter("@obsservico", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@tipo_idtipo", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@cliente_idcliente", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@IdServico", SqlDbType.Int));

                // Define os valores dos parâmetros
                mycommand.Parameters["@valorservico"].Value = Valor;
                mycommand.Parameters["@dataservico"].Value = DataServico;
                mycommand.Parameters["@obsservico"].Value = Observacao;
                mycommand.Parameters["@tipo_idtipo"].Value = Tipos_Id;
                mycommand.Parameters["@cliente_idcliente"].Value = Clientes_id;
                mycommand.Parameters["@IdServico"].Value = Id; ; 


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
                SqlCommand mycommand = new SqlCommand("DELETE FROM SERVICOS WHERE Id=@IdServico", Main.conexao);

                // Adiciona parâmetros ao comando
                mycommand.Parameters.Add(new SqlParameter("@IdServico", SqlDbType.Int));
                mycommand.Parameters["@IdServico"].Value = Id;

                // Executa o comando e obtém o número de linhas afetadas
                nReg = mycommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return nReg;
        }

    }
}
