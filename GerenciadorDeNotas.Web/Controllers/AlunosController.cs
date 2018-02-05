using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorDeNotas.Dados.Repositorios;
using GerenciadorDeNotas.Entidades;
using GerenciadorDeNotas.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorDeNotas.Web.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IEntidadeBaseRepositorio<Aluno> _repositorioAlunos;
        private readonly IEntidadeBaseRepositorio<Cidade> _repositorioCidades;

        public AlunosController(IEntidadeBaseRepositorio<Aluno> repositorioAlunos, IEntidadeBaseRepositorio<Cidade> repositorioCidades)
        {
            _repositorioAlunos = repositorioAlunos;
            _repositorioCidades = repositorioCidades;
        }

        // GET: Alunos
        public ActionResult Index(int? page)
        {

            int totalAlunos = (from aluno in _repositorioAlunos.All
                       select aluno).Count();

            Pager pager = new Pager(totalAlunos, page);

            IQueryable<Aluno> itens = _repositorioAlunos.AllIncluding(a => a.Avaliacoes);
            List<Aluno> alunos = itens.OrderBy(a => a.NomeCompleto).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            
            List<AlunoViewModel> alunosVM = new List<AlunoViewModel>();

            foreach(Aluno aluno in alunos)
            {
                AlunoViewModel alunoVM = new AlunoViewModel
                {
                    ID = aluno.ID,
                    NomeCompleto = aluno.NomeCompleto,
                    Matricula = aluno.Matricula,
                    Foto = aluno.Foto,
                    Telefone = aluno.Telefone,
                    EMail = aluno.EMail,
                    CidadeId = aluno.CidadeId

                };
                alunosVM.Add(alunoVM);
            }

            var viewModel = new IndexViewModel
            {
                Items = alunosVM,
                Pager = pager
            };

            return View(viewModel);
        }

        // GET: Alunos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alunos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Alunos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Aluno aluno = _repositorioAlunos.GetSingle((int)id);
            if (aluno == null)
            {
                return NotFound();
            }

            AlunoViewModel alunoVM = new AlunoViewModel
            {
                ID = aluno.ID,
                NomeCompleto = aluno.NomeCompleto,
                Matricula = aluno.Matricula,
                Foto = aluno.Foto,
                Telefone = aluno.Telefone,
                EMail = aluno.EMail,
                CidadeId = aluno.CidadeId
            };

            ViewBag.Cidades = _repositorioCidades.All;

            return View(alunoVM);
        }

        // POST: Alunos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Alunos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alunos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}