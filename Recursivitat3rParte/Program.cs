IEnumerable<string> result  = Sumands(10);

foreach (string s in result)
{
    Console.WriteLine(s);
}

static void Permutacions(String cadena)
{
    iPermutacions("", cadena);
}

static void iPermutacions(String partFixe, String partAPermutar)
{
    if(partAPermutar.Length == 1)
    {
        Console.WriteLine(partFixe + partAPermutar);
    }
    else
    {
        for(int posicio = 0; posicio < partAPermutar.Length; posicio++)
        {
            iPermutacions(partFixe + partAPermutar[posicio], partAPermutar.Remove(posicio, 1));
        }
    }
}

static int Ackermann(int m, int n)
{
    int resultado;
    if (m == 0)
        resultado = n + 1;
    else if (n == 0)
        resultado = Ackermann(m - 1, 1);
    else
        resultado = Ackermann(m - 1, Ackermann(m, n - 1));
    return resultado;
}


static int DivisioRusa(int primerFactor, int segonFactor)
{
    int resultat = 0;
    List<int> llistaPrimer = new List<int>();
    List<int> llistaSegon = new List<int>();
    llistaPrimer.Add(primerFactor);
    llistaSegon.Add(segonFactor);
    iDivisioRusa(primerFactor, segonFactor, llistaPrimer, llistaSegon);

    for(int i = 0; i < llistaPrimer.Count; i++)
    {
        resultat += llistaSegon[i];
    }

    return resultat;
}

static void iDivisioRusa(int primerFactor, int segonFactor, List<int> llistaPrimer, List<int> llistaSegon)
{
    if (primerFactor != 1)
    {
        primerFactor = primerFactor / 2;
        segonFactor = segonFactor * 2;
        if (primerFactor % 2 != 0)
        {
            llistaPrimer.Add(primerFactor);
            llistaSegon.Add(segonFactor);
        }
        iDivisioRusa(primerFactor, segonFactor, llistaPrimer, llistaSegon);
    }
}



static IEnumerable<string> Sumands(int num)
{
    List<string> result = new List<string>();
    result.Add("1 = 1");
    if(num != 1)
    {
        for (int i = 2; i <= num; i++)
            iSumands(i, 1, 1, 1, result);
    }
    return result;
}

static void iSumands(int num, int restaNum, int sumaNum, int nSumaNums, List<string> result)
{
    String linia;
    if (num == restaNum)
    {

    }
    else
    {
        if(num - restaNum + nSumaNums == num)
        {
            linia = num + " = " + (num - restaNum) + " + " + nSumaNums;
            result.Add(linia);
            sumaNum--;
            nSumaNums++;
        }
        else if (sumaNum == 1)
        {
            linia = num + " = " + (num - restaNum);
            for (int i = 0; i < nSumaNums; i++)
                linia += " + 1";
            result.Add(linia);
            if(num - restaNum != 2)
                sumaNum++;
            else
                nSumaNums++;
            restaNum += 1;
        }
        else
        {
            linia = num + " = " + (num - restaNum);
            if(nSumaNums - 1 > 0)
            {
                linia += " + " + (sumaNum + 1);
                for (int i = 0; i < nSumaNums - 2; i++)
                    linia += " + 1";
                result.Add(linia);
                linia = num + " = " + (num - restaNum);
            }
            linia += " + " + sumaNum;
            for (int i = 0; i < nSumaNums - 1; i++)
                linia += " + 1";
            sumaNum = 1;
            nSumaNums++;
            result.Add(linia);
        }
        iSumands(num,restaNum, sumaNum, nSumaNums, result);
    }
    
}




static void Hanoi(int nDiscos, string origen, string destino, string aux)
{
    if(nDiscos == 1)
        Console.WriteLine(origen + "--> " + destino);
    else
    {
        Hanoi(nDiscos - 1, origen, aux, destino);
        Console.WriteLine(origen + "--> " + destino);
        Hanoi(nDiscos - 1, aux, destino, origen);
    }
}


static void BuscanMinas()
{
    String linia;
    string[] nColnFila;
    long nFila;
    long nCol;

    linia = Console.ReadLine();
    nColnFila = linia.Split(' ');
    nFila = Convert.ToInt32(nColnFila[0]);
    nCol = Convert.ToInt32(nColnFila[1]);
    char[,] mapa = new char[nFila,nCol];

    // Hacer el mapa de minas
    for(int i = 0; i < nFila; i++)
    {
        linia = Console.ReadLine();
        for (int j = 0; j < nCol; j++)
        {
            mapa[i,j] = linia[j];
        }
    }

    // Puntos
    linia = Console.ReadLine();
    bool gameover = false;
    int nPuntos = Convert.ToInt32(linia);
    int puntX;
    int puntY;
    int index = 0;
    int prova;
    int[,] puntos = new int[nPuntos, nPuntos];

    while(index < nPuntos && !gameover)
    {
        linia = Console.ReadLine();
        puntY = Convert.ToInt32(linia.Split()[0]) - 1;
        puntX = Convert.ToInt32(linia.Split()[1]) - 1;

        prova = MinasOriginalAldrededor(mapa, puntX, puntY);

        if (prova != 0)
        {
            if(prova == -1)
                gameover = true;
            else 
                mapa[puntY, puntX] = Convert.ToChar(prova.ToString());
        }
        else
        {
            MinasAlrededor(mapa, puntX, puntY);          
        }
        index++;
    }

    if (gameover)
        Console.WriteLine("GAME OVER");
    else
    {
        for(int i = 0; i < nFila; i++)
        {
            for (int j = 0; j < nCol; j++)
            {
                if(mapa[i, j] == 'N')
                    Console.Write('-');
                else if(mapa[i, j] == '*' || mapa[i, j] == '-')
                    Console.Write('X');

                else
                    Console.Write(mapa[i, j]);
            }
            Console.WriteLine();
        }
    }

}

static int MinasAlrededor(char[,] mapa, int puntX, int puntY)
{
    int resultat;
    int sumaX = -1;
    int sumaY = -1;

    resultat = MinasOriginalAldrededor(mapa, puntX, puntY);

    if (resultat == -1)
        mapa[puntY, puntX] = 'X';
    if (resultat != 0)
    {
        mapa[puntY, puntX] = Convert.ToChar(resultat.ToString());
    }
    else 
    {
        mapa[puntY, puntX] = 'N';
        for (int i = 0; i < 9; i++)
        {
            if (sumaX == 0 && sumaY == 0)
                sumaX++;
            if (DinsMapa(mapa, puntX + sumaX, puntY + sumaY) && mapa[puntY + sumaY, puntX + sumaX] == '-')
                MinasAlrededor(mapa,puntX + sumaX, puntY + sumaY);
            if (sumaX == 1)
            {
                sumaX = -1;
                sumaY++;
            }
            else
                sumaX++;
        }
    }   
    return resultat;
}

static int MinasOriginalAldrededor(char[,] mapa, int puntX, int puntY)
{
    int sumaX = -1;
    int sumaY = -1;
    int resultat = 0;
    if(mapa[puntY, puntX] == '*')
    {
        resultat = -1;
    }
    else
    {
        for (int i = 0; i < 8; i++)
        {
            if (sumaX == 0 && sumaY == 0)
                sumaX++;
            if (DinsMapa(mapa, puntX + sumaX, puntY + sumaY) && mapa[puntY + sumaY, puntX + sumaX] == '*')
            {
                resultat++;
            }
            if (sumaX == 1)
            {
                sumaX = -1;
                sumaY++;
            }
            else
            {
                sumaX++;
            }
        }
    } 
    return resultat;
}

static bool DinsMapa(char[,] mapa, int puntX, int puntY)
{
    bool dinsMapa;
    if (puntX < 0 || puntY < 0)
        dinsMapa = false;
    else if (puntX > mapa.GetLength(1) - 1 || puntY > mapa.GetLength(0) - 1)
        dinsMapa = false;
    else
        dinsMapa = true;
    return dinsMapa;
}




static void InviertiendoEnJaen()
{
    String linia;
    string[] nColnFila;
    char[,] mapa;
    int nFila;
    int nCol;
    int index = 0;
    int posX = 0;
    int posY = 0;
    int contador = 0;
    List<int> totalArboles = new List<int>();

    linia = Console.ReadLine();
    nColnFila = linia.Split(' ');
    nFila = Convert.ToInt32(nColnFila[0]);
    nCol = Convert.ToInt32(nColnFila[1]);
    mapa = new char[nFila, nCol];

    // Mapa Jaen
    for (int i = 0; i < nFila; i++)
    {
        linia = Console.ReadLine();
        for (int j = 0; j < nCol; j++)
        {
            mapa[i, j] = linia[j];
        }
    }

    while (index < nCol * nFila)
    {
        if (mapa[posY, posX] == '#')
        {
            contador++;
            contador = ArbolesAlrededorOriginal(mapa, posX, posY, contador);
        }

        if (posX == nCol - 1)
        {
            posX = 0;
            posY++;
        }
        else
            posX++;
        totalArboles.Add(contador);
        contador = 0;
        index++;
    }

    Console.WriteLine(totalArboles.Max());
}

static int ArbolesAlrededorOriginal(char[,] mapa, int posX, int posY, int contador)
{
    mapa[posY, posX] = '!';
    if (DinsMapa(mapa, posX - 1, posY) && mapa[posY, posX - 1] == '#')
    {
        contador = ArbolesAlrededorOriginal(mapa, posX - 1, posY, contador + 1);
    }
    if (DinsMapa(mapa, posX + 1, posY) && mapa[posY, posX + 1] == '#')
    {
        contador = ArbolesAlrededorOriginal(mapa, posX + 1, posY, contador + 1);
    }
    if (DinsMapa(mapa, posX, posY - 1) && mapa[posY - 1, posX] == '#')
    {
        contador = ArbolesAlrededorOriginal(mapa, posX, posY - 1, contador + 1);
    }
    if (DinsMapa(mapa, posX, posY + 1) && mapa[posY + 1, posX] == '#')
    {
        contador = ArbolesAlrededorOriginal(mapa, posX, posY + 1, contador + 1);
    }

    return contador;
}




static int ConversorBase6(int num)
{
    List<int> list = new List<int>();
    String res = "";
    int resultado;
    if (num < 6)
        list.Add(num);
    else
        iConversorBase6(num, 6, list);
    for(int i = list.Count - 1; i >= 0; i--)
    {
        res += list[i];
    }
    return Convert.ToInt32(res);
}

static void iConversorBase6(int num, int divisor, List<int> res)
{
    if (divisor > num)
        res.Add(num);
    else
    {
        res.Add(num % divisor);
        iConversorBase6(num / divisor, divisor, res);
    }
}