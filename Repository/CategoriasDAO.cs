using API_Farmacia.Infra;
using API_Farmacia.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Farmacia.Repository
{
    public class CategoriasDAO
    {
        private readonly Context _context;
        private List<CategoriaRemedio> _listaCategoria;

        public CategoriasDAO(Context context)
        {
            _context = context;
            _listaCategoria = new List<CategoriaRemedio>();
        }

        public List<CategoriaRemedio> Index()
        {
            try
            {
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" SELECT * FROM CategoriaRemedio ");

                    cmd.CommandText = SQL.ToString();

                    _context.Database.OpenConnection();


                    using (var result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                CategoriaRemedio PerguntaData = new CategoriaRemedio();
                                PerguntaData.ID = result.GetInt32(0);
                                PerguntaData.Nome = result.GetString(1);
                                _listaCategoria.Add(PerguntaData);
                            }
                            result.Close();
                            _context.Database.CloseConnection();
                            return _listaCategoria;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public int Insert(CategoriaRemedio item)
        {
            int ChamadoID = 0;
            StringBuilder SQL = new StringBuilder();
            List<SqlParameter> Param = new List<SqlParameter>();
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                SQL.AppendLine(" INSERT INTO CategoriaRemedio (Nome) ");
                SQL.AppendLine(" VALUES ( ");
                SQL.AppendLine(" 	@Nome ");
                SQL.AppendLine(" 	) ");

                cmd.CommandText = SQL.ToString();

                cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));

                _context.Database.OpenConnection();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChamadoID = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
                _context.Database.CloseConnection();
                return ChamadoID;
            }
        }

        public int Update(CategoriaRemedio item)
        {
            try
            {
                int updatedRows = 0;
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" UPDATE CategoriaRemedio SET");
                    SQL.AppendLine(" 	Nome = @Nome ");
                    SQL.AppendLine("    WHERE ID = @ID; ");

                    cmd.CommandText = SQL.ToString();

                    cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));
                    cmd.Parameters.Add(new SqlParameter("@ID", item.ID));

                    _context.Database.OpenConnection();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            updatedRows = Convert.ToInt32(reader[0]);
                        }
                        reader.Close();
                    }
                    _context.Database.CloseConnection();
                    return updatedRows;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int Delete(int ID)
        {
            try
            {
                int updatedRows = 0;
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" Delete CategoriaRemedio");
                    SQL.AppendLine("    WHERE ID = @ID; ");

                    cmd.CommandText = SQL.ToString();
                    cmd.Parameters.Add(new SqlParameter("@ID", ID));

                    _context.Database.OpenConnection();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            updatedRows = Convert.ToInt32(reader[0]);
                        }
                        reader.Close();
                    }
                    _context.Database.CloseConnection();
                    return updatedRows;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
