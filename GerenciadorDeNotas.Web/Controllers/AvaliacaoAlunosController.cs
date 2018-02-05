using GerenciadorDeNotas.Dados.Infraestrutura;
using GerenciadorDeNotas.Dados.Repositorios;
using GerenciadorDeNotas.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GerenciadorDeNotas.Web.Controllers
{
    public class AvaliacaoAlunosController : Controller
    {

        private readonly IEntidadeBaseRepositorio<Aluno> _repositorioAlunos;
        private readonly IEntidadeBaseRepositorio<Avaliacao> _repositorioAvaliacoes;
        private readonly IUnitOfWork _unitOfWork;


        public AvaliacaoAlunosController(IEntidadeBaseRepositorio<Aluno> repositorioAlunos,
                                         IEntidadeBaseRepositorio<Avaliacao> repositorioAvaliacoes,
                                         IUnitOfWork unitOfWork)
        {
            _repositorioAlunos = repositorioAlunos;
            _repositorioAvaliacoes = repositorioAvaliacoes;
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        [Route("api/alunos/criar")]
        public IActionResult Create(string nome, string matricula)
        {

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(matricula))
            {
                return BadRequest();
            }

            Aluno aluno = new Aluno
            {
                NomeCompleto = nome,
                Matricula = matricula
            };

            if (_repositorioAlunos.
                    FindBy(a => a.NomeCompleto == nome || a.Matricula == matricula).Any())
            {
                return new ObjectResult(new { Menssagem = "Já existe um aluno com esse nome ou matricula" });
            }

            _repositorioAlunos.Add(aluno);
            _unitOfWork.Commit();

            return new ObjectResult(new { Menssagem = "Aluno cadastrado com sucesso"});
        }

        [HttpPost]
        [Route("api/alunos/nota")]
        public IActionResult Avaliacao(string matricula, decimal nota)
        {

            if (string.IsNullOrEmpty(matricula))
            {
                return BadRequest();
            }

            Aluno aluno = _repositorioAlunos.FindBy(a => a.Matricula == matricula).SingleOrDefault();

            if (aluno == null)
            {
                return new ObjectResult(new { Menssagem = "Aluna inexistente" });
            }

            if (_repositorioAvaliacoes.All.Where(a => a.AlunoId == aluno.ID).Count() < 4)
            {
                Avaliacao avaliacao = new Avaliacao
                {
                    Nota = nota,
                    AlunoId = aluno.ID
                };

                _repositorioAvaliacoes.Add(avaliacao);
                _unitOfWork.Commit();

                return new ObjectResult(new { Menssagem = "Nota Cadastrada com sucesso." });
            }

            return new ObjectResult(new { Menssagem = "Avaliações já cadastradas" });
        }

    }
}
