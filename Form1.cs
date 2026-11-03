using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Indivydal
{
    public partial class Form1 : Form
    {
        private TextBox txtN;
        private TextBox txtM;
        private Button btnRun;
        private ListBox listBoxResults;

        public Form1()
        {
            // Налаштування головного вікна
            this.Text = "Дослідження рівнянь - Варіант";
            this.Width = 650;
            this.Height = 480;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Поле для N (лінійні)
            Label lblN = new Label { Text = "Кількість лінійних (n):", Left = 20, Top = 20, Width = 140 };
            txtN = new TextBox { Text = "5", Left = 170, Top = 18, Width = 60 };

            // Поле для M (квадратні)
            Label lblM = new Label { Text = "Кількість квадратних (m):", Left = 250, Top = 20, Width = 150 };
            txtM = new TextBox { Text = "5", Left = 410, Top = 18, Width = 60 };

            // Кнопка запуску
            btnRun = new Button { Text = "Згенерувати та обчислити", Left = 20, Top = 55, Width = 450, Height = 30 };
            btnRun.Click += BtnRun_Click;

            // Вікно виведення результатів
            listBoxResults = new ListBox { Left = 20, Top = 100, Width = 590, Height = 310 };

            // Додавання елементів на форму
            this.Controls.Add(lblN);
            this.Controls.Add(txtN);
            this.Controls.Add(lblM);
            this.Controls.Add(txtM);
            this.Controls.Add(btnRun);
            this.Controls.Add(listBoxResults);
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            listBoxResults.Items.Clear();

            if (!int.TryParse(txtN.Text, out int n) || !int.TryParse(txtM.Text, out int m) || n < 0 || m < 0)
            {
                MessageBox.Show("Введіть коректні числа для n та m!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Random rand = new Random();
            List<TEquation> equations = new List<TEquation>();

            // Генерація лінійних рівнянь
            for (int i = 0; i < n; i++)
            {
                double a = rand.Next(-10, 11);
                if (a == 0) a = 1;
                double b = rand.Next(-20, 21);
                equations.Add(new TLinearEquation(a, b));
            }

            // Генерація квадратних рівнянь
            for (int i = 0; i < m; i++)
            {
                double a = rand.Next(-5, 6);
                if (a == 0) a = 1;
                double b = rand.Next(-10, 11);
                double c = rand.Next(-15, 16);
                equations.Add(new TQuadraticEquation(a, b, c));
            }

            double sumLinearRoots = 0;
            double sumQuadraticRoots = 0;
            int linearRootCount = 0;
            int quadraticRootCount = 0;

            listBoxResults.Items.Add("=== ЗГЕНЕРОВАНІ РІВНЯННЯ ТА ЇХ КОРЕНІ ===");

            foreach (var eq in equations)
            {
                double[] roots = eq.GetRoots();
                string rootsStr = roots.Length > 0 ? string.Join(", ", roots.Select(r => r.ToString("F2"))) : "немає дійсних коренів";

                listBoxResults.Items.Add($"{eq} | Корені: {rootsStr}");

                if (eq is TLinearEquation)
                {
                    foreach (var root in roots)
                    {
                        sumLinearRoots += root;
                        linearRootCount++;
                    }
                }
                else if (eq is TQuadraticEquation)
                {
                    foreach (var root in roots)
                    {
                        sumQuadraticRoots += root;
                        quadraticRootCount++;
                    }
                }
            }

            listBoxResults.Items.Add("");
            listBoxResults.Items.Add("=== РЕЗУЛЬТАТИ ОБЧИСЛЕНЬ ===");
            listBoxResults.Items.Add($"Сума коренів лінійних рівнянь: {(linearRootCount > 0 ? sumLinearRoots.ToString("F2") : "корені відсутні")}");
            listBoxResults.Items.Add($"Сума коренів квадратних рівнянь: {(quadraticRootCount > 0 ? sumQuadraticRoots.ToString("F2") : "корені відсутні")}");
        }
    }
}