using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    internal class Requisicao : EntidadeBase
    {
        public Paciente _paciente;
        public Medicamento _medicamento;
        public int _quantidade;
        DateTime _dataDaRequisicao;

        public Requisicao(Paciente paciente, Medicamento medicamento, int quantidade, DateTime data)
        {
            _paciente = paciente;
            _medicamento = medicamento;
            _quantidade = quantidade;
            _dataDaRequisicao = data;
        }

        public override string ToString()
        {
            return "Paciente: " + _paciente + Environment.NewLine +
                "Medicamento: " + _medicamento + Environment.NewLine +
                "Data: " + _dataDaRequisicao;
        }
    }
}
