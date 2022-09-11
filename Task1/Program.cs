using System;
using System.IO;

namespace Task1 //Напишите программу, которая чистит нужную нам папку от файлов и папок, которые не использовались более 30 минут 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = EnterPath();
            DeleteFiles(path);
            OneMore();

        }

        // Вводится путь 
        static string EnterPath()
        {
            Console.WriteLine("Введите путь до папки, например C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\SkillFactory");
            string path = Console.ReadLine();
            return path;
        }

        // Удаление папок
        static void DeleteFiles(string path)
        {
            try
            {
                // Проверка существования директории по заданному пути
                if (Directory.Exists(path))
                {
                    // Вывод всех папок и файлов в директории
                    Console.WriteLine("Папки: ");
                    string[] dirs = Directory.GetDirectories(path);

                    foreach (string dir in dirs)
                    {
                        Console.WriteLine(dir);
                    }

                    Console.WriteLine("Файлы: ");
                    string[] file = Directory.GetFiles(path);

                    foreach (string files in file)
                    {
                        Console.WriteLine(files);
                    }

                    Console.WriteLine("Сейчас произойдет удаление");
                    DateTime LastAccess;

                    foreach (string dir in dirs)
                    {
                        //Получаем информацию о том, когда последний раз проводили работу с папками или файлами
                        LastAccess = Directory.GetLastWriteTime(dir);
                        if (DateTime.Now.AddMinutes(-30) >= LastAccess)
                        {
                            Directory.Delete(dir, true);
                            Console.WriteLine($"Папка {dir} удалена");
                        }
                    }

                    foreach (string files in file)
                    {
                        LastAccess = File.GetLastWriteTime(files);
                        if (DateTime.Now.AddMinutes(-30) >= LastAccess)
                        {
                            File.Delete(files);
                            Console.WriteLine($"Файл {files} удален");
                        }
                    }



                }
            }
            catch (Exception ex) // Вывод ошибки
            {
                Console.WriteLine("Не удается добраться до файла: " + ex.Message);
                EnterPath();
            }
        }

        static void OneMore()
        {
            Console.WriteLine("Хотите запустить программу еще раз?\n 1. Да\n 2. Нет");
            int answer = int.Parse(Console.ReadLine());
            switch (answer)
            {
                case 1:
                    {
                        EnterPath();
                        break;
                    }
                case 2:
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Некорректный ввод");
                        OneMore();
                        break;
                    }

            }
        }


    }
}
