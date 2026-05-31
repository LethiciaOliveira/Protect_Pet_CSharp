using Database;
using Npgsql;

namespace Services
{
    public class LoginService
    {
        public static int FazerLogin(string login, string senha)
        {
            using var conn = Conexao.ObterConexao();

            string sql = @"
                SELECT t.id
                FROM usuarios u

                JOIN tutores t
                    ON u.tutor_id = t.id

                WHERE u.login = @login
                AND u.senha = @senha
            ";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@login", login ?? "");
            cmd.Parameters.AddWithValue("@senha", senha ?? "");

            var resultado = cmd.ExecuteScalar();

            if (resultado == null)
                return 0;

            return Convert.ToInt32(resultado);
        }
    }
}