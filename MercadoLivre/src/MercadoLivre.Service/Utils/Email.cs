using System;

namespace MercadoLivre.Service.Utils
{
    public class Email
    {
        public Email(string emailDestino, string assunto, string mensagem)
        {
            EmailDestino = emailDestino;
            Assunto = assunto;
            Mensagem = mensagem;
        }
        public string EmailDestino { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }

        public void SendEmail()
        {
            Console.WriteLine($@"
Para: {this.EmailDestino}
Assunto: {this.Assunto}

Corpo: 
{this.Mensagem}

");
        }
    }
}