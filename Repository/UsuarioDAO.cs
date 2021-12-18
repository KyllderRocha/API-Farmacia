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
    public class UsuarioDAO
    {
        private readonly Context _context;
        private List<Usuario> _listaUsuario;

        public UsuarioDAO(Context context)
        {
            _context = context;
            _listaUsuario = new List<Usuario>();
        }

        public List<Usuario> Index()
        {
            try
            {
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" SELECT ");
                    SQL.AppendLine(" u.ID, ");
                    SQL.AppendLine(" u.Nome, ");
                    SQL.AppendLine(" u.Email, ");
                    SQL.AppendLine(" u.Senha ,");
                    SQL.AppendLine(" u.FarmaciaID, ");
                    SQL.AppendLine(" f.Nome FarmaciaNome ");
                    SQL.AppendLine(" From Usuario u");
                    SQL.AppendLine(" Inner join Farmacia f on f.ID = u.FarmaciaID ");

                    cmd.CommandText = SQL.ToString();

                    _context.Database.OpenConnection();


                    using (var result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                Usuario PerguntaData = new Usuario();
                                PerguntaData.ID = result.GetInt32(0);
                                PerguntaData.Nome = result.GetString(1);
                                PerguntaData.Email = result.GetString(2);
                                PerguntaData.Senha = result.GetString(3);
                                PerguntaData.FarmaciaID = result.GetInt32(4);
                                PerguntaData.FarmaciaNome = result.GetString(5);
                                _listaUsuario.Add(PerguntaData);
                            }
                            result.Close();
                            _context.Database.CloseConnection();
                            return _listaUsuario;
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

        public int Insert(Usuario item)
        {
            int ChamadoID = 0;
            StringBuilder SQL = new StringBuilder();
            List<SqlParameter> Param = new List<SqlParameter>();
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                SQL.AppendLine(" INSERT INTO Usuario (Nome, Email, Senha, FarmaciaID) ");
                SQL.AppendLine(" VALUES ( ");
                SQL.AppendLine(" 	@Nome, ");
                SQL.AppendLine(" 	@Email, ");
                SQL.AppendLine(" 	@Senha, ");
                SQL.AppendLine(" 	@FarmaciaID ");
                SQL.AppendLine(" 	) ");

                cmd.CommandText = SQL.ToString();

                cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));
                cmd.Parameters.Add(new SqlParameter("@Email", item.Email));
                cmd.Parameters.Add(new SqlParameter("@Senha", item.Senha));
                cmd.Parameters.Add(new SqlParameter("@FarmaciaID", item.FarmaciaID));

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

        public int Update(Usuario item)
        {
            try
            {
                int updatedRows = 0;
                StringBuilder SQL = new StringBuilder();
                List<SqlParameter> Param = new List<SqlParameter>();
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    SQL.AppendLine(" UPDATE Usuario SET");
                    SQL.AppendLine(" 	Nome = @Nome, ");
                    SQL.AppendLine(" 	Email = @Email, ");
                    SQL.AppendLine(" 	Senha = @Senha, ");
                    SQL.AppendLine(" 	FarmaciaID = @FarmaciaID ");
                    SQL.AppendLine("    WHERE ID = @ID; ");

                    cmd.CommandText = SQL.ToString();

                    cmd.Parameters.Add(new SqlParameter("@Nome", item.Nome));
                    cmd.Parameters.Add(new SqlParameter("@Email", item.Email));
                    cmd.Parameters.Add(new SqlParameter("@Senha", item.Senha));
                    cmd.Parameters.Add(new SqlParameter("@FarmaciaID", item.FarmaciaID));
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
                    SQL.AppendLine(" Delete Usuario");
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
