using System;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // Интерфейс для человека
    public interface IЧеловек
    {
        // Метод для получения информации о человеке
        string Информация();
        // Метод для реакции на другого человека
        string Реакция(IЧеловек другой);
    }

    // Класс для девушки, реализует интерфейсы IЧеловек, IComparable и ICloneable
    public class Девушка : IЧеловек, IComparable<Девушка>, ICloneable
    {
        // Свойства класса
        public string Фамилия { get; set; }
        public int Возраст { get; set; }

        // Конструктор класса
        public Девушка(string фамилия, int возраст)
        {
            Фамилия = фамилия; // Инициализация фамилии
            Возраст = возраст; // Инициализация возраста
        }

        // Метод для получения информации о девушке
        public string Информация()
        {
            return $"Девушка: {Фамилия}, Возраст: {Возраст}"; // Формирование строки информации
        }

        // Метод для реакции на другого человека
        public string Реакция(IЧеловек другой)
        {
            return $"Девушка {Фамилия} увидела {другой.Информация()}"; // Реакция на другого человека
        }

        // Метод для сравнения по фамилии
        public int CompareTo(Девушка другая)
        {
            if (другая == null) return 1; // Если другая девушка null, текущая больше
            return string.Compare(this.Фамилия, другая.Фамилия); // Сравнение фамилий
        }

        // Метод для клонирования объекта
        public object Clone()
        {
            return new Девушка(Фамилия, Возраст); // Возвращаем новый экземпляр с теми же данными
        }
    }

    // Класс для парня, реализует интерфейсы IЧеловек, IComparable и ICloneable
    public class Парень : IЧеловек, IComparable<Парень>, ICloneable
    {
        // Свойства класса
        public string Фамилия { get; set; }
        public int Возраст { get; set; }

        // Конструктор класса
        public Парень(string фамилия, int возраст)
        {
            Фамилия = фамилия; // Инициализация фамилии
            Возраст = возраст; // Инициализация возраста
        }

        // Метод для получения информации о парне
        public string Информация()
        {
            return $"Парень: {Фамилия}, Возраст: {Возраст}"; // Формирование строки информации
        }

        // Метод для реакции на другого человека
        public string Реакция(IЧеловек другой)
        {
            return $"Парень {Фамилия} увидел {другой.Информация()}"; // Реакция на другого человека
        }

        // Метод для сравнения по фамилии
        public int CompareTo(Парень другой)
        {
            if (другой == null) return 1; // Если другой парень null, текущий больше
            return string.Compare(this.Фамилия, другой.Фамилия); // Сравнение фамилий
        }

        // Метод для клонирования объекта
        public object Clone()
        {
            return new Парень(Фамилия, Возраст); // Возвращаем новый экземпляр с теми же данными
        }
    }

    public partial class MainWindow : Window
    {
        private List<IЧеловек> люди; // Список людей

        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна

            люди = new List<IЧеловек> // Создание списка людей и добавление объектов в него
            {
                new Девушка("Иванова", 22),  // Добавление девушки в список 
                new Парень("Петров", 25),     // Добавление парня в список 
                new Девушка("Сидорова", 20),  // Добавление еще одной девушки в список 
                new Парень("Смирнов", 30)      // Добавление еще одного парня в список 
            };

            // Сортировка людей по фамилии с использованием лямбда-выражения
            люди.Sort((x, y) =>
            {
                string фамилияX = x is Девушка ? ((Девушка)x).Фамилия : ((Парень)x).Фамилия;
                string фамилияY = y is Девушка ? ((Девушка)y).Фамилия : ((Парень)y).Фамилия;
                return string.Compare(фамилияX, фамилияY); // Сравнение фамилий
            });

            foreach (var человек in люди) // Перебор списка людей для отображения информации в ListBox 
            {
                listBoxPeople.Items.Add(человек.Информация()); // Добавление информации о каждом человеке в ListBox 
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();  // Закрытие приложения при нажатии кнопки "Выход"
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Кошеренков Максим Сергеевич\nНомер работы: 9\nЗадание: Создать интерфейс человек и классы для реализации.", "О программе");
            // Вывод информации о разработчике и задании при нажатии кнопки "О программе"
        }

        private void ShowReactions_Click(object sender, RoutedEventArgs e)
        {
            textBlockInfo.Text = "";  // Очистка текстового блока перед выводом реакций

            foreach (var человек in люди)  // Перебор списка людей для отображения реакций 
            {
                foreach (var другой in люди)
                {
                    if (другой != человек)  // Проверка на то, что это не тот же самый человек 
                    {
                        textBlockInfo.Text += человек.Реакция(другой) + "\n";  // Добавление реакции к текстовому блоку 
                    }
                }
            }
        }
    }
}