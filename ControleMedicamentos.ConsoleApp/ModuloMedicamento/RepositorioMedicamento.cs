using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class RepositorioMedicamento : RepositorioBase<Medicamento>
    {
        public override string Inserir(Medicamento entidade)
        {
            if (registros.Count >= 1)
            {
                bool maisQueUm = false;
                int posicao = registros.FindIndex(x => x.Nome == entidade.Nome);
                if (posicao != -1)
                     maisQueUm = true;
                if (maisQueUm)
                {
                    int manterId = registros[posicao].id;
                    SomarQuantidade(posicao, entidade, manterId);
                    return "REGISTRO_VALIDO";
                }
            }
            entidade.id = ++contadorId;
            registros.Add(entidade);
            return "REGISTRO_VALIDO";
        }

        public void SomarQuantidade(int posicao, Medicamento entidade, int manterId)
        {
            string nome = entidade.Nome;
            string descricao = entidade.Descricao;
            int novaQuantidade = registros[posicao].Quantidade + entidade.Quantidade;
            registros.RemoveAt(posicao);
            Medicamento medicamentoJaExistente = new(nome, descricao, novaQuantidade);
            medicamentoJaExistente.id = manterId;
            registros.Insert(posicao, medicamentoJaExistente);
        }

        public void DiminuirQuantidade(Medicamento medicamento, int quantidadeRetirada)
        {
            int posicao = registros.FindIndex(x => x.Nome == medicamento.Nome);
            string nome = medicamento.Nome;
            string descricao = medicamento.Descricao;
            int novaQuantidade = registros[posicao].Quantidade - quantidadeRetirada;
            registros.RemoveAt(posicao);
            Medicamento medicamentoAtualizado = new(nome, descricao,novaQuantidade);
            medicamentoAtualizado.id = medicamento.id;
            registros.Insert(posicao, medicamentoAtualizado);
        }

        public List<Medicamento> SelecionarEmFalta()
        {
            List<Medicamento> medicamentosEmFalta = new List<Medicamento>();
            foreach (Medicamento medicamento in registros)
            {
                if(medicamento.Quantidade <= 0)
                    medicamentosEmFalta.Add(medicamento);
            }
            return medicamentosEmFalta;
        }

        public List<Medicamento> SelecionarMaisRequisitados()
        {
            List<Medicamento> medicamentosMaisRequisitados = new List<Medicamento>();
            
            return medicamentosMaisRequisitados;
        }
    }
}