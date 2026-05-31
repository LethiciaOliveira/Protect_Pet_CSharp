using Menu;
using Services;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            int tutorId = LoginService.FazerLogin(login, senha);

            if (tutorId == 0)
            {
                Console.WriteLine("\nLogin inválido!");
                Console.WriteLine("Pressione ENTER para tentar novamente...");
                Console.ReadLine();

                continue;
            }

            Console.WriteLine("\nLogin realizado com sucesso!");

            MenuSistema.Iniciar(tutorId);
        }
    }
}