using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacoQuaseTudo
{
    internal class ClassTipo
    {
        //atributos
        private int idtipo;
        private string descricaoTipo;

        //propriedades 
        public int Idtipo// assim eu consigo validar , da forma curta na class servico;
        {
            get { return idtipo; }
            set { idtipo = value; }
        }
        public string DescricaoTipo
        {
            get { return descricaoTipo; }
            set { descricaoTipo = value; }
        }
        public DataTable Listar() //metodo
        {
            SqlDataAdapter daTipo;

            DataTable dtTipo = new DataTable();

            try
            {
                daTipo = new SqlDataAdapter("SELECT * FROM TIPOS", Main.conexao);
                daTipo.Fill(dtTipo);
                daTipo.FillSchema(dtTipo, SchemaType.Source);
            }
            catch (Exception)
            {
                throw; //levantar uma exceção
            }
            return dtTipo;
        }
    }
}
