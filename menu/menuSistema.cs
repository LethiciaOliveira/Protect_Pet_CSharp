using Services;

namespace Menu
{
    public class MenuSistema
    {
        public static void Iniciar(int tutorId)
        {
            bool executando = true;

            while (executando)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1 - Cadastrar Pet");
                Console.WriteLine("2 - Listar Pets");
                Console.WriteLine("3 - Excluir Pet");
                Console.WriteLine("4 - Alterar Pet");
                Console.WriteLine("5 - Alterar Tutor");
                Console.WriteLine("6 - Meus Dados");
                Console.WriteLine("7 - Sair");

                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":

                        PetService.CadastrarPet(tutorId);
                        break;

                    case "2":

                        PetService.ListarPets(tutorId);
                        break;

                    case "3":

                        PetService.ExcluirPet(tutorId);
                        break;

                    case "4":

                        PetService.AlterarPet(tutorId);
                        break;

                    case "5":

                        TutorService.AlterarTutor(tutorId);
                        break;

                    case "6":

                        TutorService.MostrarDadosTutor(tutorId);
                        break;

                    case "7":

                        Console.WriteLine("Encerrando sessão...");
                        executando = false;
                        break;

                    default:

                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
