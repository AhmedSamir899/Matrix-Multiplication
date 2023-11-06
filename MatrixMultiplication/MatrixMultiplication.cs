using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class MatrixMultiplication
    {
        #region YOUR CODE IS HERE
        
        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        static public  int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {


            if (N <= 64)
            {
                int[,] m = new int[N, N];

                int i, j, k;
                for (i = 0; i < N; i++)
                {
                    for (k = 0; k < N; k++)
                    {
                        for (j = 0; j < N; j++)
                        {
                        

                            m[i,j] += M1[i,k] * M2[k,j];
                        }
                    }
                }

                return m;
            
            }

            int half = N / 2;
            
            int z, w;
            
            int[,] a= new int[half, half];
            int[,] b= new int[half, half];
            int[,] c= new int[half, half];
            int[,] d= new int[half, half];

            int[,] e= new int[half, half];
            int[,] f= new int[half, half];
            int[,] g= new int[half, half];
            int[,] h= new int[half, half];


            int[,] f_h = new int[half, half];
            int[,] a_b = new int[half, half];
            int[,] c_d = new int[half, half];
            int[,] g_e = new int[half, half];

            int[,] a_d = new int[half, half];
            int[,] e_h = new int[half, half];
            int[,] b_d = new int[half, half];
            int[,] g_h = new int[half, half];

            int[,] a_c = new int[half, half];
            int[,] e_f = new int[half, half];







            for (int i = 0; i < half; i++)
            {
                for (int j = 0; j < half; j++)
                {
                    z = i + half; w = j + half;


                        a[i, j] = M1[i, j];
                        e[i, j] = M2[i, j];
                    
                        b[i, j ] = M1[i, w];
                        f[i, j ] = M2[i, w];

                        c[i , j] = M1[z, j];
                        g[i , j] = M2[z, j];

                        d[i , j ] = M1[z, w];
                        h[i , j ] = M2[z, w];

                        f_h[i, j] = M2[i, w] - M2[z, w];

                        a_b[i, j] = M1[i, j] + M1[i, w];

                        c_d[i, j] = M1[z, j] + M1[z, w];

                        g_e[i, j] = M2[z, j] - M2[i, j];

                        a_d[i, j] = M1[i, j] + M1[z, w];

                        e_h[i, j] = M2[i, j] + M2[z, w];

                        b_d[i, j] = M1[i, w] - M1[z, w];

                        g_h[i, j] = M2[z, j] + M2[z, w];

                        a_c[i, j] = M1[i, j] - M1[z, j];

                        e_f[i, j] = M2[i, j] + M2[i, w];


                }
            }
            
            int[,] p1 = new int[half, half];
            int[,] p2 = new int[half, half];
            int[,] p3 = new int[half, half];
            int[,] p4 = new int[half, half];
            int[,] p5 = new int[half, half];
            int[,] p6 = new int[half, half];
            int[,] p7 = new int[half, half];



            Parallel.Invoke
            (
                            () =>
                            {
                                p1 = MatrixMultiply(a, (f_h), half);
                            },

                            () =>
                            {
                                p2 = MatrixMultiply(a_b, h, half);
                            },

                            () =>
                            {
                                 p3 = MatrixMultiply(c_d, e, half);
                            }
                            ,
                            () =>
                            {
                                p4 = MatrixMultiply(d, (g_e), half);
                            }

                            ,
                            () =>
                            {
                                p5 = MatrixMultiply(a_d, e_h, half);
                            }
                            ,
                            () =>
                            {
                                p6 = MatrixMultiply(b_d, g_h, half);
                            }
                            ,
                            () =>
                            {
                                p7 = MatrixMultiply(a_c, e_f, half);
                            }

             );



            int[,] mm = new int[N, N];


            for (int i = 0; i < half; i++)
            {
                for (int j = 0; j < half; j++)
                {
                   z = i + half; w = j + half;


                   mm[i, j] = p5[i, j] + p4[i, j] - p2[i, j] + p6[i, j];

                   mm[i, w]= p1[i, j] + p2[i, j];
                  
                   mm[z, j] = p3[i, j] + p4[i, j];

                   mm[z, w] = p5[i, j] + p1[i, j] - p3[i, j] - p7[i, j];


                }
            }




            return mm;

        }

        #endregion
    }
}
