using Aplicacao;
using CRUD_Axado.App_Start;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Axado.Controllers
{
    /// <summary>
    /// Controlador HOME
    /// Métodos de cadastro edição e alteração, assim como avaliação da transportadora
    /// Permissoes definidias com PermissaoFiltro
    /// Ex: 
    ///      [PermissaoFiltro(Roles = "Administrador, Usuario")] (Apenas Usuario e Administrador) acessa.
    ///      
    /// Autor: Leonardo de Aquino Neto | Axado Teste
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [PermissaoFiltro(Roles = "Administrador, Usuario")]
        public ActionResult Index()
        {
            UsuarioAuth auth = new UsuarioAuth();//coleta usuaro autenticado
            var permissoes = auth.getPermissoesDoUsuario();//Coleta permissoes

            //Coloca na viewbag para mostrar na View
            ViewBag.isAdministrador = permissoes.Contains("Administrador");
            ViewBag.isUsuario = permissoes.Contains("Usuario");
            ViewBag.Usario = auth.getUsuarioLogado();

            //Coleta lista de transportadoras
            var appTransportadora = new TransportadoraAplicacao();
            var listaDeAlunos = appTransportadora.ListarTodos();

            return View(listaDeAlunos);
        }

        /// <summary>
        /// Acessar a view de cadastro de transportadora
        /// Nível de acesso aceito: Administrador
        /// </summary>
        /// <returns>View</returns>
        [PermissaoFiltro(Roles = "Administrador")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        /// <summary>
        /// Avaliar uma transpoortadora
        /// Nível de acesso: Usuario (Somente)
        /// </summary>
        /// <param name="t">Transportadora a ser avaliada</param>
        /// <returns>View</returns>
        /// 
        [PermissaoFiltro(Roles = "Usuario")]
        [HttpPost]
        public ActionResult AvaliarTransportadora(Transportadora t)
        {
            UsuarioAuth auth = new UsuarioAuth();

            //Verifica se já avaliou uma tranportadora antes
            if (auth.getAvaliacao(t.Codigo) != AVALIACAO_TIPO.Nenhuma)
            {
                ModelState.AddModelError("", "Voce já avaliou esta tranportadora!");
            }
            else
            {
                int avaliacao = -1;
                try
                {
                    avaliacao = Convert.ToInt32(HttpContext.Request.Params.Get("avaliacao"));

                    if (avaliacao < 0)
                    {
                        ModelState.AddModelError("", "Selecione uma avaliação!");
                    }
                    else
                    {
                        //Avalia a tranpostadora e retorna para pagina inicial
                        auth.avaliarTransportadora(t, avaliacao);
                        return RedirectToAction("Index");
                    }
                }
                catch (FormatException /*Verificar uma possivel passagem de parametros maliciosa em 'avaliacao' */)
                {
                    ModelState.AddModelError("", "Valor inválido!");
                }
            }
            return View(t);
        }

        public ActionResult Negado()
        {
            return View();
        }

        /// <summary>
        /// Cadastra transportadora 
        /// Nível de acesso: Adminitrador (Somente)
        /// </summary>
        /// <param name="t">Transportadora a ser cadastrada</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissaoFiltro(Roles = "Administrador")]
        public ActionResult Cadastrar(Transportadora t)
        {
            if (ModelState.IsValid) //verifica a validade do form
            {
                var appAluno = new TransportadoraAplicacao();
                appAluno.Salvar(t);//chama a função de salvamento
                return RedirectToAction("Index");
            }
            return View(t);
        }

        /// <summary>
        /// Acessar os detalhes da transportadora 
        /// Nível de acesso definido em PermissaoFiltro
        /// </summary>
        /// <param name="id">Id da transportadoa</param>
        /// <returns></returns>
        [PermissaoFiltro(Roles = "Administrador, Usuario")]
        public ActionResult Detalhes(int id)
        {
            TransportadoraAplicacao transportadoraApp = new TransportadoraAplicacao();
            Transportadora transportadora = transportadoraApp.ListarPorId(id);

            UsuarioAuth auth = new UsuarioAuth();
            var permissoes = auth.getPermissoesDoUsuario();

            //é necessário enviar para view um valor lógico mostrando que este usuario pode avaliar a transportadora
            ViewBag.podeAvaliar = permissoes.Contains("Usuario") && auth.getAvaliacao(transportadora.Codigo) == AVALIACAO_TIPO.Nenhuma;

            //teoricamente transportadora nunca vai ser null, mas por segurança coloquei este check null
            if (transportadora == null)
            {
                return RedirectToAction("Index");
            }

            return View(transportadora);
        }

        /// <summary>
        /// Editar uma tranpostadora
        /// </summary>
        /// <param name="id">Id da transportaodra</param>
        /// <returns>View</returns>
        [PermissaoFiltro(Roles = "Administrador")]
        public ActionResult Editar(int id)
        {
            var appTransportadora = new TransportadoraAplicacao();
            var transportadora = appTransportadora.ListarPorId(id);

            if (transportadora == null)
                return HttpNotFound();

            return View(transportadora);
        }

        /// <summary>
        /// Abrir pagina de Avaliar uma transportadora
        /// </summary>
        /// <param name="id">Id da tranportaora</param>
        /// <returns></returns>
        [PermissaoFiltro(Roles = "Usuario")]
        public ActionResult AvaliarTransportadora(int id)
        {
            TransportadoraAplicacao transportadoraApp = new TransportadoraAplicacao();
            Transportadora transportadora = transportadoraApp.ListarPorId(id);

            if (transportadora == null)
            {
                return RedirectToAction("Index");
            }

            return View(transportadora);
        }

        /// <summary>
        /// Efetivar edição de tranportadora
        /// </summary>
        /// <param name="t">Transportadora</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissaoFiltro(Roles = "Administrador")]
        public ActionResult Editar(Transportadora t)
        {
            if (ModelState.IsValid)
            {
                var appTransportadora = new TransportadoraAplicacao();
                appTransportadora.Salvar(t);//chama metodo salvar
                return RedirectToAction("Index");
            }
            return View(t);
        }

        /// <summary>
        /// Confirmacao de exclusao de uma tranportadora, necessita do Adminitrador
        /// </summary>
        /// <param name="id">ID da transportadora</param>
        /// <returns>View</returns>
        [PermissaoFiltro(Roles = "Administrador")]
        public ActionResult Excluir(int id)
        {
            var appTransportadora = new TransportadoraAplicacao();
            var transportadora = appTransportadora.ListarPorId(id);

            if (transportadora == null)
                return HttpNotFound();

            return View(transportadora);
        }

        /// <summary>
        /// Confirmou a exclusão ...
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [PermissaoFiltro(Roles = "Administrador")]
        public ActionResult ExcluirConfirmado(int id)
        {
            var appTransportadora = new TransportadoraAplicacao();
            appTransportadora.Excluir(id);//chama método exlusao
            return RedirectToAction("Index");
        }


    }
}
