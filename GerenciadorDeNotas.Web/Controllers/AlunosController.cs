﻿using GerenciadorDeNotas.Dados.Infraestrutura;
using GerenciadorDeNotas.Dados.Repositorios;
using GerenciadorDeNotas.Entidades;
using GerenciadorDeNotas.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace GerenciadorDeNotas.Web.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IEntidadeBaseRepositorio<Aluno> _repositorioAlunos;
        private readonly IEntidadeBaseRepositorio<Cidade> _repositorioCidades;
        private readonly IEntidadeBaseRepositorio<Avaliacao> _repositorioAvaliacoes;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IHostingEnvironment _environment;

        public AlunosController(IEntidadeBaseRepositorio<Aluno> repositorioAlunos, 
                                IEntidadeBaseRepositorio<Cidade> repositorioCidades,
                                IEntidadeBaseRepositorio<Avaliacao> repositorioAvaliacoes,
                                IUnitOfWork unitOfWork,
                                IHostingEnvironment environment)
        {
            _repositorioAlunos = repositorioAlunos;
            _repositorioCidades = repositorioCidades;
            _repositorioAvaliacoes = repositorioAvaliacoes;
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        // GET: Alunos
        public ActionResult Index(int? page)
        {

            int totalAlunos = (from aluno in _repositorioAlunos.All
                       select aluno).Count();

            Pager pager = new Pager(totalAlunos, page);

            IQueryable<Aluno> itens = _repositorioAlunos.All;
            List<Aluno> alunos = itens.OrderBy(a => a.NomeCompleto)
                    .Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            
            List<AlunoViewModel> alunosVM = new List<AlunoViewModel>();

            foreach(Aluno aluno in alunos)
            {
                AlunoViewModel alunoVM = new AlunoViewModel
                {
                    ID = aluno.ID,
                    NomeCompleto = aluno.NomeCompleto,
                  
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
        public ActionResult Details(int? id)
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
                Avaliacoes = _repositorioAvaliacoes.All.Where(avaliacao => avaliacao.AlunoId == aluno.ID).OrderBy(a => a.ID).ToList()
            };

            return View(alunoVM);
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
                CidadeId = aluno.CidadeId != null ? aluno.CidadeId.ToString() : null
            };

            ViewBag.Cidades = _repositorioCidades.All;

            return View(alunoVM);
        }

        // POST: Alunos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlunoViewModel alunoVM)
        {
            if (ModelState.IsValid)
            {
                bool fileUploaded = CarregarArquivo(alunoVM);

                Aluno aluno = _repositorioAlunos.GetSingle(alunoVM.ID);

                if (aluno != null)
                {
                    alunoVM.PupularAluno(aluno);

                    _repositorioAlunos.Edit(aluno);
                    _unitOfWork.Commit();

                    if (!fileUploaded)
                        return RedirectToAction("Index");
                    else
                    {
                        ViewBag.Cidades = _repositorioCidades.All;
                        return View(alunoVM);
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            ViewBag.Cidades = _repositorioCidades.All;
            return View(alunoVM);
        }

        private bool CarregarArquivo(AlunoViewModel alunoVM)
        {
            var files = HttpContext.Request.Form.Files;
            bool fileUploaded = false;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {

                    var file = Image;
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads\\img\\alunos");

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse
                            (file.ContentDisposition).FileName.Trim('"');

                        System.Console.WriteLine(fileName);
                        string fileNameWithPath = Path.Combine(uploads, file.FileName);
                        using (var fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            alunoVM.Foto = "~/uploads/img/alunos/" + file.FileName;
                            fileUploaded = true;
                        }

                    }
                }
            }

            return fileUploaded;
        }
        
    }
}