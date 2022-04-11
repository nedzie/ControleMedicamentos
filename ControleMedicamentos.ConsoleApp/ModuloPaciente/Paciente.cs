using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class Paciente : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _cpf;
        public string Nome { get => _nome; }
        public string CPF { get => _cpf; }

        public Paciente(string nome, string cpf)
        {
            _nome = nome;
            _cpf = cpf;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "CPF: " + CPF;
        }
    }
}