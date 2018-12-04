using System.Reflection;

namespace AdventOfCode.Solutions.Problem
{
    public static class ProblemFactory
    {
        public static IProblem Create<T>() where T : IProblem
        {
            return (IProblem)Assembly.GetExecutingAssembly()
                .CreateInstance(typeof(T).FullName);
        }
    }
}
