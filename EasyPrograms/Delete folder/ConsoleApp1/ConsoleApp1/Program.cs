﻿using System;
using System.Text;
using System.IO;
namespace TestApplicationForStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            string deletePath = "D:\\FolderToDelete"; //Можно написать вот так string deletePath = @"D:\FolderToDelete";
            deleteFolder(deletePath); //Вызываем наш рекурсивный метод
            //После вызова метода deleteFolder() папка, путь которой указан в deletePath,
            //должна быть пуста. Остаётся просто удалить её.
            //Делаем это, вызвав метод Directory.Delete().
            try
            {
                Directory.Delete(deletePath);
                Console.WriteLine("Папка {0} успешно удалена", deletePath);
            }
            catch
            {
                Console.WriteLine("При удалении папки возникли ошибки");
            }
            Console.ReadLine();
        }

        //Рекурсивный метод по удалению папки. Рекурсия - это метод, который вызывает сам себя.
        static void deleteFolder(string folder)
        {
            try
            {
                //Класс DirectoryInfo как раз позволяет работать с папками. Создаём объект этого
                //класса, в качестве параметра передав путь до папки.
                DirectoryInfo di = new DirectoryInfo(folder);
                //Создаём массив дочерних вложенных директорий директории di
                DirectoryInfo[] diA = di.GetDirectories();
                //Создаём массив дочерних файлов директории di
                FileInfo[] fi = di.GetFiles();
                //В цикле пробегаемся по всем файлам директории di и удаляем их
                foreach (FileInfo f in fi)
                {
                    f.Delete();
                }
                //В цикле пробегаемся по всем вложенным директориям директории di 
                foreach (DirectoryInfo df in diA)
                {
                    //Как раз пошла рекурсия
                    deleteFolder(df.FullName);
                    //Если в папке нет больше вложенных папок и файлов - удаляем её,
                    if (df.GetDirectories().Length == 0 && df.GetFiles().Length == 0) df.Delete();
                }
            }
            //Начинаем перехватывать ошибки
            //DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
            }
            //UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
            }
            //Во всех остальных случаях
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
            }
        }
    }
}