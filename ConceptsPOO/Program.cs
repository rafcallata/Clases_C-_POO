
using ConceptsPOO;

Console.WriteLine("POO Concepts");
Console.WriteLine("============");

try
{
    Employee employee1 = new SalaryEmployee()
    {
        Id = 1010,
        FirstName = "Sandra",
        LastName = "Morales",
        BirthDate = new Date(1990, 5, 23),
        HiringDate = new Date(2022,1,15),
        IsActive = true,
        Salary = 1815453.45M
    };
    Console.WriteLine(employee1);
    Employee employee2 = new CommissionEmploye()
    {
        Id = 2020,
        FirstName = "Patricia",
        LastName = "Gutierrez",
        BirthDate = new Date(1995, 5, 23),
        HiringDate = new Date(2022, 2, 5),
        IsActive = true,
        Sales = 320000000M,
        CommisionPercentaje = 0.03F
    };
    Console.WriteLine(employee2);
}
catch (Exception error)
{
    Console.WriteLine(error.Message);
}
