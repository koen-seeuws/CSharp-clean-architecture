namespace MyTodos.Application.Contracts.RandomNumbers;

public interface IRandomNumberGenerator
{
    Task<int[]> GenerateRandomNumbers(int count, int min, int max);
}