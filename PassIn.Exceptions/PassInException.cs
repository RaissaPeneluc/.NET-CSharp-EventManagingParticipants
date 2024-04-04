namespace PassIn.Exceptions
{
    /* Função dessa classe: É preciso criar essa própria exception customizada 
     * para garantir no catch, que o .Message só vai ser feito, que só pode confiar 
     * na mensagem da exceção, se somente se, for um PassInException, pois significa 
     * que é uma exceção tratada.
     */

    // Utilização de herança, para que essa classe seja vista como uma exception
    public class PassInException : SystemException
    {
        // Utilizando a base para repassar a mensagem para o construtor SystemException
        public PassInException(string message) : base(message)
        {
            
        }
    }
}
