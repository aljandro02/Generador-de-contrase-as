using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generadorDeContraseñas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*la contraseña se va a generar entre 8-20 caracteres
             * va a contener letras minusculas
             * va a contener letras mayusculas
             * va a contener numeros
             * va a contener caracteres especiales
             * */
            string nombreUsuario, opcion, contraseña;
            (bool contraseñaValida, string mensajeError) verificarContraseña;
            Console.WriteLine("\t\t***Generador de Contraseñas***\n\n");
            Console.WriteLine("Ingrese un nombre de usuario");
            nombreUsuario = Console.ReadLine();
            Console.WriteLine("Desea que le generemos una contraseña? (si/no)");
            opcion = Console.ReadLine();
            opcion = opcion.ToLower();
            switch (opcion)
            {
                case "si":
                    contraseña contraseña1 = new contraseña();
                   contraseña = contraseña1.GenerarContraseña();
                    Console.WriteLine("esta es tu contraseña genereda:" + contraseña);
                    Console.WriteLine("\nPresione una tecla para terminar con tu registro");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine($"\nTus datos de acceso son los siguientes:\n\tusuario: " + nombreUsuario +
                    "\n\tcontraseña: "+ contraseña);
                    break;
                case "no":
                        Console.WriteLine("\nIngrese una contraseña segura (la contraseña debe contener entre 8-20 caracteres, debe incluir: un numero, una mayuscula, una minuscula y uno de los siguientes caracteres especiales: $%#&!? )");
                    contraseña = Console.ReadLine();
                    contraseña contraseña2 = new contraseña();
                    verificarContraseña = contraseña2.ComprobarContraseña(contraseña);

                    if(verificarContraseña.contraseñaValida)
                    {
                        Console.WriteLine("\nPresione una tecla para terminar con tu registro");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine($"\nTus datos de acceso son los siguientes:\n\tusuario: " + nombreUsuario +
                        "\n\tcontraseña: " + contraseña);
                    }
                    else
                    {
                        Console.WriteLine(verificarContraseña.mensajeError + "\nIngresa una contraseña valida");
                    }
                    break;
            }

        }
    }
    class contraseña
    {
        //variables
        string numeros = "0123456789";
        string letrasMin = "abcdefghijklmnopqrstuvwxyz";
        string letrasMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string caracterEspecial = "$%#&!?";
        //contadores
        int numContiene = 0, minContiene = 0, mayContiene = 0, espContiene = 0;

        public string GenerarContraseña()
        {
            //variables de la funcion 
            string contraseñaGenerada = "";
            Random random = new Random();
            int longContraseña = random.Next(8, 21);
            //particiones de los caracteres entre el tamaño de la contraseña
            double numTener = longContraseña * .15;
            double minTener = longContraseña * .35;
            double mayTener = longContraseña * .35;
            double espTener = longContraseña * .15;

            char caracterEscogido;
            //bucle para generar la contraseña aleatoria
            while (contraseñaGenerada.Length < longContraseña)
            {
                switch (random.Next(0,4))
                {
                    //caso de agregar los numeros aleatorios
                    case 0:
                        if (numContiene < numTener)
                        {
                            caracterEscogido = numeros[random.Next(numeros.Length)];
                            contraseñaGenerada += caracterEscogido;
                            numContiene++;
                        }
                        break;
                        //caso de agregar letras minusculas
                    case 1:
                        if (minContiene < minTener)
                        {
                            caracterEscogido = letrasMin[random.Next(letrasMin.Length)];
                            contraseñaGenerada += caracterEscogido;
                            minContiene++;
                        }
                        break;
                        //caso de agregar letras mayusculas
                    case 2:
                        if (mayContiene < mayTener)
                        {
                            caracterEscogido = letrasMay[random.Next(letrasMay.Length)];
                            contraseñaGenerada += caracterEscogido;
                            mayContiene++;
                        }
                        break;
                        //caso de agregar caracteres especiales
                    case 3:
                        if (espContiene < espTener)
                        {
                            caracterEscogido = caracterEspecial[random.Next(caracterEspecial.Length)];
                            contraseñaGenerada += caracterEscogido;
                            espContiene++;
                        }
                        break;
                }
            }
            return contraseñaGenerada;
        }

        public (bool,string) ComprobarContraseña(string contraseña)
        {
            bool contraseñaValida = false, hayNumero=false, hayMinuscula = false, hayMayuscula = false, hayEspecial = false;
            string mensajeError = "";

            if (contraseña.Length >= 8 && contraseña.Length <=20)
            {
                foreach (char elemento in numeros)
                {
                    if (contraseña.IndexOf(elemento) >= 0)
                    {
                        hayNumero = true;
                        break;
                    }else
                    {
                        hayNumero = false;
                        mensajeError ="la contraseña debe contener al menos un numero";
                    }
                }
                //verificamos que exista un numero en la contraseña
                if (hayNumero)
                {   //verificamos que contenga letras minusculas
                    foreach (char elemento in letrasMin)
                    {
                        if (contraseña.IndexOf(elemento) >= 0)
                        {
                            hayMinuscula = true;
                            break;
                        }else
                        {
                            hayMinuscula = false;
                            mensajeError = "La contraseña debe contener al menos una letra minuscula";
                        }
                    }
                if (hayMinuscula)
                    { //verificamos que exitan letras mayusculas
                        foreach (char elemento in letrasMay)
                        {
                            if (contraseña.IndexOf(elemento)>=0)
                            {
                                hayMayuscula = true;
                                break;
                            }
                            else
                            {
                                hayMayuscula = false;
                                mensajeError = "la contraseña debe contener al menos una letra mayuscula";
                            }
                        }
                    }
                if (hayMayuscula)
                    {  //vericamos que existan caracteres especiales
                        foreach (char elemento in caracterEspecial)
                        {
                            if (contraseña.IndexOf(elemento)>= 0)
                            {
                                hayEspecial = true;
                                break;
                            } else
                            {
                                hayEspecial = false;
                                mensajeError = "la contraseña debe contener al menos un caracter especial ($%#&!?)";
                            }
                        }
                    }
                } // verificamos que existan todos los caracteres correspondientes
                if (hayNumero && hayMinuscula && hayMayuscula && hayEspecial)
                {
                    contraseñaValida = true;
                } else
                {
                    contraseñaValida = false;
                }
            }
            else
            {
                mensajeError = "La contraseña debe contener entre 8-20 caracteres";
                contraseñaValida = false;
            }
            return (contraseñaValida, mensajeError); 
        }
    }
}
