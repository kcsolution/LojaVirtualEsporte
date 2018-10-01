using LojaEsportes.Dominio.Entidades;
using LojaEsportes.Dominio.Repositorio;
using LojaEsportes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaEsportes.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private IRepositorio<Produto> _produtoRepositorio;

        public CarrinhoController()
        {
            _produtoRepositorio = new ProdutoRepositorio(new ProdutoContexto());
        }

        // GET: Carrinho
        public ActionResult Index(string ReturnUrl)
        {
            return View(new CarrinhoViewModel { Carrinho = GetCarrinho(), ReturnUrl = ReturnUrl });
        }

        public RedirectToRouteResult AdicionarItemAoCarrinho(int ProdutoId,string ReturnUrl)
        {
            Produto _produto = _produtoRepositorio.GetTodos().FirstOrDefault(p => p.ProdutoId == ProdutoId);

            if (_produto != null)
            {
                GetCarrinho().AdicionarItem(_produto, 1);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        public RedirectToRouteResult RemoverItemDoCarrinho(int ProdutoId,string ReturnUrl)
        {
            Produto _produto = _produtoRepositorio.GetTodos().FirstOrDefault(p => p.ProdutoId == ProdutoId);

            if (_produto != null)
            {
                GetCarrinho().RemoverItem(_produto);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        private Carrinho GetCarrinho()
        {
            Carrinho _carrinho = (Carrinho)Session["Carrinho"];

            if (_carrinho == null)
            {
                _carrinho = new Carrinho();
                Session["Carrinho"] = _carrinho;
            }
            return _carrinho;
        }
    }
}