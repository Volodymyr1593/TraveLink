






int[] array = { 1, 2, 3, 4 };

static void Sum(int[] array,int count = 0,int currency = 0)
{


    int result =  currency + array[count];
    count++;

    if (count >= array.Length)
    {
        Console.WriteLine(result);
        return;

    }
    Sum(array,count,result);

   

}




static void FigureSum(int value,int result=0)
{

    result = value % 10 + result;
    if (value < 10)
    {
        Console.WriteLine(result);
        return;
    }

   
    int valyetemp = value / 10;

    FigureSum(valyetemp,result) ;



}

FigureSum(123);

Func <int,int>func;


