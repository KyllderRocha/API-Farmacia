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
    public class RemedioDAO
    {
        private readonly Context _context;
        private List<Remedio> _listaRemedio;

        public RemedioDAO(Context context)
        {
            _context = context;
            _listaRemedio = new List<Remedio>();
        }

        public List<Remedio> Index()
        {
            try
            {
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" SELECT ");
                    SQL.AppendLine(" r.ID, ");
                    SQL.AppendLine(" r.Nome, ");
                    SQL.AppendLine(" r.Validade, ");
                    SQL.AppendLine(" r.FarmaciaID, ");
                    SQL.AppendLine(" r.CategoriaID ,");
                    SQL.AppendLine(" f.Nome FarmaciaNome, ");
                    SQL.AppendLine(" cr.Nome CategoriaNome ");
                    SQL.AppendLine(" From Remedio r ");
                    SQL.AppendLine(" Inner join Farmacia f on f.ID = r.FarmaciaID ");
                    SQL.AppendLine(" Inner join CategoriaRemedio cr on cr.ID = r.CategoriaID ");

                    cmd.CommandText = SQL.ToString();

                    _context.Database.OpenConnection();


                    using (var result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                Remedio PerguntaData = new Remedio();
                                PerguntaData.ID = result.GetInt32(0);
                                PerguntaData.Nome = result.GetString(1);
                                PerguntaData.Validade = result.GetDateTime(2);
                                PerguntaData.FarmaciaID = result.GetInt32(3);
                                PerguntaData.CategoriaID = result.GetInt32(4);
                                PerguntaData.FarmaciaNome = result.GetString(5);
                                PerguntaData.CategoriaNome = result.GetString(6);
                                _listaRemedio.Add(PerguntaData);
                            }
                            result.Close();
                            _context.Database.CloseConnection();
                            return _listaRemedio;
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

        public int Insert(Remedio item)
        {
            int ChamadoID = 0;
            StringBuilder SQL = new StringBuilder();
            List<SqlParameter> Param = new List<SqlParameter>();
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                SQL.AppendLine(" INSERT INTO Remedio (Nome, Validade, FarmaciaID, CategoriaID) ");
                SQL.AppendLine(" VALUES ( ");
                SQL.AppendLine(" 	@Nome, ");
                SQL.AppendLine(" 	@Validade, ");
                SQL.AppendLine(" 	@FarmaciaID, ");
                SQL.AppendLine(" 	@CategoriaID ");
                SQL.AppendLine(" 	) ");

                cmd.CommandText = SQL.ToString();

                cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));
                cmd.Parameters.Add(new SqlParameter("@Validade", item.Validade));
                cmd.Parameters.Add(new SqlParameter("@FarmaciaID", item.FarmaciaID));
                cmd.Parameters.Add(new SqlParameter("@CategoriaID", item.CategoriaID));

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

        public int Update(Remedio item)
        {
            try
            {
                int updatedRows = 0;
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" UPDATE Remedio SET");
                    SQL.AppendLine(" 	Nome = @Nome, ");
                    SQL.AppendLine(" 	Validade = @Validade, ");
                    SQL.AppendLine(" 	FarmaciaID = @FarmaciaID, ");
                    SQL.AppendLine(" 	CategoriaID = @CategoriaID ");
                    SQL.AppendLine("    WHERE ID = @ID; ");

                    cmd.CommandText = SQL.ToString();

                    cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));
                    cmd.Parameters.Add(new SqlParameter("@Validade", item.Validade));
                    cmd.Parameters.Add(new SqlParameter("@FarmaciaID", item.FarmaciaID));
                    cmd.Parameters.Add(new SqlParameter("@CategoriaID", item.CategoriaID));
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
                    SQL.AppendLine(" Delete Remedio");
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
