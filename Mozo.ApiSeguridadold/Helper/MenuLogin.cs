using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Helper.Enu;
using Mozo.Model.Seguridad;
using System.Collections.Generic;
using System.Linq;

namespace Mozo.ApiSeguridad.Helper;

public static class MenuLogin
{
    public static List<ModuloUsuarioModel> GetAllModulo(
        ModuloUsuarioModel Usuario
        , IModuloUsuarioService _serviceModuloUsuario
        , IMenuService _serviceMenu
        , IPaginaService _servicePagina
    )
    {
        var ModuloUsuarioCol = _serviceModuloUsuario.GetAllPersona(new ModuloUsuarioModel
        { CoEmpresa = Usuario.CoEmpresa, CoPersona = Usuario.CoPersona }).ToList();
        //ModuloUsuarioCol.Sort(new Sorter<ModuloUsuarioModel>("NuOrden"));
        //if (c.CoUsuario != -1)
        //{
        //    ModuloUsuarioCol = ModuloUsuarioCol.Where(x => x.CoModulo != -1);
        //}
        foreach (var ModuloUsuario in ModuloUsuarioCol.FindAll(x => x.CoModulo == Usuario.CoModulo))
            if (ModuloUsuario.CoModulo == -1)
            {
                ModuloUsuario.MenuCol = GetAllMenuSeguridad(ModuloUsuario.CoPerfil);
            }
            else if (ModuloUsuario.CoModulo == -2)
            {
                ModuloUsuario.MenuCol = GetAllMenuMaestro(ModuloUsuario.CoPerfil);
            }
            else
            {
                ModuloUsuario.MenuCol = _serviceMenu._GetAll(new MenuModel
                {
                    CoEmpresa = Usuario.CoEmpresa,
                    CoPersona = ModuloUsuario.CoPersona,
                    CoModulo = ModuloUsuario.CoModulo
                }).ToList();
                //ModuloUsuario.MenuCol.Sort(new Sorter<MenuModel>("NuOrden"));// .OrderBy(x => x.NuOrden);

                var PaginalCol = _servicePagina.GetAllPagina(new PaginaModel
                {
                    CoEmpresa = Usuario.CoEmpresa,
                    CoPersona = ModuloUsuario.CoPersona,
                    CoModulo = ModuloUsuario.CoModulo
                }).ToList();
                //PaginalCol.Sort(new Sorter<PaginaModel>("NuOrden"));

                var SubPaginaCol = _servicePagina.GetAllSubPagina(new SubPaginaModel
                {
                    CoEmpresa = Usuario.CoEmpresa,
                    CoPersona = ModuloUsuario.CoPersona,
                    CoModulo = ModuloUsuario.CoModulo
                }).ToList();
                //SubPaginaCol.Sort(new Sorter<SubPaginaModel>("NuOrden"));

                var PaginaFlotanteCol = _servicePagina.GetAllPaginaFlotante(new PaginaFlotanteModel
                {
                    CoEmpresa = Usuario.CoEmpresa,
                    CoPersona = ModuloUsuario.CoPersona,
                    CoModulo = ModuloUsuario.CoModulo
                }).ToList();

                var ServicioWebCol = _servicePagina.GetAllServicioWeb(new ServicioWebModel
                {
                    CoEmpresa = Usuario.CoEmpresa,
                    CoPersona = ModuloUsuario.CoPersona,
                    CoModulo = ModuloUsuario.CoModulo
                }).ToList();

                foreach (var Menu in ModuloUsuario.MenuCol)
                {
                    Menu.PaginalCol = PaginalCol.FindAll(x =>
                        x.CoMenu == Menu.CoMenu && x.CoTipoPagina == EnuSeguridad.TipoPagina.Pagina &&
                        (x.CoPaginaPadre == 0 || x.CoPaginaPadre == null));

                    foreach (var Pagina in Menu.PaginalCol)
                    {
                        Pagina.SubPaginaCol = SubPaginaCol.FindAll(x =>
                            x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);

                        Pagina.PaginaFlotanteCol = PaginaFlotanteCol.FindAll(x =>
                            x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);
                        Pagina.ServicioWebCol = ServicioWebCol.FindAll(x =>
                            x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);

                        foreach (var SubPagina in Pagina.SubPaginaCol)
                        {
                            SubPagina.PaginaFlotanteCol = PaginaFlotanteCol.FindAll(x =>
                                x.CoMenu == SubPagina.CoMenu && x.CoPaginaPadre == SubPagina.CoPagina);
                            SubPagina.ServicioWebCol = ServicioWebCol.FindAll(x =>
                                x.CoMenu == SubPagina.CoMenu && x.CoPaginaPadre == SubPagina.CoPagina);
                        }
                    }
                }
            }

        //if (ModuloUsuario.TxConfiguracion.NoNulo())
        //{
        //    if ( ModuloUsuario.CoModulo == EnuSeguridad.Modulo.Invoice.CoModulo)
        //    {
        //        InvoiceConfigModel InvoiceConfig = ModuloUsuario.TxConfiguracion.Deserializa<InvoiceConfigModel>();
        //        ModuloUsuario.Config = InvoiceConfig;
        //    }
        //    ModuloUsuario.TxConfiguracion = null;
        //}

        // ModuloUsuarioCol.Sort(new Sorter<ModuloUsuarioModel>("NuOrden"));
        return ModuloUsuarioCol;
    }


    private static List<MenuModel> GetAllMenuSeguridad(int? CoPerfil)
    {
        var MenuList = new List<MenuModel>
        {
            new() { CoMenu = -1, CoModulo = -1, NuOrden = 1, CoPerfil = -1, NoMenu = "Configuración" },
            new() { CoMenu = -3, CoModulo = -1, NuOrden = 1, CoPerfil = -1, NoMenu = "Mantenimiento" }
        };

        var PaginaList = new List<PaginaModel>
        {
            new()
            {
                CoModulo = -1, CoMenu = -1, CoTipoPagina = 1, CoPagina = -101, NoArea = "Seguridad",
                NoControlador = "Modulo", NoAccion = "Index", NoOpcion = "Módulo", NuOrden = 1
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoMenu = -1, CoTipoPagina = 1, CoPagina = -102, NoArea = "Seguridad",
                NoControlador = "Menu", NoAccion = "Index", NoOpcion = "Menus", NuOrden = 2
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoMenu = -1, CoTipoPagina = 1, CoPagina = -103, NoArea = "Seguridad",
                NoControlador = "Perfil", NoAccion = "Index", NoOpcion = "Perfiles", NuOrden = 3
            }, // Seguridad Administrador
            //new PaginaModel() { CoModulo =-1,CoMenu = -3, CoTipoPagina= 1, CoPagina= -104,  NoArea = "Seguridad", NoControlador = "Permiso", NoAccion = "Index", NoOpcion = "Accesos", NuOrden = 4 },// Seguridad Administrador
            new()
            {
                CoModulo = -1, CoMenu = -3, CoTipoPagina = 1, CoPagina = -104, NoArea = "Seguridad",
                NoControlador = "Empresa", NoAccion = "Index", NoOpcion = "Empresa", NuOrden = 1
            } // Maestro Administrador
        };

        var PaginaFlotanteList = new List<PaginaFlotanteModel>
        {
            new()
            {
                CoModulo = -1, CoPaginaPadre = -101, CoTipoPagina = 3, CoPagina = -1501, NoArea = "Seguridad",
                NoControlador = "Modulo", NoAccion = "GetAll"
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -101, CoTipoPagina = 3, CoPagina = -1502, NoArea = "Seguridad",
                NoControlador = "Modulo", NoAccion = "GetById"
            }, // Seguridad Administrador

            //Inicio opcion: Crear paginas y menus
            new()
            {
                CoModulo = -1, CoPaginaPadre = -102, CoTipoPagina = 3, CoPagina = -1601, NoArea = "Seguridad",
                NoControlador = "Menu", NoAccion = "GetAll"
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -102, CoTipoPagina = 3, CoPagina = -1602, NoArea = "Seguridad",
                NoControlador = "Menu", NoAccion = "GetById"
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -102, CoTipoPagina = 3, CoPagina = -1603, NoArea = "Seguridad",
                NoControlador = "Pagina", NoAccion = "GetById"
            }, // Seguridad Administrador
            //Fin opcion: Crear paginas y menus

            //Inicio Opcion Perfil
            new()
            {
                CoModulo = -1, CoPaginaPadre = -103, CoTipoPagina = 3, CoPagina = -1701, NoArea = "Seguridad",
                NoControlador = "Perfil", NoAccion = "GetAll"
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -103, CoTipoPagina = 3, CoPagina = -1702, NoArea = "Seguridad",
                NoControlador = "Perfil", NoAccion = "GetById"
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -103, CoTipoPagina = 3, CoPagina = -1703, NoArea = "Seguridad",
                NoControlador = "Privilegio", NoAccion = "GetAll"
            }, // Seguridad Administrador
            //Fin Opcion Perfil


            //Inicio Chekbox Empresa Modulo                
            new()
            {
                CoModulo = -1, CoPaginaPadre = -104, CoTipoPagina = 3, CoPagina = -1806, NoArea = "Seguridad",
                NoControlador = "EmpresaModulo", NoAccion = "GetAll"
            }, // Seguridad Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -104, CoTipoPagina = 3, CoPagina = -1807, NoArea = "Seguridad",
                NoControlador = "EmpresaModulo", NoAccion = "Index"
            }, // Seguridad Administrador
            //Fin Chekbox Empresa Modulo

            new()
            {
                CoModulo = -1, CoPaginaPadre = -104, CoTipoPagina = 3, CoPagina = -1900, NoArea = "Seguridad",
                NoControlador = "Empresa", NoAccion = "GetAll"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -104, CoTipoPagina = 3, CoPagina = -1901, NoArea = "Seguridad",
                NoControlador = "Empresa", NoAccion = "GetById"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -104, CoTipoPagina = 3, CoPagina = -1902, NoArea = "Seguridad",
                NoControlador = "Empresa", NoAccion = "GetByIdUser"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -1, CoPaginaPadre = -104, CoTipoPagina = 3, CoPagina = -1903, NoArea = "Seguridad",
                NoControlador = "Empresa", NoAccion = "_GetById"
            } // Maestro Administrador
        };

        var MenuList2 = MenuList.FindAll(x => x.CoPerfil == CoPerfil);

        foreach (var Menu in MenuList2)
        {
            Menu.PaginalCol = PaginaList.FindAll(x => x.CoMenu == Menu.CoMenu);
            foreach (var Pagina in Menu.PaginalCol)
                Pagina.PaginaFlotanteCol = PaginaFlotanteList.FindAll(x => x.CoPaginaPadre == Pagina.CoPagina);
        }

        return MenuList2;
    }


    private static List<MenuModel> GetAllMenuMaestro(int? CoPerfil)
    {
        var MenuList = new List<MenuModel>
        {
            new() { CoMenu = -2, CoModulo = -2, NuOrden = 1, CoPerfil = -2, NoMenu = "Tablas" }, // Administrador
            new() { CoMenu = -4, CoModulo = -2, NuOrden = 2, CoPerfil = -2, NoMenu = "Accesos" } //  Administrador

            //new MenuModel() { CoMenu = -6, CoModulo = -2, NuOrden = 1, CoPerfil= -3, NoMenu = "Tablas" } , // Usuario
            //new MenuModel() { CoMenu = -8, CoModulo = -2, NuOrden = 2, CoPerfil= -3, NoMenu = "Accesos"  }  // Usuario
        };

        var PaginaList = new List<PaginaModel>
        {
            //*Administrador*//
            new()
            {
                CoModulo = -2, CoMenu = -2, CoTipoPagina = 1, CoPagina = -5, NoArea = "Maestro", NoControlador = "Tipo",
                NoAccion = "IndexAdmin", NoOpcion = "Tipo", NuOrden = 1
            },
            new()
            {
                CoModulo = -2, CoMenu = -2, CoTipoPagina = 1, CoPagina = -3, NoArea = "Maestro",
                NoControlador = "Parametro", NoAccion = "IndexAdmin", NoOpcion = "Parametros", NuOrden = 2
            },
            new()
            {
                CoModulo = -2, CoMenu = -4, CoTipoPagina = 1, CoPagina = -4, NoArea = "Maestro",
                NoControlador = "Persona", NoAccion = "IndexAdmin", NoOpcion = "Persona", NuOrden = 3
            }
        };


        var PaginaFlotanteList = new List<PaginaFlotanteModel>
        {
            new()
            {
                CoModulo = -2, CoPaginaPadre = -3, CoTipoPagina = 3, CoPagina = -1201, NoArea = "Maestro",
                NoControlador = "Parametro", NoAccion = "GetAllAdmin"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -3, CoTipoPagina = 3, CoPagina = -1202, NoArea = "Maestro",
                NoControlador = "Parametro", NoAccion = "GetById"
            }, // Maestro Administrador


            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1301, NoArea = "Maestro",
                NoControlador = "Persona", NoAccion = "GetAllAdmin"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1302, NoArea = "Maestro",
                NoControlador = "Persona", NoAccion = "GetById"
            }, // Maestro Administrador                                
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1306, NoArea = "Maestro",
                NoControlador = "TelCor", NoAccion = "GetById"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1307, NoArea = "Maestro",
                NoControlador = "TelCor", NoAccion = "GetAll"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1307, NoArea = "Maestro",
                NoControlador = "TelCor", NoAccion = "Index"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1308, NoArea = "Maestro",
                NoControlador = "TipoPersona", NoAccion = "GetAll"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1308, NoArea = "Maestro",
                NoControlador = "TipoPersona", NoAccion = "Index"
            }, // Maestro Administrador


            new()
            {
                CoModulo = -2, CoPaginaPadre = -5, CoTipoPagina = 3, CoPagina = -1401, NoArea = "Maestro",
                NoControlador = "Tipo", NoAccion = "GetAllAdmin"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -5, CoTipoPagina = 3, CoPagina = -1402, NoArea = "Maestro",
                NoControlador = "Tipo", NoAccion = "GetAllHijo"
            }, // Maestro Administrador
            new()
            {
                CoModulo = -2, CoPaginaPadre = -5, CoTipoPagina = 3, CoPagina = -1403, NoArea = "Maestro",
                NoControlador = "Tipo", NoAccion = "GetById"
            }, // Maestro Administrador                                
            new()
            {
                CoModulo = -2, CoPaginaPadre = -5, CoTipoPagina = 3, CoPagina = -1404, NoArea = "Maestro",
                NoControlador = "Tipo", NoAccion = "GetByIdCommand"
            }, // Maestro Administrador                                
            new()
            {
                CoModulo = -2, CoPaginaPadre = -5, CoTipoPagina = 3, CoPagina = -1405, NoArea = "Maestro",
                NoControlador = "Tipo", NoAccion = "IndexHijo"
            }, // Maestro Administrador                


            //Inicio Modulo Usuario dentro del la opción Acceso
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1801, NoArea = "Maestro",
                NoControlador = "ModuloUsuario", NoAccion = "GetAll"
            }, // Seguridad Usuario
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1802, NoArea = "Maestro",
                NoControlador = "ModuloUsuario", NoAccion = "GetById"
            }, // Seguridad Usuario
            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1803, NoArea = "Maestro",
                NoControlador = "ModuloUsuario", NoAccion = "Index"
            }, // Seguridad Usuario
            // new PaginaFlotanteModel {CoModulo =-2,CoPaginaPadre= -105,CoTipoPagina= 3, CoPagina=-1803, NoArea = "Seguridad", NoControlador = "ModuloUsuario", NoAccion = "Index"}, // Seguridad Usuario


            new()
            {
                CoModulo = -2, CoPaginaPadre = -4, CoTipoPagina = 3, CoPagina = -1806, NoArea = "Maestro",
                NoControlador = "Permiso", NoAccion = "GetById"
            } // Seguridad Usuario


            //Fin Modulo Usuario dentro del la opción Acceso
        };


        var MenuList2 = MenuList.FindAll(x => x.CoPerfil == CoPerfil);
        foreach (var Menu in MenuList2)
        {
            Menu.PaginalCol = PaginaList.FindAll(x => x.CoMenu == Menu.CoMenu);
            foreach (var Pagina in Menu.PaginalCol)
                Pagina.PaginaFlotanteCol = PaginaFlotanteList.FindAll(x => x.CoPaginaPadre == Pagina.CoPagina);
        }

        return MenuList2;
    }
}