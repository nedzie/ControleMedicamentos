﻿using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class TelaCadastroPaciente : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioPaciente _repositorioPaciente;
        private readonly Notificador _notificador;

        public TelaCadastroPaciente(RepositorioPaciente repositorioPaciente, Notificador notificador)
            : base("Cadastro de Pacientes")
        {
            _repositorioPaciente = repositorioPaciente;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Paciente");

            Paciente novoPaciente = ObterPaciente();

            _repositorioPaciente.Inserir(novoPaciente);

            _notificador.ApresentarMensagem("Paciente cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Paciente");

            bool temPacientesCadastrados = VisualizarRegistros("Pesquisando");

            if (temPacientesCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            Paciente PacienteAtualizado = ObterPaciente();

            bool conseguiuEditar = _repositorioPaciente.Editar(numeroPaciente, PacienteAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Funcionário");

            bool temPacientesRegistrados = VisualizarRegistros("Pesquisando");

            if (temPacientesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioPaciente.Excluir(numeroPaciente);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Pacientes");

            List<Paciente> Pacientes = _repositorioPaciente.SelecionarTodos();

            if (Pacientes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Paciente Paciente in Pacientes)
                Console.WriteLine(Paciente.ToString());

            Console.ReadLine();

            return true;
        }

        private Paciente ObterPaciente()
        {
            Console.WriteLine("Digite o nome do paciente: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o CPF do paciente: ");
            string cpf = Console.ReadLine();
            return new Paciente(nome, cpf);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Funcionário que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioPaciente.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Funcionário não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
