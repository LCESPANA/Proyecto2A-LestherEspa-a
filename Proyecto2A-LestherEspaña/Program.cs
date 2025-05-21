using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;


class Jugador
{
    private string coordenada;
    private char[,] flotaNaval;
    private char[,] matrizAtaque;
    private int puntosJugador;
    private string nickname;
    public string Coordenada
    {
        get { return coordenada; } set { coordenada = value; }
    }
    public char[,] FlotaNaval
    {
        get { return flotaNaval; } set { FlotaNaval = value; }
    }
    public char[,] MatrizAtaque
    {
        get { return matrizAtaque; } set { matrizAtaque = value; }
    }
    public int PuntosJugador
    {
        get { return puntosJugador; } set { puntosJugador = value; }
    }
    public string Nickname
    {
        get { return nickname; } set { nickname = value; }
    }
    public Jugador(char[,] FlotaNaval, char[,] MatrizAtaque, int PuntosJugador, string Nickname)
    {
        this.flotaNaval = FlotaNaval;
        this.MatrizAtaque = MatrizAtaque;
        this.puntosJugador = PuntosJugador;
        this.nickname = Nickname;
    }
    public static void GenerarFlota(char[,] flotaNaval)
    {
        for (int fila = 0; fila < 6; fila++)
        {
            for (int columna = 0; columna < 6; columna++)
            {

                flotaNaval[fila, columna] = '≈';
            }
        }

    }

    public static bool PuedeColocar(int fila, int columna, int tamañoBarco, bool vertical, char[,] flotaNaval)
    {
        if (vertical)
        {
            if ((fila + tamañoBarco) > 6)
            {
                return false;
            }
            for (int i = 0; i < tamañoBarco; i++)
            {
                if (flotaNaval[fila + i, columna] != '≈')
                {
                    return false;
                }
            }
        }
        else
        {
            if ((columna + tamañoBarco) > 6)
            {
                return false;
            }
            for (int i = 0; i < tamañoBarco; i++)
            {
                if (flotaNaval[fila, columna + i] != '≈')
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void ColocarBarco(int fila, int columna, int tamaño, bool vertical, char simbolo, char[,] flotaNaval)
    {
        for (int i = 0; i < tamaño; i++)
        {
            if (vertical)
            {
                flotaNaval[fila + i, columna] = simbolo;
            }
            else
            {
                flotaNaval[fila, columna + i] = simbolo;
            }
        }
    }

    public static void GenerarSubmarino(char[,] flotaNaval)
    {
        Random random = new Random();
        bool puesto = false;

        while (!puesto)
        {
            int fila = random.Next(0, 6);
            int columna = random.Next(0, 5);

            if (PuedeColocar(fila, columna, 2, false, flotaNaval))
            {

                ColocarBarco(fila, columna, 2, false, '■', flotaNaval);
                puesto = true;
            }
        }

    }

    public static void GenerarFragata(char[,] flotaNaval)
    {
        Random random = new Random();
        bool puesto = false;

        while (!puesto)
        {
            int fila = random.Next(0, 4);
            int columna = random.Next(0, 6);

            if (PuedeColocar(fila, columna, 3, true, flotaNaval))
            {

                ColocarBarco(fila, columna, 3, true, '■', flotaNaval);
                puesto = true;
            }

        }
    }

    public static void GenerarDestructor(char[,] flotaNaval)
    {
        Random random = new Random();
        bool puesto = false;

        while (!puesto)
        {
            bool vertical = random.Next(0, 2) == 0;

            int fila = vertical ? random.Next(0, 3) : random.Next(0, 6);
            int columna = vertical ? random.Next(0, 6) : random.Next(0, 3);
            if (PuedeColocar(fila, columna, 4, vertical, flotaNaval))
            {

                ColocarBarco(fila, columna, 4, vertical, '■', flotaNaval);
                puesto = true;
            }
        }

    }
    public static void GenerarColor(char[,] flotaNaval, int fila, int columna)
    {

        switch (flotaNaval[fila, columna])
        {
            case '■':
                Console.ForegroundColor = ConsoleColor.Green;

                break;
            case '≈':
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                break;
            case '#':
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            default:
                Console.ResetColor();
                break;
        }
    }
    public static void GenerarMatrizAleatoria(char[,] flotaNaval)
    {
        GenerarFlota(flotaNaval);
        GenerarSubmarino(flotaNaval);
        GenerarFragata(flotaNaval);
        GenerarDestructor(flotaNaval);

        Console.WriteLine($"     1   2   3   4   5   6 ");
        char[] letras = ['a', 'b', 'c', 'd', 'e', 'f'];

        for (int fila = 0; fila < 6; fila++)
        {

            Console.Write($"{letras[fila]}    ");
            for (int columna = 0; columna < 6; columna++)
            {
                GenerarColor(flotaNaval, fila, columna);
                Console.Write(flotaNaval[fila, columna] == ' ' ? "." : flotaNaval[fila, columna]);
                Console.Write($"   ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
    public static string VerificarString(string dato)
    {
        while (true)
        {
            if (string.IsNullOrEmpty(dato) || string.IsNullOrWhiteSpace(dato))
            {
                Console.WriteLine($"Vuelva a ingresar el dato, es invalido");
                dato = Console.ReadLine().ToLower().Trim();
            }
            else
            {
                return dato;

            }

        }
    }
    public static bool SeguirJugando(ref bool jugando, string nickname)
    {
        bool correcto = true;
        Console.WriteLine($"Que deseas hacer \n Seguir \t Rendirse");
        string opcion = Console.ReadLine().ToLower().Trim();
        opcion = VerificarString(opcion);
        while (correcto)
        {
            if (opcion == "seguir")
            {
                jugando = true;
                correcto = false;
            }
            else if (opcion == "rendirse")
            {
                Console.WriteLine($"que mal, haz perdido {nickname}");
                jugando = false;
                correcto = false;
            }

            else
            {
                Console.WriteLine($"Ingresa una opcion valida");
                opcion = Console.ReadLine().ToLower();
                correcto = true;
            }
        }
        return jugando;
    }

    public static int VerificarInt(int datoNumerico)
    {

        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out datoNumerico) && datoNumerico > 0)
            {
                Console.WriteLine($"Ingrese un dato correcto");
                Console.ReadLine();
            }
            else
            {
                return datoNumerico;
            }
        }
    }
    public static void GenerarMatrizAtaque(char[,] matrizAtaque)
    {
        for (int filas = 0; filas < 6; filas++)
        {
            for (int columnas = 0; columnas < 6; columnas++)
            {
                matrizAtaque[filas, columnas] = '~';
            }
        }
    }
    public static void DibujarTablero(string nickname, int puntosJugador, char[,] flotaNaval, char[,] matrizAtaque)
    {

        string opcion;
        bool Notrue = true;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Jugador {nickname} \t Puntos: {puntosJugador} ");
        Console.WriteLine($"\n");
        Console.WriteLine($"\t Flota Naval");
        Console.ResetColor();
        GenerarMatrizAleatoria(flotaNaval);
        Console.WriteLine($"Te parece la posicion de los barcos? \nSi   \nNo");
        opcion = Console.ReadLine().ToLower().Trim();

        opcion = VerificarString(opcion);
        while (Notrue)
        {
            if (opcion != "si" && opcion != "no")
            {
                Console.WriteLine($"Ingrese una opcion valida");
                opcion = Console.ReadLine().ToLower().Trim();
                VerificarString(opcion);


            }
            else
            {
                switch (opcion)
                {
                    case "no":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Jugador {nickname} \t Puntos: {puntosJugador} ");
                        Console.WriteLine($"\n");
                        Console.WriteLine($"\t Flota Naval");
                        Console.ResetColor();
                        GenerarMatrizAleatoria(flotaNaval);
                        Console.WriteLine($"Te parece la posicion de los barcos? \n Si \n No ");
                        opcion = Console.ReadLine().ToLower().Trim();
                        VerificarString(opcion);
                        break;
                    case "si":
                        Notrue = false;
                        continue;
                }
            }

        }
        GenerarMatrizAtaque(matrizAtaque);
        Console.ResetColor();
        Console.ResetColor();
        Console.WriteLine($"Presione enter");
        Console.ReadKey();
    }
    public static void Ataque(string nickname, int puntosJugador, char[,] matrizAtaque, char[,] flotaNaval, int turnos)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Jugador {nickname} \t Puntos: {puntosJugador} \t turnos: {turnos}");
        Console.WriteLine($"\n");
        Console.WriteLine($"Flota Naval   \t\t\t\tTablero de ataque");
        Console.ResetColor();
        Console.WriteLine($"     1    2    3    4    5    6 \t     1    2    3    4    5    6 ");
        char[] letras = ['a', 'b', 'c', 'd', 'e', 'f'];
        for (int fila = 0; fila < 6; fila++)
        {
            Console.Write($"{letras[fila]}    ");
            for (int columna = 0; columna < 6; columna++)
            {
                GenerarColor(flotaNaval, fila, columna);
                Console.Write($"{flotaNaval[fila, columna]}    ");
            }
            Console.Write($"\t");
            Console.ResetColor();
            Console.Write($"{letras[fila]}    ");
            for (int columna = 0; columna < 6; columna++)
            {
                Console.Write($"{matrizAtaque[fila, columna]}    ");
            }
            Console.Write($"\n");
            Console.ResetColor();
        }
    }
    public static bool Victoria(string nickname1, string nickname2, int puntosJ1, int puntosJ2, int turnos)
    {
        if (turnos < 15)
        {
            if (puntosJ1 == 9)
            {
                Console.WriteLine($"Felicidades {nickname1} haz ganado el duelo en contra de {nickname2}");
                return false;
            }
            if (puntosJ2 == 9)
            {
                Console.WriteLine($"Felicidades {nickname2} haz ganado el duelo en contra de {nickname1}");
                return false;
            }
        }
        else
        {
            if (puntosJ1 > puntosJ2)
            {
                Console.WriteLine($"Felicidades {nickname1} haz ganado el duelo en contra de {nickname2}");
                return false;
            }
            if (puntosJ2 > puntosJ1)
            {
                Console.WriteLine($"Felicidades {nickname2} haz ganado el duelo en contra de {nickname1}");
                return false;
            }
            else
            {
                Console.WriteLine($"Felicidades, han jugado los 15 turnos y ninguno ha logrado sacar ventaja, han empatado, esperamos y vuelvas a jugar");
                return false;
            }

        }
        return true;

    }


    public static void LeerCoordenadas(string coordenadas, ref int fila, ref int columna)
    {

        string[] Coordenadas = coordenadas.Split('-');
        char letra = Coordenadas[0][0];
        fila = char.ToLower(letra) - 'a';
        int numero = int.Parse(Coordenadas[1]);
        columna = numero - 1;
    }
    public static void VerificarCasilla(ref string coordenadas, char[,] matrizAtaque, char[,] flotaNaval)
    {
        string patron = @"^[a-f]-[1-6]$";
        int fila = 0;
        int columna = 0;
        while (true)
        {
            if (!Regex.IsMatch(coordenadas, patron))
            {
                Console.WriteLine($"'{coordenadas}' NO es una coordenada válida. ej (F-3)");
            }
            else
            {
                LeerCoordenadas(coordenadas, ref fila, ref columna);

                if (matrizAtaque[fila, columna] == '~')
                {

                    break;
                }
                else
                {
                    Console.WriteLine($"Al parecer ya haz atacado aqui, prueba con otra coordenada.");
                }
            }


            coordenadas = Console.ReadLine().ToLower().Trim();
        }
    }
    public static void Atacar(char[,] flotaNaval, char[,] matrizAtaque, string coordenadas, ref int puntosJ1)
    {
        int fila = 0;
        int columna = 0;
        LeerCoordenadas(coordenadas, ref fila, ref columna);

            if (flotaNaval[fila, columna] == '■')
            {
                matrizAtaque[fila, columna] = 'O';
            
                flotaNaval[fila, columna] = '#';
                puntosJ1++;
                
            }
            else
            {
                matrizAtaque[fila, columna] = 'X';
            }
        
         
    }
    public static void PantallaDeCarga()
    {
        
        Console.Clear();
        Console.Write($"Cargando");
        for (int i = 0; i < 2; i++)
        {
            Thread.Sleep(1000);
            Console.Write($".");
        }
        Console.Write($"\n");
        Console.WriteLine($"Presiona enter");
        Console.ReadKey();
        Console.Clear();
    }
    public static bool VolverAJugar(bool seguirJugando)
    {
        bool correcto = true;
        Console.WriteLine($"Quieres seguir jugando \n Si \tNo");
        string opcion = Console.ReadLine().ToLower().Trim();
        VerificarString(opcion);
        while (correcto)
            if (opcion != "si" && opcion != "no")
            {
                seguirJugando = true;
                correcto = false;
            }
            else if (opcion == "no")
            {
                Console.WriteLine($"que mal, te esperamos a la proxima");
                seguirJugando = false;
                correcto = false;
            }

            else if (opcion == "si")
            {
                seguirJugando = true;
                correcto = false;
            }
        return seguirJugando;

    }


}
class Principal
{   
    static void Main()
    {
        bool jugarDeNuevo = true;
        while (jugarDeNuevo)
        {
            Console.Clear();
            Console.WriteLine($"Ingrese Su nickname");
            string nickname1 = Console.ReadLine();
            nickname1 = Jugador.VerificarString(nickname1);
            int puntosj1 = 0;
            int puntosj2 = 0;
            int turnos = 0;
            bool jugando = true;

            char[,] flotaNaval1 = new char[6, 6];
            char[,] matrizAtaque1 = new char[6, 6];
            char[,] flotaNaval2 = new char[6, 6];
            char[,] matrizAtaque2 = new char[6, 6];

            Jugador jugador1 = new Jugador(flotaNaval1, matrizAtaque1, puntosj1, nickname1);

            Jugador.DibujarTablero(nickname1, puntosj1, flotaNaval1, matrizAtaque1);
            Jugador.PantallaDeCarga();

            Console.WriteLine($"Ingrese Su nickname");
            string nickname2 = Console.ReadLine();
            nickname2 = Jugador.VerificarString(nickname2);
            Jugador jugador2 = new Jugador(flotaNaval2, matrizAtaque2, puntosj2, nickname2);



            Jugador.DibujarTablero(nickname2, puntosj2, flotaNaval2, matrizAtaque2);
            Jugador.PantallaDeCarga();
            while (jugando)
            {
                Jugador.Ataque(nickname1, puntosj1, matrizAtaque1, flotaNaval1, turnos);
                if(!(jugando = Jugador.SeguirJugando(ref jugando, nickname2)))
                {
                    continue;
                }
                Console.WriteLine($"Ingrese la coordenada que quiere atacar Ej. (F-3)");
                string coordenadas1 = Console.ReadLine().ToLower().Trim();
                Jugador.VerificarCasilla(ref coordenadas1, matrizAtaque1, flotaNaval1);
                Jugador.Atacar(flotaNaval2, matrizAtaque1, coordenadas1, ref puntosj1);
                Jugador.PantallaDeCarga();
                Jugador.Ataque(nickname2, puntosj2, matrizAtaque2, flotaNaval2, turnos);
                if (!(jugando = Jugador.SeguirJugando(ref jugando, nickname2)))
                {
                    continue;
                }
                Console.WriteLine($"Ingrese la coordenada que quiere atacar Ej. (F-3)");
                string coordenadas2 = Console.ReadLine().ToLower().Trim();
                Jugador.VerificarCasilla(ref coordenadas2, matrizAtaque2, flotaNaval2);
                Jugador.Atacar(flotaNaval1, matrizAtaque2, coordenadas2,ref puntosj2);
                Jugador.PantallaDeCarga();
                turnos++;
                jugando = Jugador.Victoria(nickname1, nickname2, puntosj1, puntosj2, turnos);
            }
            jugarDeNuevo = Jugador.VolverAJugar(jugarDeNuevo);
        }
    }
}
