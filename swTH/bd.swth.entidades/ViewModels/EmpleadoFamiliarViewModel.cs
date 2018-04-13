using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.webappth.entidades.ViewModels
{
    public class EmpleadoFamiliarViewModel : IValidatableObject
    {
        public int IdEmpleado { get; set; }
        public int IdEmpleadoFamiliar { get; set; }
        public int IdPersona { get; set; }

        public int IdTipoIdentificacion { get; set; }

        public string Identificacion { get; set; }

        public int IdParentesco { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int IdSexo { get; set; }

        public int IdGenero { get; set; }

        public int IdEstadoCivil { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string LugarTrabajo { get; set; }


        public string TelefonoPrivado { get; set; }

        public string Ocupacion { get; set; }

        public string CorreoPrivado { get; set; }

        public string TelefonoCasa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (IdTipoIdentificacion == 1)
            {
                var cad = Identificacion.ToString();
                var total = 0;
                var longitud = cad.Length;
                var longcheck = longitud - 1;

                if (cad != "" && longitud == 10)
                {
                    for (int i = 0; i < longcheck; i++)
                    {
                        if (i % 2 == 0)
                        {
                            var aux = int.Parse(cad.Substring(i, 1)) * 2;
                            if (aux > 9)
                            {
                                aux -= 9;
                            }
                            total += aux;

                        }
                        else
                        {
                            total += int.Parse(cad.Substring(i, 1)); // parseInt o concatenará en lugar de sumar
                        }
                    }

                    total = total % 10;
                    total = 10 - total;

                    if (int.Parse(cad.Substring(longitud - 1, 1)) != total)
                    {
                        yield return
                      new ValidationResult(errorMessage: "La cédula no es válida",
                                           memberNames: new[] { "Identificacion" });
                    }
                }
            }

        }
    }
}
