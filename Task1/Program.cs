bool CheckSimmetry(int[,] m, int n)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = i + 1; j < n; j++)
        {
            if ((m[i, j] != m[j, i]) && (i != j)) return false;
        }
    }
    return true;
}

void ShowMatrix(int[,] m, int n)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++) Console.Write($"{m[i, j]} ");
        Console.WriteLine(' ');
    }
}

int VectorMultMatrixMultTVector(int[,] m, int[] vector, int n)
{
    int[] result = new int[n];
    for (int i = 0; i < n; i++)
    {
        result[i] = 0;
    }
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            result[i] += vector[i] * m[j, i];
        }

    }
    int sum = 0;
    for (int i = 0; i != n; i++)
    {

        sum += result[i] * vector[i];

    }
    return sum;
}



string fi = @"test.txt";
string line;
StreamReader sr = new StreamReader(fi);
int n = Convert.ToInt32(sr.ReadLine());
int[,] x = new int[n, n];
for (int i = 0; i < n; i++)
{
    line = sr.ReadLine();

    char el;
    int o = 0;
    int u = line.Length;


    for (int l = 0; l < u; l++)
    {


        el = line[l];

        if (line[l] == ' ') continue;
        else
        {
            x[i, o] = Convert.ToInt32(el) - 48;
            o++;
        }
    }


}
int[] vector = sr.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

ShowMatrix(x, n);
if (CheckSimmetry(x, n))
{
    int result = VectorMultMatrixMultTVector(x, vector, n);
    Console.WriteLine(Math.Sqrt(result));
}
else
{
    Console.WriteLine("Ваша матрица на симметрична");
}