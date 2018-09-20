using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.ViewModels;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Enumeradores;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/FichasMedicas")]
    public class FichasMedicasController : Controller
    {
        private readonly SwTHDbContext db;

        public FichasMedicasController(SwTHDbContext db)
        {
            this.db = db;
        }

        

        
        [HttpPost]
        [Route("ListarFichaMedicaViewModel")]
        public async Task<Response> ListarFichaMedicaViewModel([FromBody] FichaMedicaViewModel fmv)
        {

            try
            {
                Persona personaVar = new Persona();

                if (fmv.DatosBasicosPersonaViewModel.IdPersona < 1)
                {
                    try
                    {
                        personaVar = db.Persona
                            .Where(x => x.Identificacion == fmv.DatosBasicosPersonaViewModel.Identificacion)
                            .FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };
                    }

                }
                else
                {
                    try
                    {
                        personaVar = db.Persona
                            .Where(x => x.IdPersona == fmv.DatosBasicosPersonaViewModel.IdPersona)
                            .FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };
                    }
                }



                /*

                if (personaVar == null)
                {
                    return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };
                }


                PersonaEstudio personaEstudioVar = db.PersonaEstudio.Where(x => x.IdPersona == personaVar.IdPersona).FirstOrDefault();
                Sexo sexoVar = db.Sexo.Where(x => x.IdSexo == personaVar.IdSexo).FirstOrDefault();
                Nacionalidad NacionalidadVar = db.Nacionalidad.Where(x => x.IdNacionalidad == personaVar.IdNacionalidad).FirstOrDefault();
                Parroquia parroquiaVar = db.Parroquia.Where(x => x.IdParroquia == personaVar.IdParroquia).FirstOrDefault();
                EstadoCivil estadoCivilVar = db.EstadoCivil.Where(x => x.IdEstadoCivil == personaVar.IdEstadoCivil).FirstOrDefault();
                Etnia etniaVar = db.Etnia.Where(x => x.IdEtnia == personaVar.IdEtnia).FirstOrDefault();
                TipoSangre tipoSangreVar = db.TipoSangre.Where(x => x.IdTipoSangre == personaVar.IdTipoSangre).FirstOrDefault();
                PersonaDiscapacidad personaDiscapacidadVar = db.PersonaDiscapacidad.Where(x => x.IdPersona == personaVar.IdPersona).FirstOrDefault();
                Empleado empleadoVar = db.Empleado.Where(x => x.IdPersona == personaVar.IdPersona).FirstOrDefault();


                // INICIALIZACIÓN DE OBJETOS CON VALORES NULOS

                Titulo tituloVar = new Titulo();
                Estudio estudioVar = new Estudio();
                EmpleadoContactoEmergencia contactoEmergenciaVar = new EmpleadoContactoEmergencia();
                Dependencia dependenciaVar = new Dependencia();
                EmpleadoFamiliar empleadoFamiliarVar = new EmpleadoFamiliar();
                Persona personaEmergenciaVar = new Persona();
                Sucursal sucursalVar = new Sucursal();
                Parentesco parentescoVar = new Parentesco();
                TipoDiscapacidad discapacidadVar = new TipoDiscapacidad();
                IndiceOcupacionalModalidadPartida indOcupModParVar = new IndiceOcupacionalModalidadPartida();
                IndiceOcupacional inOcupVar = new IndiceOcupacional();
                ManualPuesto manPuestoVar = new ManualPuesto();

                // INICIALIZACIÓN DE VARIABLES NULAS

                var datoConadis = "";
                var datoPorcentaje = "";
                var datoCargoTrabajo = "";
                var datoPuestoTrabajo = "";
                var datoEdad = "";
                bool datoCondicionEspecial = false;
                int datoNumHijos = 0;
                var datoSedeTrabajo = "";
                var datoPersonaEmergencia = "";
                var datoPersonaEmergenciaContacto = "";


                var nombreCampoHijos = "Hijo/a";


                if (personaEstudioVar != null && personaEstudioVar.IdPersonaEstudio > 0)
                {
                    tituloVar = db.Titulo.Where(x => x.IdTitulo == personaEstudioVar.IdTitulo).FirstOrDefault();
                }

                if (tituloVar != null && tituloVar.IdEstudio > 0)
                {
                    estudioVar = db.Estudio.Where(x => x.IdEstudio == tituloVar.IdEstudio).FirstOrDefault();
                }


                if (empleadoVar != null && empleadoVar.IdEmpleado > 0)
                {
                    contactoEmergenciaVar = db.EmpleadoContactoEmergencia
                        .Where(x => x.IdEmpleado == empleadoVar.IdEmpleado)
                        .FirstOrDefault();

                    dependenciaVar = db.Dependencia
                        .Where(x => x.IdDependencia == empleadoVar.IdDependencia)
                        .FirstOrDefault();

                    empleadoFamiliarVar = db.EmpleadoFamiliar
                        .Where(x => x.IdEmpleado == empleadoVar.IdEmpleado)
                        .FirstOrDefault();

                    indOcupModParVar = db.IndiceOcupacionalModalidadPartida
                        .Where(x => x.IdEmpleado == empleadoVar.IdEmpleado)
                        .FirstOrDefault();


                    var listaHijos = db.EmpleadoFamiliar
                        .Where(x => x.IdEmpleado == empleadoVar.IdEmpleado)
                        .ToList();

                    datoNumHijos = listaHijos.Count();
                }

                if (indOcupModParVar != null && indOcupModParVar.IdEmpleado > 0)
                {
                    inOcupVar = db.IndiceOcupacional.Where(x => x.IdIndiceOcupacional == indOcupModParVar.IdIndiceOcupacional).FirstOrDefault();
                }

                if (inOcupVar != null && inOcupVar.IdIndiceOcupacional > 0)
                {
                    manPuestoVar = db.ManualPuesto.Where(x => x.IdManualPuesto == inOcupVar.IdManualPuesto).FirstOrDefault();
                }


                if (contactoEmergenciaVar != null && contactoEmergenciaVar.IdEmpleadoContactoEmergencia > 0)
                {
                    personaEmergenciaVar = db.Persona.Where(x => x.IdPersona == contactoEmergenciaVar.IdPersona).FirstOrDefault();
                }


                if (dependenciaVar != null && dependenciaVar.IdDependencia > 0)
                {
                    sucursalVar = db.Sucursal.Where(x => x.IdSucursal == dependenciaVar.IdSucursal).FirstOrDefault();
                }


                if (empleadoFamiliarVar != null && empleadoFamiliarVar.IdEmpleadoFamiliar > 0 && contactoEmergenciaVar != null && contactoEmergenciaVar.IdEmpleadoContactoEmergencia > 0)
                {
                    parentescoVar = db.Parentesco.Where(x => x.IdParentesco == contactoEmergenciaVar.IdParentesco).FirstOrDefault();
                }

                if (personaDiscapacidadVar != null && personaDiscapacidadVar.IdPersonaDiscapacidad > 0)
                {
                    discapacidadVar = db.TipoDiscapacidad.Where(x => x.IdTipoDiscapacidad == personaDiscapacidadVar.IdTipoDiscapacidad).FirstOrDefault();
                    datoCondicionEspecial = true;
                }




                // Validación obtencion de dato nulo


                if (discapacidadVar != null && discapacidadVar.IdTipoDiscapacidad > 0)
                {
                    datoConadis = personaDiscapacidadVar.NumeroCarnet;
                    datoPorcentaje = personaDiscapacidadVar.Porciento + " %";
                }

                if (sucursalVar != null && sucursalVar.IdSucursal > 0)
                {
                    datoSedeTrabajo = sucursalVar.Nombre;
                }

                if (personaEmergenciaVar != null && personaEmergenciaVar.IdPersona > 0)
                {
                    datoPersonaEmergencia = personaEmergenciaVar.Nombres + " " + personaEmergenciaVar.Apellidos;
                    datoPersonaEmergenciaContacto = personaEmergenciaVar.TelefonoCasa + "  " + personaEmergenciaVar.TelefonoPrivado;
                }

                if (manPuestoVar != null && manPuestoVar.IdManualPuesto > 0)
                {
                    datoPuestoTrabajo = manPuestoVar.Descripcion;
                    datoCargoTrabajo = manPuestoVar.Nombre;
                }
                */

                /*

                DateTime year = (DateTime)personaVar.FechaNacimiento;


                int yActual = DateTime.Now.Year;
                int yNacimiento = (int)(year.Year);

                datoEdad = (yActual - yNacimiento) + "";
                */

                DistributivosController crtlDistributivos = new DistributivosController(db);
                var distributivos = await crtlDistributivos.ObtenerDistributivoFormal();
                var registroDistributivo = distributivos.Where(w => w.Empleado.Persona.IdPersona == personaVar.IdPersona)
                    .FirstOrDefault();

                DatosBasicosPersonaViewModel dbvm = new DatosBasicosPersonaViewModel();
                var varPersonaEstudio = await db.PersonaEstudio
                    .Include(i => i.Titulo)
                    .Include(i => i.Titulo.Estudio)
                    .Where(w=>w.IdPersona == personaVar.IdPersona)
                    .FirstOrDefaultAsync()
                ;

                var tipoDiscapacidad = await db.PersonaDiscapacidad
                    .Where(w => w.IdPersona == personaVar.IdPersona)
                    .FirstOrDefaultAsync()
                ;
                

                var varNivelEducativo = "";
                var varProfesion = "";
                var varTipoDiscapacidad = "";
                var varCarnet = "";
                var varPorcentaje = "";

                var varContactoEmergenciaNombre = "";
                var varContactoEmergenciaTelefono = "";
                var varParentesco = "";

                if (varPersonaEstudio != null) {

                    if (varPersonaEstudio.Titulo != null) {

                        if (varPersonaEstudio.Titulo.Estudio != null)
                        {

                            varNivelEducativo = varPersonaEstudio.Titulo.Estudio.Nombre;

                        }

                        varProfesion = varPersonaEstudio.Titulo.Nombre;

                    }

                }

                if (tipoDiscapacidad != null) {

                    if (tipoDiscapacidad.TipoDiscapacidad != null) {
                        varTipoDiscapacidad = tipoDiscapacidad.TipoDiscapacidad.Nombre;
                    }

                    varCarnet = tipoDiscapacidad.NumeroCarnet;
                    varPorcentaje = tipoDiscapacidad.Porciento.ToString();
                }

                if (registroDistributivo != null) {

                    var contactoEmergencia = await db.EmpleadoContactoEmergencia
                        .Include(i => i.Persona)
                        .Include(i => i.Parentesco)
                        .Where(w => w.IdEmpleado == registroDistributivo.IdEmpleado)
                        .FirstOrDefaultAsync();

                    if (contactoEmergencia != null) {

                        varContactoEmergenciaTelefono = contactoEmergencia.Persona.TelefonoCasa + " " + contactoEmergencia.Persona.TelefonoPrivado;

                        varContactoEmergenciaNombre = contactoEmergencia.Persona.Nombres + " " + contactoEmergencia.Persona.Apellidos ;

                        varParentesco = contactoEmergencia.Parentesco.Nombre;
                    }
                }


                if (
                    registroDistributivo == null || 
                    (registroDistributivo != null && registroDistributivo.Empleado.Activo==false ) 
                ) 
                {

                    dbvm = await db.Persona
                        .Where(w => w.IdPersona == personaVar.IdPersona)
                        .Select(s => new DatosBasicosPersonaViewModel
                        {

                            IdPersona = s.IdPersona,
                            NombresApellidos = s.Nombres + " " + s.Apellidos,
                            Identificacion = s.Identificacion,

                            LugarNacimiento = "",

                            FechaNacimiento = (s.FechaNacimiento != null)
                            ? ((DateTime)s.FechaNacimiento).Day + "/" + ((DateTime)s.FechaNacimiento).Month + "/" + ((DateTime)s.FechaNacimiento).Year
                            : ""
                            ,


                            DireccionDomiciliaria = s.CallePrincipal + " " + s.CalleSecundaria + " " + s.Numero,
                            Telefono = s.TelefonoPrivado + "   " + s.TelefonoCasa,
                            Edad = (s.FechaNacimiento) != null
                            ? (DateTime.Now.Year - ((DateTime)s.FechaNacimiento).Year).ToString()
                            : ""
                            ,


                            Genero = (s.Sexo != null)
                            ? s.Sexo.Nombre
                            : ""
                            ,

                            NivelEducativo = varNivelEducativo,

                            EstadoCivil = (s.EstadoCivil != null)
                            ? s.EstadoCivil.Nombre
                            : ""
                            ,

                            Profesion = varProfesion,

                            NumeroHijos = 0,
                            Etnia = (s.Etnia != null)
                            ? s.Etnia.Nombre
                            : ""
                            ,


                            CondicionEspecial = (s.PersonaDiscapacidad.Where(w => w.IdPersona == s.IdPersona).FirstOrDefault() != null)
                            ? true
                            : false
                            ,

                            TipoDiscapacidad = varTipoDiscapacidad,

                            Conadis = varCarnet,

                            Porcentaje = varPorcentaje,

                            NombreCargoTrabajo = "",

                            DescripcionPuestoTrabajo = "",

                            SedeTrabajo = "",

                            ContactoEmergencias = varContactoEmergenciaNombre,

                            Parentesco = varParentesco,
                            ParienteTelefono = varContactoEmergenciaTelefono,
                            FechaIngreso = null
                            
                        })
                        .FirstOrDefaultAsync();


                }
                else
                {

                    dbvm = await db.Empleado
                        .Where(w => w.IdPersona == personaVar.IdPersona)
                        .Select(s => new DatosBasicosPersonaViewModel
                        {

                            IdPersona = s.Persona.IdPersona,
                            NombresApellidos = s.Persona.Nombres + " " + s.Persona.Apellidos,
                            Identificacion = s.Persona.Identificacion,
                            
                            LugarNacimiento = (s.CiudadNacimiento!= null)
                            ?s.CiudadNacimiento.Nombre
                            :""
                            ,

                            FechaNacimiento = (s.Persona.FechaNacimiento != null)
                            ? ((DateTime)s.Persona.FechaNacimiento).Day + "/" + ((DateTime)s.Persona.FechaNacimiento).Month + "/" + ((DateTime)s.Persona.FechaNacimiento).Year
                            : ""
                            ,

                            
                            DireccionDomiciliaria = s.Persona.CallePrincipal + " " + s.Persona.CalleSecundaria + " " + s.Persona.Numero,

                            Telefono = s.Persona.TelefonoPrivado + "   " + s.Persona.TelefonoCasa,

                            Edad = (s.Persona.FechaNacimiento) != null
                            ? (DateTime.Now.Year - ((DateTime)s.Persona.FechaNacimiento).Year).ToString()
                            : ""
                            ,

                            
                            Genero = (s.Persona.Sexo != null)
                            ? s.Persona.Sexo.Nombre
                            : ""
                            ,

                            NivelEducativo = varNivelEducativo,

                            EstadoCivil = (s.Persona.EstadoCivil != null)
                            ? s.Persona.EstadoCivil.Nombre
                            : ""
                            ,

                            Profesion = varProfesion,
                            
                            NumeroHijos = 0,

                            Etnia = (s.Persona.Etnia != null)
                            ? s.Persona.Etnia.Nombre
                            : ""
                            ,

                            
                            CondicionEspecial = (s.Persona.PersonaDiscapacidad.Where(w => w.IdPersona == s.IdPersona).FirstOrDefault() != null)
                            ? true
                            : false
                            ,

                            TipoDiscapacidad = varTipoDiscapacidad,


                            
                            Conadis = varCarnet,
                            
                            
                            Porcentaje = varPorcentaje,

                            NombreCargoTrabajo = registroDistributivo.IndiceOcupacionalModalidadPartida.IndiceOcupacional.DenominacionPuesto,

                            DescripcionPuestoTrabajo = "",
                            
                            SedeTrabajo =
                                registroDistributivo.IndiceOcupacionalModalidadPartida.Dependencia.Sucursal.Nombre,
                            
                            ContactoEmergencias = varContactoEmergenciaNombre,
                            

                            
                            Parentesco = varParentesco,
                            ParienteTelefono = varContactoEmergenciaTelefono,

                            FechaIngreso = s.FechaIngreso
                            
                        })
                        .FirstOrDefaultAsync();
                }



                




                var ListaFichasMedicas = await db.FichaMedica
                    .Where(x => x.IdPersona == personaVar.IdPersona)
                    .OrderByDescending(x => x.FechaFichaMedica)
                    .ToListAsync(); // devuelve una lista


                var fichaMedicaViewModel = new FichaMedicaViewModel { DatosBasicosPersonaViewModel = dbvm, FichasMedicas = ListaFichasMedicas, ListaPersonas = ListaPersonasFichaMedicaView() };



                return new Response { IsSuccess = true, Resultado = fichaMedicaViewModel };
            }
            catch(Exception ex)
            {

                var fichaMedicaViewModel = new FichaMedicaViewModel();

                return new Response { IsSuccess = false, Resultado = fichaMedicaViewModel, Message = Mensaje.Error };
            }

        }
        


        // POST: api/FichasMedicas
        [HttpPost]
        [Route("VerUltimaFichaMedica")]
        public async Task<Response> GetUltimaFichaMedica([FromBody] FichaMedica fichaMedica)
        {

            FichaMedica UltimaFicha = new FichaMedica();

            try
            {
                //Estado 0 = Ficha recién creada

                UltimaFicha = await db.FichaMedica.Where(
                    x => x.Estado == 0 
                    && x.IdPersona == fichaMedica.IdPersona
                ).FirstOrDefaultAsync();

                var persona = await db.Persona
                    .Include(i=>i.Sexo)
                    .Include(i=>i.TipoSangre)
                    .Where(w => w.IdPersona == fichaMedica.IdPersona)
                    .FirstOrDefaultAsync();
                
                
                if (UltimaFicha != null)
                {
                    UltimaFicha.Sexo = persona.Sexo.Nombre;
                    UltimaFicha.TipoSangre = persona.TipoSangre.Nombre;

                    return new Response { IsSuccess = true, Resultado = UltimaFicha };
                }

                return new Response { IsSuccess = false, Resultado = UltimaFicha, Message = "Sin fichas médicas en edición" };

            }
            catch (Exception ex)
            {

                return new Response { IsSuccess = false, Resultado = UltimaFicha, Message = "Hubo una excepción !!!" };

            }
            
        }




        // POST: api/FichasMedicas
        [HttpPost]
        [Route("VerIdPersonaPorFicha")]
        public async Task<Response> GetIdPersonaPorFicha([FromBody] int idFicha)
        {

            FichaMedica UltimaFicha = new FichaMedica();

            try
            {
                //Estado 0 = Ficha recién creada

                UltimaFicha = db.FichaMedica.Where(
                    x => x.Estado == 0
                    && x.IdFichaMedica == idFicha
                ).FirstOrDefault();



                if (UltimaFicha != null)
                {
                    return new Response { IsSuccess = true, Resultado = UltimaFicha };
                }

                return new Response { IsSuccess = false, Resultado = UltimaFicha, Message = "No existe información de esa ficha médica" };

            }
            catch (Exception ex)
            {

                return new Response { IsSuccess = false, Resultado = UltimaFicha, Message = "Hubo una excepción !!!" };

            }

        }



        // GET: api/FichasMedicas
        [HttpGet]
        [Route("ListarFichasMedicas")]
        public async Task<List<FichaMedica>> GetFichaMedica()
        {

            try
            {

                return await db.FichaMedica.OrderBy(x => x.IdFichaMedica).ToListAsync();

            }
            catch (Exception ex)
            {
                
                return new List<FichaMedica>();
            }
        }



        // GET: api/FichasMedicas/5
        [HttpGet("{id}")]
        public async Task<Response> GetFichaMedica([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var FichaMedica = await db.FichaMedica.SingleOrDefaultAsync(m => m.IdFichaMedica == id);


                if (FichaMedica == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = FichaMedica,
                };
            }
            catch (Exception ex)
            {
                
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }

        }

        // PUT: api/FichasMedicas/5
        [HttpPut("{id}")]
        public async Task<Response> PutFichaMedica([FromRoute] int id, [FromBody] FichaMedica FichaMedica)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }

                var Respuesta1 = new FichaMedica();
                
                
                Respuesta1 = db.FichaMedica.Where(x => x.IdPersona == FichaMedica.IdPersona && x.IdFichaMedica == FichaMedica.IdFichaMedica).FirstOrDefault(); // devuelve una lista
                


                if (Respuesta1 == null )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                var Actualizar = await db.FichaMedica.Where(x => x.IdFichaMedica == id).FirstOrDefaultAsync();
                if (Actualizar != null)
                {
                    try
                    {

                        Actualizar.IdPersona = FichaMedica.IdPersona;
                        Actualizar.FechaFichaMedica = FichaMedica.FechaFichaMedica;
                        Actualizar.AntecedenteMedico = FichaMedica.AntecedenteMedico;
                        Actualizar.AntecedenteQuirurgico = FichaMedica.AntecedenteQuirurgico;
                        Actualizar.Alergias = FichaMedica.Alergias;
                        Actualizar.UsoMedicinaDiaria = FichaMedica.UsoMedicinaDiaria;
                        Actualizar.Vacunas = FichaMedica.Vacunas;
                        Actualizar.FechaUltimaDosis = FichaMedica.FechaUltimaDosis;
                        Actualizar.PrimeraMenstruacion = FichaMedica.PrimeraMenstruacion;
                        Actualizar.UltimaMenstruacion = FichaMedica.UltimaMenstruacion;
                        Actualizar.CicloMenstrual = FichaMedica.CicloMenstrual;
                        Actualizar.Gestas = FichaMedica.Gestas;
                        Actualizar.Partos = FichaMedica.Partos;
                        Actualizar.Cesarias = FichaMedica.Cesarias;
                        Actualizar.Abortos = FichaMedica.Abortos;
                        Actualizar.HijosVivos = FichaMedica.HijosVivos;
                        Actualizar.UltimoPapTest = FichaMedica.UltimoPapTest;
                        Actualizar.UltimaMamografia = FichaMedica.UltimaMamografia;
                        Actualizar.Anticoncepcion = FichaMedica.Anticoncepcion;
                        Actualizar.Cigarrillo = FichaMedica.Cigarrillo;
                        Actualizar.FrecuenciaCigarrillo = FichaMedica.FrecuenciaCigarrillo;
                        Actualizar.CigarrilloDesde = FichaMedica.CigarrilloDesde;
                        Actualizar.CigarrilloHasta = FichaMedica.CigarrilloHasta;
                        Actualizar.Licor = FichaMedica.Licor;
                        Actualizar.LicorFrecuencia = FichaMedica.LicorFrecuencia;
                        Actualizar.LicorDesde = FichaMedica.LicorDesde;
                        Actualizar.LicorHasta = FichaMedica.LicorHasta;
                        Actualizar.Drogas = FichaMedica.Drogas;
                        Actualizar.FrecuenciaDrogas = FichaMedica.FrecuenciaDrogas;
                        Actualizar.DrogasDesde = FichaMedica.DrogasDesde;
                        Actualizar.DrogasHasta = FichaMedica.DrogasHasta;
                        Actualizar.Ejercicios = FichaMedica.Ejercicios;
                        Actualizar.EjerciciosFrecuencia = FichaMedica.EjerciciosFrecuencia;
                        Actualizar.EjerciciosTipo = FichaMedica.EjerciciosTipo;
                        Actualizar.TensionArterial = FichaMedica.TensionArterial;
                        Actualizar.FrecuenciaCardiaca = FichaMedica.FrecuenciaCardiaca;
                        Actualizar.FrecuenciaRespiratoria = FichaMedica.FrecuenciaRespiratoria;
                        Actualizar.Talla = FichaMedica.Talla;
                        Actualizar.Peso = FichaMedica.Peso;
                        Actualizar.LateralidadDominante = FichaMedica.LateralidadDominante;
                        Actualizar.Interpretacion = FichaMedica.Interpretacion;
                        Actualizar.Cabeza = FichaMedica.Cabeza;
                        Actualizar.Ojos = FichaMedica.Ojos;
                        Actualizar.Oidos = FichaMedica.Oidos;
                        Actualizar.Nariz = FichaMedica.Nariz;
                        Actualizar.Boca = FichaMedica.Boca;
                        Actualizar.FaringeAmigdalas = FichaMedica.FaringeAmigdalas;
                        Actualizar.Cuello = FichaMedica.Cuello;
                        Actualizar.Corazon = FichaMedica.Corazon;
                        Actualizar.Pulmones = FichaMedica.Pulmones;
                        Actualizar.Abdomen = FichaMedica.Abdomen;
                        Actualizar.Hernias = FichaMedica.Hernias;
                        Actualizar.Genitales = FichaMedica.Genitales;
                        Actualizar.ExtremidadesSuperiores = FichaMedica.ExtremidadesSuperiores;
                        Actualizar.ExtremidadesInferiores = FichaMedica.ExtremidadesInferiores;
                        Actualizar.Varices = FichaMedica.Varices;
                        Actualizar.SistemaNerviosoCentral = FichaMedica.SistemaNerviosoCentral;
                        Actualizar.Piel = FichaMedica.Piel;
                        Actualizar.Diagnostico = FichaMedica.Diagnostico;
                        Actualizar.SospechaEnfermedadLaboral = FichaMedica.SospechaEnfermedadLaboral;
                        Actualizar.DetalleEnfermedad = FichaMedica.DetalleEnfermedad;
                        Actualizar.AptoCargo = FichaMedica.AptoCargo;
                        Actualizar.Recomendaciones = FichaMedica.Recomendaciones;
                        Actualizar.AccidenteTrabajo = FichaMedica.AccidenteTrabajo;
                        Actualizar.FechaAccidente = FichaMedica.FechaAccidente;
                        Actualizar.EmpresaAccidente = FichaMedica.EmpresaAccidente;
                        Actualizar.EnfermedadProfesional = FichaMedica.EnfermedadProfesional;
                        Actualizar.FechaDiagnostico = FichaMedica.FechaDiagnostico;
                        Actualizar.EmpresaEnfermedad = FichaMedica.EmpresaEnfermedad;
                        Actualizar.DetalleAccidenteEnfermedadOcupacional = FichaMedica.DetalleAccidenteEnfermedadOcupacional;
                        Actualizar.HabitosObservaciones = FichaMedica.HabitosObservaciones;
                        Actualizar.CabezaHallazgos = FichaMedica.CabezaHallazgos;
                        Actualizar.OjosHallazgos = FichaMedica.OjosHallazgos;
                        Actualizar.OidosHallazgos = FichaMedica.OidosHallazgos;
                        Actualizar.NarizHallazgos = FichaMedica.NarizHallazgos;
                        Actualizar.BocaHallazgos = FichaMedica.BocaHallazgos;
                        Actualizar.FaringeAmigdalasHallazgos = FichaMedica.FaringeAmigdalasHallazgos;
                        Actualizar.CuelloHallazgos = FichaMedica.CuelloHallazgos;
                        Actualizar.CorazonHallazgos = FichaMedica.CorazonHallazgos;
                        Actualizar.PulmonesHallazgos = FichaMedica.PulmonesHallazgos;
                        Actualizar.AbdomenHallazgos = FichaMedica.AbdomenHallazgos;
                        Actualizar.HerniasHallazgos = FichaMedica.HerniasHallazgos;
                        Actualizar.GenitalesHallazgos = FichaMedica.GenitalesHallazgos;
                        Actualizar.ExtremidadesSuperioresHallazgos = FichaMedica.ExtremidadesSuperioresHallazgos;
                        Actualizar.ExtremidadesInferioresHallazgos = FichaMedica.ExtremidadesInferioresHallazgos;
                        Actualizar.VaricesHallazgos = FichaMedica.VaricesHallazgos;
                        Actualizar.SistemaNerviosoHallazgos = FichaMedica.SistemaNerviosoHallazgos;
                        Actualizar.PielHallazgos = FichaMedica.PielHallazgos;
                        Actualizar.Estado = FichaMedica.Estado;

                        db.FichaMedica.Update(Actualizar);

                        await db.SaveChangesAsync();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                        };

                    }
                    catch (Exception ex)
                    {
                        
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.Error,
                        };
                    }
                }


                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro,
                };


            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }

        
        //// PUT: api/FichasMedicas/5
        //[HttpPut("{id}")]
        //public async Task<Response> PutEstado([FromRoute] int id, [FromBody] FichaMedica FichaMedica)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ModeloInvalido
        //            };
        //        }

        //        var Respuesta1 = new FichaMedica();


        //        Respuesta1 = db.FichaMedica.Where(x => x.IdFichaMedica == FichaMedica.IdFichaMedica).FirstOrDefault(); 



        //        if (Respuesta1 == null)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.RegistroNoEncontrado,
        //            };
        //        }

        //        var Actualizar = await db.FichaMedica.Where(x => x.IdFichaMedica == id).FirstOrDefaultAsync();
        //        if (Actualizar != null)
        //        {
        //            try
        //            {
                        
        //                Actualizar.Estado = FichaMedica.Estado;

        //                db.FichaMedica.Update(Actualizar);

        //                await db.SaveChangesAsync();

        //                return new Response
        //                {
        //                    IsSuccess = true,
        //                    Message = Mensaje.Satisfactorio,
        //                };

        //            }
        //            catch (Exception ex)
        //            {

        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = Mensaje.Error,
        //                };
        //            }
        //        }


        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.ExisteRegistro,
        //        };


        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.Excepcion
        //        };
        //    }

        //}

        


        // POST: api/FichasMedicas
        [HttpPost]
        [Route("InsertarFichasMedicas")]
        public async Task<Response> PostFichaMedica([FromBody] FichaMedica FichaMedica)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                //var respuesta = Existe(FichaMedica);
                //if (!respuesta.IsSuccess)
                //{
                    db.FichaMedica.Add(FichaMedica);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
                //}
                
                //return new Response
                //{
                //    IsSuccess = false,
                //    Message = Mensaje.ExisteRegistro
                //};
                

            }
            catch (Exception ex)
            {
                
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion + ", Asegurese de haberllenado todos los campos obligatorios",
                };
            }
        }

        // DELETE: api/FichasMedicas/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteFichaMedica([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var respuesta = await db.FichaMedica.SingleOrDefaultAsync(m => m.IdFichaMedica == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.FichaMedica.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }

        }




        private Response Existe(FichaMedica FichaMedica)
        {

            var Respuesta = new FichaMedica();

            if (FichaMedica.IdFichaMedica > 0)
            {
                Respuesta = db.FichaMedica.Where(x => x.IdPersona == FichaMedica.IdPersona && x.IdFichaMedica == FichaMedica.IdFichaMedica).FirstOrDefault(); // devuelve una lista
            }



            
            //var Respuesta = db.FichaMedica.Where(

            //            p => p.IdPersona == FichaMedica.IdPersona
            //            && p.FechaFichaMedica == FichaMedica.FechaFichaMedica
                        
                        
            //            && p.AntecedenteMedico.ToUpper().TrimEnd().TrimStart() == FichaMedica.AntecedenteMedico.ToUpper().TrimEnd().TrimStart()
                        
            //            && p.AntecedenteQuirurgico.ToUpper().TrimEnd().TrimStart() == FichaMedica.AntecedenteQuirurgico.ToUpper().TrimEnd().TrimStart()
            //            
            //            && p.Alergias.ToUpper().TrimEnd().TrimStart() == FichaMedica.Alergias.ToUpper().TrimEnd().TrimStart()
            //            && p.UsoMedicinaDiaria.ToUpper().TrimEnd().TrimStart() == FichaMedica.UsoMedicinaDiaria.ToUpper().TrimEnd().TrimStart()
            //            && p.Vacunas.ToUpper().TrimEnd().TrimStart() == FichaMedica.Vacunas.ToUpper().TrimEnd().TrimStart()
            //            && p.AntecedenteMedico.ToUpper().TrimEnd().TrimStart() == FichaMedica.AntecedenteMedico.ToUpper().TrimEnd().TrimStart()
            //            && p.FechaUltimaDosis == FichaMedica.FechaUltimaDosis

            //            
            //            && p.PrimeraMenstruacion == FichaMedica.PrimeraMenstruacion
            //            && p.UltimaMenstruacion == FichaMedica.UltimaMenstruacion
            //            && p.CicloMenstrual.ToUpper().TrimEnd().TrimStart() == FichaMedica.CicloMenstrual.ToUpper().TrimEnd().TrimStart()
            //            && p.Gestas == FichaMedica.Gestas
            //            && p.Partos == FichaMedica.Partos
            //            && p.Cesarias == FichaMedica.Cesarias
            //            && p.Abortos == FichaMedica.Abortos
            //            && p.HijosVivos == FichaMedica.HijosVivos
            //            && p.UltimoPapTest == FichaMedica.UltimoPapTest
            //            && p.UltimaMamografia == FichaMedica.UltimaMamografia
            //            && p.Anticoncepcion == FichaMedica.Anticoncepcion

            //            

            //            && p.Cigarrillo == p.Cigarrillo
            //            && p.FrecuenciaCigarrillo.ToUpper().TrimEnd().TrimStart() == FichaMedica.FrecuenciaCigarrillo.ToUpper().TrimEnd().TrimStart()
            //            && p.CigarrilloDesde.ToUpper().TrimEnd().TrimStart() == FichaMedica.CigarrilloDesde.ToUpper().TrimEnd().TrimStart()
            //            && p.CigarrilloHasta.ToUpper().TrimEnd().TrimStart() == FichaMedica.CigarrilloHasta.ToUpper().TrimEnd().TrimStart()

            //            && p.Licor == p.Licor
            //            && p.LicorFrecuencia.ToUpper().TrimEnd().TrimStart() == FichaMedica.LicorFrecuencia.ToUpper().TrimEnd().TrimStart()
            //            && p.LicorDesde.ToUpper().TrimEnd().TrimStart() == FichaMedica.LicorDesde.ToUpper().TrimEnd().TrimStart()
            //            && p.LicorHasta.ToUpper().TrimEnd().TrimStart() == FichaMedica.LicorHasta.ToUpper().TrimEnd().TrimStart()

            //            && p.Drogas == p.Drogas
            //            && p.FrecuenciaDrogas.ToUpper().TrimEnd().TrimStart() == FichaMedica.FrecuenciaDrogas.ToUpper().TrimEnd().TrimStart()
            //            && p.DrogasDesde.ToUpper().TrimEnd().TrimStart() == FichaMedica.DrogasDesde.ToUpper().TrimEnd().TrimStart()
            //            && p.DrogasHasta.ToUpper().TrimEnd().TrimStart() == FichaMedica.DrogasHasta.ToUpper().TrimEnd().TrimStart()

            //            && p.Ejercicios == p.Ejercicios
            //            && p.EjerciciosFrecuencia.ToUpper().TrimEnd().TrimStart() == FichaMedica.EjerciciosFrecuencia.ToUpper().TrimEnd().TrimStart()
            //            && p.EjerciciosTipo.ToUpper().TrimEnd().TrimStart() == FichaMedica.EjerciciosTipo.ToUpper().TrimEnd().TrimStart()


                        
            //            && p.TensionArterial.ToUpper().TrimEnd().TrimStart() == FichaMedica.TensionArterial.ToUpper().TrimEnd().TrimStart()
            //            && p.FrecuenciaCardiaca.ToUpper().TrimEnd().TrimStart() == FichaMedica.FrecuenciaCardiaca.ToUpper().TrimEnd().TrimStart()
            //            && p.FrecuenciaRespiratoria.ToUpper().TrimEnd().TrimStart() == FichaMedica.FrecuenciaRespiratoria.ToUpper().TrimEnd().TrimStart()
            //            && p.Talla.ToUpper().TrimEnd().TrimStart() == FichaMedica.Talla.ToUpper().TrimEnd().TrimStart()
            //            && p.Peso.ToUpper().TrimEnd().TrimStart() == FichaMedica.Peso.ToUpper().TrimEnd().TrimStart()
            //            && p.LateralidadDominante == FichaMedica.LateralidadDominante
            //            && p.Interpretacion == FichaMedica.Interpretacion

                    
            //            && p.Cabeza == FichaMedica.Cabeza
            //            && p.Ojos == FichaMedica.Ojos
            //            && p.Oidos == FichaMedica.Oidos
            //            && p.Nariz == FichaMedica.Nariz
            //            && p.Boca == FichaMedica.Boca
            //            && p.FaringeAmigdalas == FichaMedica.FaringeAmigdalas
            //            && p.Cuello == FichaMedica.Cuello
            //            && p.Corazon == FichaMedica.Corazon
            //            && p.Pulmones == FichaMedica.Pulmones
            //            && p.Abdomen == FichaMedica.Abdomen
            //            && p.Hernias == FichaMedica.Hernias
            //            && p.Genitales == FichaMedica.Genitales
            //            && p.ExtremidadesSuperiores == FichaMedica.ExtremidadesSuperiores
            //            && p.ExtremidadesInferiores == FichaMedica.ExtremidadesInferiores
            //            && p.Varices == FichaMedica.Varices
            //            && p.SistemaNerviosoCentral == FichaMedica.SistemaNerviosoCentral
            //            && p.Piel == FichaMedica.Piel

                        
            //            && p.Diagnostico.ToUpper().TrimEnd().TrimStart() == FichaMedica.Diagnostico.ToUpper().TrimEnd().TrimStart()


            //            && p.SospechaEnfermedadLaboral == FichaMedica.SospechaEnfermedadLaboral
            //            && p.DetalleEnfermedad.ToUpper().TrimEnd().TrimStart() == FichaMedica.DetalleEnfermedad.ToUpper().TrimEnd().TrimStart()

            //            && p.AptoCargo == FichaMedica.AptoCargo
            //            && p.Recomendaciones.ToUpper().TrimEnd().TrimStart() == FichaMedica.Recomendaciones.ToUpper().TrimEnd().TrimStart()


            //            && p.AccidenteTrabajo == FichaMedica.AccidenteTrabajo
            //            && p.FechaAccidente == FichaMedica.FechaAccidente
            //            && p.EmpresaAccidente.ToUpper().TrimEnd().TrimStart() == FichaMedica.EmpresaAccidente.ToUpper().TrimEnd().TrimStart()


            //            && p.EnfermedadProfesional == FichaMedica.EnfermedadProfesional
            //            && p.FechaDiagnostico == FichaMedica.FechaDiagnostico
            //            && p.EmpresaEnfermedad.ToUpper().TrimEnd().TrimStart() == FichaMedica.EmpresaEnfermedad.ToUpper().TrimEnd().TrimStart()

            //            && p.DetalleAccidenteEnfermedadOcupacional.ToUpper().TrimEnd().TrimStart() == FichaMedica.DetalleAccidenteEnfermedadOcupacional.ToUpper().TrimEnd().TrimStart()


            //            && p.HabitosObservaciones.ToUpper().TrimEnd().TrimStart() == FichaMedica.HabitosObservaciones.ToUpper().TrimEnd().TrimStart()


            //            && p.CabezaHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.CabezaHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.OjosHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.OjosHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.OidosHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.OidosHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.NarizHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.NarizHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.BocaHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.BocaHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.FaringeAmigdalasHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.FaringeAmigdalasHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.CuelloHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.CuelloHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.CorazonHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.CorazonHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.PulmonesHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.PulmonesHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.AbdomenHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.AbdomenHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.HerniasHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.HerniasHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.GenitalesHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.GenitalesHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.ExtremidadesSuperioresHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.ExtremidadesSuperioresHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.ExtremidadesInferioresHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.ExtremidadesInferioresHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.VaricesHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.VaricesHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.SistemaNerviosoHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.SistemaNerviosoHallazgos.ToUpper().TrimEnd().TrimStart()
            //            && p.PielHallazgos.ToUpper().TrimEnd().TrimStart() == FichaMedica.PielHallazgos.ToUpper().TrimEnd().TrimStart()

    
            //        ).FirstOrDefault();

            

            if (Respuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = Respuesta,
            };
        }



        // GET: api/FichasMedicas
        [HttpGet]
        [Route("ListarPersonasFichaMedica")]
        public async Task<List<Persona>> GetListaPersonas()  //este metodo debe devolver
        {
            
            

            return ListaPersonasFichaMedicaView();

        }



        public List<Persona>ListaPersonasFichaMedicaView()
        {
            List<Persona> personasLista = new List<Persona>();
            

            FichaMedica ficha = new FichaMedica();
            

            List<FichaMedica> fichasOrdenas = db.FichaMedica.Include(x => x.Persona).OrderBy(x => x.IdPersona).ToList();
            List<FichaMedica> fichaSinRepeticiones = new List<FichaMedica>();

            List<int> ids = new List<int>();


            for (int i = 0; i < fichasOrdenas.Count(); i++)
            {
                ficha = fichasOrdenas.ElementAt(i);

                if (ids.Contains(ficha.IdPersona) == true)
                {

                }
                else
                {
                    fichaSinRepeticiones.Add(ficha); // aqui agrgar la identif, nombre de la persona
                    ids.Add(ficha.IdPersona);

                    personasLista.Add( new Persona {
                        IdPersona = fichasOrdenas.ElementAt(i).IdPersona ,
                        Nombres = fichasOrdenas.ElementAt(i).Persona.Nombres ,
                        Apellidos = fichasOrdenas.ElementAt(i).Persona.Apellidos ,
                        Identificacion = fichasOrdenas.ElementAt(i).Persona.Identificacion


                    } );
                }
            }

            
            //for (int i = 0; i < fichaSinRepeticiones.Count(); i++)
            //{
            //    ficha = fichaSinRepeticiones.ElementAt(i);

                
            //    personasLista.Add(new Persona { IdPersona = ficha.IdPersona });
            //}
            


            return personasLista;

        }



        // Post: api/AntecedentesLaborales
        [HttpPost]
        [Route("ObtenerFichaPorId")]
        public async Task<Response> ObtenerFichaPorId([FromBody] int idFicha)
        {

            Response response = new entidades.Utils.Response();

            try
            {
                var lista = await db.FichaMedica.Where(x => x.IdFichaMedica == idFicha).OrderBy(x => x.IdFichaMedica).FirstOrDefaultAsync();


                return new Response { IsSuccess = true, Resultado = lista };

            }
            catch (Exception ex)
            {

                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }


        }


        
    }
}