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
    public class FarmaciaDAO
    {
        private readonly Context _context;
        private List<Farmacia> _listaFarmacia;

        public FarmaciaDAO(Context context)
        {
            _context = context;
            _listaFarmacia = new List<Farmacia>();
        }

        public List<Farmacia> Index()
        {
            try
            {
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" SELECT f.ID, f.Nome, f.Email, f.Endereco FROM Farmacia f");

                    cmd.CommandText = SQL.ToString();

                    _context.Database.OpenConnection();


                    using (var result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                Farmacia PerguntaData = new Farmacia();
                                PerguntaData.ID = result.GetInt32(0);
                                PerguntaData.Nome = result.GetString(1);
                                PerguntaData.Email = result.GetString(2);
                                PerguntaData.Endereco = result.GetString(3);
                                _listaFarmacia.Add(PerguntaData);
                            }
                            result.Close();
                            _context.Database.CloseConnection();
                            return _listaFarmacia;
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

        public int Insert(Farmacia item)
        {
            int ChamadoID = 0;
            StringBuilder SQL = new StringBuilder();
            List<SqlParameter> Param = new List<SqlParameter>();
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                SQL.AppendLine(" INSERT INTO Farmacia (Nome, Email, Endereco) ");
                SQL.AppendLine(" VALUES ( ");
                SQL.AppendLine(" 	@Nome, ");
                SQL.AppendLine(" 	@Email, ");
                SQL.AppendLine(" 	@Endereco ");
                SQL.AppendLine(" 	) ");

                cmd.CommandText = SQL.ToString();

                cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));
                cmd.Parameters.Add(new SqlParameter("@Email", item.Email));
                cmd.Parameters.Add(new SqlParameter("@Endereco", item.Endereco));

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

        public int Update(Farmacia item)
        {
            try
            {
                int updatedRows = 0;
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" UPDATE Farmacia SET");
                    SQL.AppendLine(" 	Nome = @Nome, ");
                    SQL.AppendLine(" 	Email = @Email, ");
                    SQL.AppendLine(" 	Endereco = @Endereco ");
                    SQL.AppendLine("    WHERE ID = @ID; ");

                    cmd.CommandText = SQL.ToString();

                    cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));
                    cmd.Parameters.Add(new SqlParameter("@Email", item.Email));
                    cmd.Parameters.Add(new SqlParameter("@Endereco", item.Endereco));
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
                    SQL.AppendLine(" Delete Farmacia");
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
