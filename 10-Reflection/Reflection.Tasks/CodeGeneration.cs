using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reflection.Tasks
{
    public class CodeGeneration
    {
        /// <summary>
        /// Returns the functions that returns vectors' scalar product:
        /// (a1, a2,...,aN) * (b1, b2, ..., bN) = a1*b1 + a2*b2 + ... + aN*bN
        /// Generally, CLR does not allow to implement such a method via generics to have one function for various number types:
        /// int, long, float. double.
        /// But it is possible to generate the method in the run time! 
        /// See the idea of code generation using Expression Tree at: 
        /// http://blogs.msdn.com/b/csharpfaq/archive/2009/09/14/generating-dynamic-methods-with-expression-trees-in-visual-studio-2010.aspx
        /// https://devblogs.microsoft.com/visualstudio/generating-dynamic-methods-with-expression-trees-in-visual-studio-2010/
        /// </summary>
        /// <typeparam name="T">number type (int, long, float etc)</typeparam>
        /// <returns>
        ///   The function that return scalar product of two vectors
        ///   The generated dynamic method should be equal to static MultuplyVectors (see below).   
        /// </returns>
        public static Func<T[], T[], T> GetVectorMultiplyFunction<T>() where T : struct
        {
            // TODO : Implement GetVectorMultiplyFunction<T>.
            ParameterExpression array1 = Expression.Parameter(typeof(T[]), "array1");
            ParameterExpression array2 = Expression.Parameter(typeof(T[]), "array2");

            ParameterExpression index = Expression.Variable(typeof(int), "ind");

            BinaryExpression elementArray1 = Expression.ArrayIndex(array1, index);
            BinaryExpression elementArray2 = Expression.ArrayIndex(array2, index);

            ParameterExpression result = Expression.Variable(typeof(T), "Result");

            var breakLabel = Expression.Label("LoopBreak");
          
            BlockExpression block = Expression.Block(
                new[] { result, index },
                Expression.Loop(
                    Expression.IfThenElse(
                               Expression.LessThan(index, Expression.ArrayLength(array1)),

                               Expression.Block(
                               Expression.Assign(result, Expression.Add(result, Expression.Multiply(elementArray1, elementArray2))),
                               Expression.Assign(index, Expression.Add(index, Expression.Constant(1)))
                                               ),

                               Expression.Break(breakLabel)
                                         ),
                       breakLabel
                                ),
                    result
              );

              Expression<Func<T[], T[], T>> lambda = Expression.Lambda<Func<T[], T[], T>>(block, array1, array2);
           
              var method = lambda.Compile();
              return method;
        }


        // Static solution to check performance benchmarks
        public static int MultuplyVectors(int[] first, int[] second)
        {
            int result = 0;
            for (int i = 0; i < first.Length; i++)
            {
                result += first[i] * second[i];
            }
            return result;
        }

    }
}
