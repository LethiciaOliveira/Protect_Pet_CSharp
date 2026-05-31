using Npgsql;

namespace Database
{
    public class Conexao
    {
        private static string stringConexao =
        "Host=localhost;Port=5432;Username=postgres;Password=santiago1720;Database=pet_protecao";
    
        public static NpgsqlConnection ObterConexao()
        {
            var conn = new NpgsqlConnection(stringConexao);
            conn.Open();
            return conn;
        }
    }
}
