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
    internal class TelaCadastroRequisicao : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioRequisicao _repositorioRequisicao;
        private readonly RepositorioPaciente _repositorioPaciente;
        private readonly RepositorioMedicamento _repositorioMedicamento;
        private readonly TelaCadastroPaciente _telaCadastroPaciente;
        private readonly TelaCadastroMedicamento _telaCadastroMedicamento;
        private readonly Notificador _notificador;
        public TelaCadastroRequisicao(RepositorioRequisicao repositorioRequisicao, RepositorioPaciente repositorioPaciente, RepositorioMedicamento repositorioMedicamento, TelaCadastroPaciente telaCadastroPaciente, TelaCadastroMedicamento telaCadastroMedicamento, Notificador notificador) : base("Cadastro de Requisições")
        {
            _repositorioRequisicao = repositorioRequisicao;
            _repositorioPaciente = repositorioPaciente;
            _repositorioMedicamento = repositorioMedicamento;
            _telaCadastroPaciente = telaCadastroPaciente;
            _telaCadastroMedicamento = telaCadastroMedicamento;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Requisicao");

            Requisicao novaRequisicao = ObterRequisicao();

            _repositorioMedicamento.DiminuirQuantidade(novaRequisicao._medicamento, novaRequisicao._quantidade);
            _repositorioRequisicao.Inserir(novaRequisicao);

            _notificador.ApresentarMensagem("Requisicao cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Requisicao");

            bool temRequisicoesCadastradas = VisualizarRegistros("Pesquisando");

            if (!temRequisicoesCadastradas)
            {
                _notificador.ApresentarMensagem("Nenhuma requisição cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            Requisicao RequisicaoAtualizada = ObterRequisicao();

            bool conseguiuEditar = _repositorioRequisicao.Editar(numeroRequisicao, RequisicaoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Requisicao editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Funcionário");

            bool temRequisicoesRegistrados = VisualizarRegistros("Pesquisando");

            if (temRequisicoesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioRequisicao.Excluir(numeroRequisicao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Requisicao excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Requisicoes");

            List<Requisicao> Requisicoes = _repositorioRequisicao.SelecionarTodos();

            if (Requisicoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao Requisicao in Requisicoes)
                Console.WriteLine(Requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosDiferentao(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Requisicoes");

            List<Requisicao> Requisicoes = _repositorioRequisicao.SelecionarTodos();

            if (Requisicoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao Requisicao in Requisicoes)
                Console.WriteLine(Requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        private Requisicao ObterRequisicao()
        {
            _telaCadastroPaciente.VisualizarRegistros("Pesquisando");

            Console.WriteLine("Qual desses pacientes vai requisitar? ");
            int pacienteSelecionado = int.Parse(Console.ReadLine());

            Paciente paciente = _repositorioPaciente.SelecionarRegistro(pacienteSelecionado);

            _telaCadastroMedicamento.VisualizarRegistros("Pesquisando");
            Console.WriteLine("Qual desses medicamentos " + paciente.Nome + " pegou?" );
            int medicamentoSelecionado = int.Parse(Console.ReadLine());

            Medicamento medicamento = _repositorioMedicamento.SelecionarRegistro(medicamentoSelecionado);

            Console.WriteLine("Qual a quantidade de " + medicamento.Nome + " que " + paciente.Nome + " retirou?");
            int quantidade = int.Parse(Console.ReadLine());

            DateTime data = DateTime.Today;

            return new Requisicao(paciente, medicamento, quantidade, data);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Funcionário que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioRequisicao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Funcionário não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}