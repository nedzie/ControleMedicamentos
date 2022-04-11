using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class Medicamento : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _descricao;
        private readonly int _quantidade;

        public string Nome { get => _nome; }
        public string Descricao { get => _descricao; }
        public int Quantidade { get => _quantidade; }

        public Medicamento(string nome, string descricao, int quantidade)
        {
            _nome = nome;
            _descricao = descricao;
            _quantidade = quantidade;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Descrição: " + Descricao + Environment.NewLine +
                "Quantidade: " + Quantidade + Environment.NewLine;
        }
    }
}
